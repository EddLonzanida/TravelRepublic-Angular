using System.Collections.Generic;
using System.Linq;
using TravelRepublic.Business.Common.Dto;

namespace TravelRepublic.Tests.Unit.RequestEngines
{
    public static class FlightTestExtensions
    {
        public static List<Segment> Flatten(this IList<Flight> flights)
        {
            return flights.SelectMany(f => f.Segments.Select(s => s)).ToList();
        }

        public static List<Segment> Flatten(this Flight flight)
        {
            return flight.Segments.Select(s => s).ToList();
        }

        public static bool Exists(this Flight flight, List<Segment> segments)
        {
            var target = flight.Flatten();
            return target.All(s => segments.Any(r => r.ArrivalDate == s.ArrivalDate &&
                                                     r.DepartureDate == s.DepartureDate));
        }
    }
}
