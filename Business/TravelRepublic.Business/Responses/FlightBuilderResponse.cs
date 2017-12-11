using System.Collections.Generic;using Eml.Mediator.Contracts;using TravelRepublic.Business.Common.Dto;namespace TravelRepublic.Business.Responses
{
    public class FlightBuilderResponse : IResponse
    {
       public IList<Flight> Flights { get; }

        public FlightBuilderResponse(IList<Flight> flights)
        {
            Flights = flights;
        }
    }
}
