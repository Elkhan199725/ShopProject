using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class UserCreatedisAdminColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invoices");
        }
    }
}
