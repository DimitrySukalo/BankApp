using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApp.DAL.Migrations
{
    public partial class AddedPiggyBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PiggyBanks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Money = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PiggyBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PiggyBanks_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PiggyBanks");
        }
    }
}
