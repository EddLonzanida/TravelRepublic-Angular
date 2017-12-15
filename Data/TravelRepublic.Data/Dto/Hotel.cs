using System.Collections.Generic;
using TravelRepublic.Business.Common.Entities;

namespace TravelRepublic.Data.Dto
{
    public class Hotel
    {
        public string AvailabilitySearchId { get; set; }

        public List<Establishment> Establishments { get; set; } = new List<Establishment>();
    }
}
