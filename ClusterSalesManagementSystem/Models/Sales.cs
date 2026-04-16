using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace ClusterSalesManagementSystem.Models
{
    public class Sales
    {
        [Key]
        [Column("SalesId", TypeName = "Numeric(18,0)")]
        public int SalesId { get; set; }
        [Column("SalesCluId", TypeName = "Numeric(18,0)")]
        public int SalesCluId { get; set; }
        [Column("SalesAmount", TypeName = "Numeric(18,0)")]
        public Decimal SalesAmount { get; set; }
        [Column("InvoiceUrl", TypeName = "Nvarchar(100)")]
        public string? InvoiceUrl { get; set; }
        public DateTime SalesDate { get; set; }
    }
}
