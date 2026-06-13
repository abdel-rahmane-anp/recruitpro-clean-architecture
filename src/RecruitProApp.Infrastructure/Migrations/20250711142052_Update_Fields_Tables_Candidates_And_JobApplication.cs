using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitProApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Fields_Tables_Candidates_And_JobApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Candidates");
        }
    }
}
