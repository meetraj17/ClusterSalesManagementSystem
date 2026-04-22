using ClusterSalesManagementSystem.DTOs;
using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClusterSalesManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly SalesDbContext Context;
        private readonly IConfiguration configuration;

        public ReportController(SalesDbContext context, IConfiguration configuration)
        {
            Context = context;
            this.configuration = configuration;
        }
        public async Task<List<SalesReportResult>> GetSalesReport(SalesReportVM model)
        {
            List<SalesReportResult> list = new List<SalesReportResult>();

            string connStr = configuration.GetConnectionString("dbcs");

            using (SqlConnection con = new SqlConnection(connStr))
            { 
                using (SqlCommand cmd = new SqlCommand("GetSalesReport_Sp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FromDate", (object?)model.FromDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ToDate", (object?)model.ToDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ClusterId", (object?)model.ClusterId ?? DBNull.Value);

                    await con.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new SalesReportResult
                            {
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                CluName = reader["CluName"].ToString()
                            });
                        }
                    }
                }
            }

            return list;
        }

        public async Task<IActionResult> ReportView(SalesReportVM model)
        {
            if (model.FromDate == null && model.ToDate == null && model.ClusterId == null)
            {
                model.ToDate = DateTime.Now;
                model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                model.SalesList = new List<SalesReportResult>(); // empty
            }
            else
            {
                // User clicked GO → fetch data
                model.SalesList = await GetSalesReport(model);
            }


            ViewBag.Clusters = new SelectList(Context.Clusters, "CluId", "CluName");

            return View(model);
        }
    }
}
