using ClusterSalesManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClusterSalesManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly SalesDbContext context;

        public DashboardController(SalesDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            var data = context.Sales.GroupBy(x => x.SalesCluId).Select(g => new
            {
                Cluster = g.Key,
                Total = g.Sum(x => x.SalesAmount)
            }).ToList();

            return View(data);
        }
    }
}
