using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.DataAccess.Migrations
{
    public partial class NewTable111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersAndOrders_OrderId",
                table: "UsersAndOrders");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UsersAndOrders_OrderId_UserId",
                table: "UsersAndOrders",
                columns: new[] { "OrderId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UsersAndOrders_OrderId_UserId",
                table: "UsersAndOrders");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAndOrders_OrderId",
                table: "UsersAndOrders",
                column: "OrderId");
        }
    }
}
