using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClusterSalesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ClusterCreateAndSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    CluId = table.Column<decimal>(type: "Numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CluName = table.Column<string>(type: "Nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clusters", x => x.CluId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SalesId = table.Column<decimal>(type: "Numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesCluId = table.Column<decimal>(type: "Numeric(18,0)", nullable: false),
                    SalesAmount = table.Column<decimal>(type: "Numeric(18,0)", nullable: false),
                    InvoiceUrl = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    SalesDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SalesId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
