using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Ordering_System.Migrations
{
    public partial class AddForeignKeyProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellerID",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerID",
                table: "Products",
                column: "SellerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_SellerID",
                table: "Products",
                column: "SellerID",
                principalTable: "Sellers",
                principalColumn: "SellerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_SellerID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellerID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellerID",
                table: "Products");
        }
    }
}
