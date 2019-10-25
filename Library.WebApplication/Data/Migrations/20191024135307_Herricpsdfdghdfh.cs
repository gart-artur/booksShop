using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.WebApplication.Data.Migrations
{
    public partial class Herricpsdfdghdfh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Orders",
                newName: "Total");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Orders",
                newName: "Amount");
        }
    }
}
