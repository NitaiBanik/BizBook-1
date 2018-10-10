using Microsoft.EntityFrameworkCore.Migrations;

namespace BizBook.Data.Migrations
{
    public partial class pk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BusinessProfile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfile_ApplicationUserId",
                table: "BusinessProfile",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfile_AspNetUsers_ApplicationUserId",
                table: "BusinessProfile",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfile_AspNetUsers_ApplicationUserId",
                table: "BusinessProfile");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProfile_ApplicationUserId",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BusinessProfile");
        }
    }
}
