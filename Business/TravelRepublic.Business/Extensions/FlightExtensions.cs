﻿using Eml.Contracts.Services;
using System.Collections.Generic;
using System.Linq;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;
using TravelRepublic.Business.Common.Requests;

{
    public static class FlightExtensions
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

        }

        public static List<Flight> ArrivalBeforeDepartureDate(this IEnumerable<Flight> flights)
        {
            var filteredFlights = flights.Where(r => r.Segments.All(s => s.ArrivalDate < s.DepartureDate)).ToList();


        }

        public static List<Flight> DepartureBeforeCurrentDate(this IEnumerable<Flight> flights, IClockService clockService)
        {
            var now = clockService.Now();
            var filteredFlights = flights.Where(r => r.Segments.All(s => s.DepartureDate < now)).ToList();


        }
    }
}