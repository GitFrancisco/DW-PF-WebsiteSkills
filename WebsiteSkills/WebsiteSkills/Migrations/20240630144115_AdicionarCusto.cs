using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteSkills.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCusto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Custo",
                table: "Skills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Custo",
                table: "Skills");
        }
    }
}
