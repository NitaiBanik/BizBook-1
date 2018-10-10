using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BizBook.Data.Migrations
{
    public partial class BusinessProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessProfile",
                columns: table => new
                {
                    BusinessID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusinessName = table.Column<string>(nullable: true),
                    BusinessType = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    CityStateZip = table.Column<string>(nullable: true),
                    BusinessBio = table.Column<string>(nullable: true),
                    Promotions = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProfile", x => x.BusinessID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessProfile");
        }
    }
}
