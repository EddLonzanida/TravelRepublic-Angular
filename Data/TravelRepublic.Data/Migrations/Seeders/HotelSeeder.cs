using System.Data.Entity.Migrations;
using Eml.DataRepository;
using Eml.DataRepository.Extensions;
using TravelRepublic.Data.Dto;

namespace TravelRepublic.Data.Migrations.Seeders
{
    public static class HotelSeeder
    {
        public static void Seed(TravelRepublicDb context, string relativeFolder)
        {

            Seeder.Execute("Customers", () =>
            {
                var intialData = Seeder.GetJsonStub<Hotel>("hotels", relativeFolder);

                intialData.Establishments.ForEach(r =>
                {
                    context.Establishments.AddOrUpdate(establishment => establishment.EstablishmentId, r);
                });

                context.DoSave("Customer");
            });
        }
    }
}
