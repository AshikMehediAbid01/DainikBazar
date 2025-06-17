using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DainikBazar.Storage.Migrations
{
    /// <inheritdoc />
    public partial class CodeMergeFromDevelop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualPrice",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualPrice",
                table: "Carts");
        }
    }
}
