using System;
using Eml.DataRepository;
using Microsoft.EntityFrameworkCore.Migrations;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;

namespace TravelRepublic.DataMigration.TravelRepublicDbMigrations
{
    public partial class InitialSeed : Migration
    {
        private const string COLUMN_ID = "Id";
        private const string RELATIVE_FOLDER = "SampleDataSources";

        private readonly string tableName;
        private readonly string[] columns = { COLUMN_ID, "EstablishmentId", "Name", "EstablishmentType", "Stars", "UserRating", "UserRatingTitle", "UserRatingCount", "Location", "Distance", "MinCost", "ThumbnailUrl", "ImageUrl" };

        public InitialSeed()
        {
            tableName = "Establishments";
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine($"InsertData: {tableName}");

            var i = 0;
            var initialData = Seeder.GetJsonStub<Hotel>("hotels", RELATIVE_FOLDER);

            if (initialData == null)
            {
                throw new Exception("Cannot parse hotels.json");
            }

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
