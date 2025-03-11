using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatRInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Elapse_Add_Year_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Elapse_NumberOfDays",
                table: "Plans",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elapse_NumberOfDays",
                table: "Plans");
        }
    }
}
