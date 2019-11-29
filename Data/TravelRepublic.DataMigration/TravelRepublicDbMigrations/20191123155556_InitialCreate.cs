using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelRepublic.DataMigration.TravelRepublicDbMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Establishments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstablishmentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    EstablishmentType = table.Column<string>(nullable: true),
                    MinCost = table.Column<double>(nullable: false),
                    Stars = table.Column<int>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    UserRating = table.Column<double>(nullable: false),
                    UserRatingTitle = table.Column<string>(nullable: true),
                    UserRatingCount = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ThumbnailUrl = table.Column<string>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: true),
                    DeletionReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishments", x => x.Id);
                });
            
            //NLog
            migrationBuilder.Sql(NLogSql.GetCreateLogTable(3));
            migrationBuilder.Sql(NLogSql.GetInsertLogSp());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Establishments");

            migrationBuilder.Sql(NLogSql.GetDropSp());
            migrationBuilder.DropTable("Logs");
        }
    }
}
