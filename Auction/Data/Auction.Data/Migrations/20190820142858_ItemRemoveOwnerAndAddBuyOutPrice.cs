using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.Data.Migrations
{
    public partial class ItemRemoveOwnerAndAddBuyOutPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Items",
                newName: "AuctionUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_OwnerId",
                table: "Items",
                newName: "IX_Items_AuctionUserId");

            migrationBuilder.AddColumn<decimal>(
                name: "BuyOutPrice",
                table: "Items",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_AuctionUserId",
                table: "Items",
                column: "AuctionUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_AuctionUserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BuyOutPrice",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "AuctionUserId",
                table: "Items",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_AuctionUserId",
                table: "Items",
                newName: "IX_Items_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
