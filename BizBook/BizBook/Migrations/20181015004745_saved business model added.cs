using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BizBook.Migrations
{
    public partial class savedbusinessmodeladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedBusiness",
                columns: table => new
                {
                    SavedBusinessId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusinessId = table.Column<int>(nullable: false),
                    ConsumerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedBusiness", x => x.SavedBusinessId);
                    table.ForeignKey(
                        name: "FK_SavedBusiness_BusinessProfile_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "BusinessProfile",
                        principalColumn: "BusinessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedBusiness_Consumer_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumer",
                        principalColumn: "ConsumerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedBusiness_BusinessId",
                table: "SavedBusiness",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedBusiness_ConsumerId",
                table: "SavedBusiness",
                column: "ConsumerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedBusiness");
        }
    }
}
