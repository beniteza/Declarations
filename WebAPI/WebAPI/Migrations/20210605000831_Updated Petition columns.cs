using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class UpdatedPetitioncolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubmittedDate",
                table: "Petition",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Petition",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Petition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Petition");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Petition",
                newName: "SubmittedDate");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Petition",
                newName: "State");
        }
    }
}
