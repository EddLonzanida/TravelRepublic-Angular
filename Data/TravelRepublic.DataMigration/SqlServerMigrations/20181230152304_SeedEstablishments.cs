using Eml.DataRepository;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using TravelRepublic.Data.Dto;

namespace TravelRepublic.DataMigration.SqlServerMigrations
{
    public partial class SeedEstablishments : Migration
    {
        private const string COLUMN_ID = "Id";
        private const string RELATIVE_FOLDER = "SampleDataSources";

        private readonly string tableName;
        private readonly string[] columns = new[] { COLUMN_ID, "EstablishmentId", "Name", "EstablishmentType", "Stars", "UserRating", "UserRatingTitle", "UserRatingCount", "Location", "Distance", "MinCost", "ThumbnailUrl", "ImageUrl" };

        public SeedEstablishments()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine($"InsertData: {tableName}");

            var i = 0;
            var initialData = Seeder.GetJsonStub<Hotel>("hotels", RELATIVE_FOLDER);

            initialData.Establishments.ForEach(r =>
            {
                i++;
                migrationBuilder.InsertData(tableName, columns, new object[] { i, r.EstablishmentId, r.Name, r.EstablishmentType, r.Stars, r.UserRating, r.UserRatingTitle, r.UserRatingCount, r.Location, r.Distance, r.MinCost
                    , r.ThumbnailUrl.Replace("https://i.t-rp.co.uk/", "assets/img/Thumbnails/")
                    , r.ImageUrl.Replace("https://i.t-rp.co.uk/", "assets/img/") });
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine($"DeleteData: {tableName}");

            migrationBuilder.Sql($@"DELETE FROM {tableName} WHERE {COLUMN_ID} BETWEEN 0 AND 1132");
        }
    }
}
