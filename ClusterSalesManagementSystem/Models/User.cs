using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClusterSalesManagementSystem.Models
{
    public class User
    {
        [Key]
        [Column("UserId", TypeName = "Numeric(18,0)")]
        public int UserId { get; set; }
        [Column(TypeName = "Nvarchar(100)")]
        public String? UserName { get; set; }
        [Column("UserPassword", TypeName = "Nvarchar(100)")]
        public string? PasswordHash { get; set; }
        [Column("UserRole", TypeName = "Nvarchar(100)")]
        public String? UserRole { get; set; }
    }
}
