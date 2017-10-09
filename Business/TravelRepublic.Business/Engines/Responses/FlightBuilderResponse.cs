using System.Collections.Generic;
using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.Dto;

namespace TravelRepublic.Business.Engines.Responses
{
    public class FlightBuilderResponse : IResponse
    {
       public IList<Flight> Flights { get; private set; }

        public FlightBuilderResponse(IList<Flight> flights)
        {
            Flights = flights;
        }
    }
}
