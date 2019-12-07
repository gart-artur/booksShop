using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.DataAccess.Migrations
{
    public partial class NewTable2323232 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersAndOrders_AspNetUsers_UserId1",
                table: "UsersAndOrders");

            migrationBuilder.DropIndex(
                name: "IX_UsersAndOrders_UserId1",
                table: "UsersAndOrders");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UsersAndOrders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersAndOrders",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAndOrders_AspNetUsers_UserId",
                table: "UsersAndOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersAndOrders_AspNetUsers_UserId",
                table: "UsersAndOrders");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UsersAndOrders",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UsersAndOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAndOrders_UserId1",
                table: "UsersAndOrders",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAndOrders_AspNetUsers_UserId1",
                table: "UsersAndOrders",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
