using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BizBook.Data.Migrations
{
    public partial class changeupblogposttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "BlogPost");

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "BlogPost",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    AdID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdPost = table.Column<bool>(nullable: false),
                    Carousel = table.Column<bool>(nullable: false),
                    PaymentCollected = table.Column<bool>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.AdID);
                    table.ForeignKey(
                        name: "FK_Ad_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_ApplicationUserId",
                table: "Ad",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ad");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "BlogPost");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "BlogPost",
                nullable: false,
                defaultValue: false);
        }
    }
}
