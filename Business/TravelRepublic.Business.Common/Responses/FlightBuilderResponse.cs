using Eml.Mediator.Contracts;
using System.Collections.Generic;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;

namespace TravelRepublic.Business.Common.Responses
{
    public class FlightBuilderResponse : IResponse
    {
        public List<Flight> Flights { get; }

        public FlightBuilderResponse(List<Flight> flights)
        {
            Flights = flights;
        }
    }
}