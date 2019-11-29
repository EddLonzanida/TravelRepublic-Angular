using System.Collections.Generic;
using TravelRepublic.Business.Common.Entities.TravelRepublicDb;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
    public class Hotel
    {
        public string AvailabilitySearchId { get; set; }

        public List<Establishment> Establishments { get; set; } = new List<Establishment>();
    }
}
