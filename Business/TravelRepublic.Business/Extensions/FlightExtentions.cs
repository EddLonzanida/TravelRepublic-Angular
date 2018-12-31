using Eml.Contracts.Services;using System.Collections.Generic;using System.Linq;using TravelRepublic.Business.Common.Dto;using TravelRepublic.Business.Common.Requests;
namespace TravelRepublic.Business.Extensions
{
    public static class FlightExtentions
    {
        public static List<Flight> SegmentsWithTwoHoursWaitingTime(this IEnumerable<Flight> flights)
        {
            var filteredFlights = flights.Where(r =>
                 {
                     var waitingTime = r.Segments
                         .Zip(r.Segments.Skip(1), (x, y) => (y.DepartureDate - x.ArrivalDate).Hours);
                     var totalHrs = waitingTime.Sum(x => x);

                     return totalHrs > 2;
                 })
                 .ToList();

            filteredFlights.ForEach(r => r.Segments.ForEach(x => x.SegmentType = nameof(eFlightFilter.TwoHoursWaitingTime)));
            return filteredFlights;
        }

        public static List<Flight> ArrivalBeforeDepartureDate(this IEnumerable<Flight> flights)
        {
            var filteredFlights = flights.Where(r => r.Segments.All(s => s.ArrivalDate < s.DepartureDate)).ToList();
            filteredFlights.ForEach(r => r.Segments.ForEach(x => x.SegmentType = nameof(eFlightFilter.ArrivalBeforeDepartureDate)));
            return filteredFlights;
        }

        public static List<Flight> DepartureBeforeCurrentDate(this IEnumerable<Flight> flights, IClockService clockService)
        {
            var now = clockService.Now();
            var filteredFlights = flights.Where(r => r.Segments.All(s => s.DepartureDate < now)).ToList();
            filteredFlights.ForEach(r => r.Segments.ForEach(x => x.SegmentType = nameof(eFlightFilter.DepartureBeforeCurrentDate)));
            return filteredFlights;
        }
    }
}
