using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClusterSalesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AllTableCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<decimal>(type: "Numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    UserPassword = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    UserRole = table.Column<string>(type: "Nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
