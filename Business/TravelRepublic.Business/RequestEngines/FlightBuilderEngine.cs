using Eml.Contracts.Services;
using Eml.Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using TravelRepublic.Business.Common.Dto;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;
using TravelRepublic.Business.Common.Requests;
using TravelRepublic.Business.Common.Responses;
using TravelRepublic.Business.Extensions;
using TravelRepublic.Business.Managers;

namespace TravelRepublic.Business.RequestEngines
{
    public class FlightBuilderEngine : IRequestEngine<FlightBuilderRequest, FlightBuilderResponse>
    {
        private readonly IFlightBuilder _flightBuilder;
        private readonly IClockService _clockService;

        [ImportingConstructor]
        public FlightBuilderEngine(IFlightBuilder flightBuilder, IClockService clockService)
        {
            _flightBuilder = flightBuilder;
            _clockService = clockService;
        }

        public FlightBuilderResponse Get(FlightBuilderRequest request)
        {
            var flights = _flightBuilder.GetFlights();
            var filteredFlights = new List<Flight>();

            List<Flight> arrivalBeforeDepartureDate;
            List<Flight> twoHoursWaitingTime;
            List<Flight> departureBeforeCurrentDate;

            switch (request.SearchCode)
            {
                case eFlightFilter.None:
                    filteredFlights = flights.ToList();
                    break;

                case eFlightFilter.TwoHoursWaitingTime:
                    filteredFlights = flights.SegmentsWithTwoHoursWaitingTime();
                    break;

                case eFlightFilter.ArrivalBeforeDepartureDate:
                    filteredFlights = flights.ArrivalBeforeDepartureDate();
                    break;

                case eFlightFilter.DepartureBeforeCurrentDate:
                    filteredFlights = flights.DepartureBeforeCurrentDate(_clockService);
                    break;

                case eFlightFilter.ArrivalBeforeDepartureDateTwoHoursWaitingTime:
                    arrivalBeforeDepartureDate = flights.ArrivalBeforeDepartureDate();
                    twoHoursWaitingTime = flights.SegmentsWithTwoHoursWaitingTime();

                    if (arrivalBeforeDepartureDate.Any()) filteredFlights.AddRange(arrivalBeforeDepartureDate);
                    if (twoHoursWaitingTime.Any()) filteredFlights.AddRange(twoHoursWaitingTime);

                    break;
                case eFlightFilter.DepartureBeforeCurrentDateTwoHoursWaitingTime:
                    departureBeforeCurrentDate = flights.DepartureBeforeCurrentDate(_clockService);
                    twoHoursWaitingTime = flights.SegmentsWithTwoHoursWaitingTime();

                    if (departureBeforeCurrentDate.Any()) filteredFlights.AddRange(departureBeforeCurrentDate);
                    if (twoHoursWaitingTime.Any()) filteredFlights.AddRange(twoHoursWaitingTime);

                    break;
                case eFlightFilter.DepartureBeforeCurrentDateArrivalBeforeDepartureDate:
                    departureBeforeCurrentDate = flights.DepartureBeforeCurrentDate(_clockService);
                    arrivalBeforeDepartureDate = flights.ArrivalBeforeDepartureDate();

                    if (departureBeforeCurrentDate.Any()) filteredFlights.AddRange(departureBeforeCurrentDate);
                    if (arrivalBeforeDepartureDate.Any()) filteredFlights.AddRange(arrivalBeforeDepartureDate);

                    break;
                case eFlightFilter.DepartureBeforeCurrentDateArrivalBeforeDepartureDateTwoHoursWaitingTime:
                    arrivalBeforeDepartureDate = flights.ArrivalBeforeDepartureDate();
                    twoHoursWaitingTime = flights.SegmentsWithTwoHoursWaitingTime();
                    departureBeforeCurrentDate = flights.DepartureBeforeCurrentDate(_clockService);

                    if (departureBeforeCurrentDate.Any()) filteredFlights.AddRange(departureBeforeCurrentDate);
                    if (twoHoursWaitingTime.Any()) filteredFlights.AddRange(twoHoursWaitingTime);
                    if (arrivalBeforeDepartureDate.Any()) filteredFlights.AddRange(arrivalBeforeDepartureDate);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new FlightBuilderResponse(filteredFlights);
        }

        public void Dispose()
        {
        }
    }
}
