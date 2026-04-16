using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClusterSalesManagementSystem.Models
{
    public class Cluster
    {
        [Key]
        [Column("CluId", TypeName = "Numeric(18,0)")]
        public int CluId { get; set; }
        [Column("CluName", TypeName = "Nvarchar(100)")]
        public string? CluName { get; set; }
        [Column("CluUserId", TypeName = "Numeric(18,0)")]
        public int CluUserId { get; set; }
        [ForeignKey("CluUserId")]
        public User User { get; set; }
    }
}
