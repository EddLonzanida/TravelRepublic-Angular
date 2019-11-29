using System.Collections.Generic;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
    public class Airport
    {
        public IList<Flight> Flights { get; set; } = new List<Flight>();
    }
}
