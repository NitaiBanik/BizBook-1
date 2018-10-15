using Microsoft.EntityFrameworkCore.Migrations;

namespace BizBook.Migrations
{
    public partial class altermodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfile_Consumer_ConsumerID",
                table: "BusinessProfile");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProfile_ConsumerID",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "ConsumerID",
                table: "BusinessProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsumerID",
                table: "BusinessProfile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfile_ConsumerID",
                table: "BusinessProfile",
                column: "ConsumerID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfile_Consumer_ConsumerID",
                table: "BusinessProfile",
                column: "ConsumerID",
                principalTable: "Consumer",
                principalColumn: "ConsumerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
