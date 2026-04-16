using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClusterSalesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCluUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CluUserId",
                table: "Clusters",
                type: "Numeric(18,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CluUserId",
                table: "Clusters");
        }
    }
}
