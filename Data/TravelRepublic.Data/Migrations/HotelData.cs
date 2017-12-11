using System.Data.Entity.Migrations;
using Eml.DataRepository.Extensions;
using TravelRepublic.Data.Dto;

namespace TravelRepublic.Data.Migrations
{
    public static class HotelData
    {
        public static void Seed(TravelRepublicDb context, string relativeFolder)
        {
            var intialData = Eml.DataRepository.Seed.GetStub<Hotel>("hotels", relativeFolder);
            intialData.Establishments.ForEach(r =>
            {
                context.Establishments.AddOrUpdate(establishment => establishment.EstablishmentId, r);
            });
            context.DoSave("Customer");
        }
    }
}
