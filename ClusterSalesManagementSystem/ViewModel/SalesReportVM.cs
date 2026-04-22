using ClusterSalesManagementSystem.DTOs;
using ClusterSalesManagementSystem.Models;

namespace ClusterSalesManagementSystem.ViewModel
{
    public class SalesReportVM
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ClusterId { get; set; }

        public List<SalesReportResult> ?SalesList { get; set; }
    }
}
