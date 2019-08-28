using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.WebApplication.Data.Migrations
{
    public partial class _00000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorFilterItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorFilterItems",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorFilterItemid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorFilterItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_AuthorFilterItems_AuthorFilterItems_AuthorFilterItemid",
                        column: x => x.AuthorFilterItemid,
                        principalTable: "AuthorFilterItems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorFilterItems_AuthorFilterItemid",
                table: "AuthorFilterItems",
                column: "AuthorFilterItemid");
        }
    }
}
