using Eml.Mediator.Contracts;
using TravelRepublic.Business.Responses;

namespace TravelRepublic.Business.Requests
{
    public enum eFlightFilter
    {
        None = 0,
        TwoHoursWaitingTime = 1,
        ArrivalBeforeDepartureDate = 2,
        DepartureBeforeCurrentDate = 4,
        ArrivalBeforeDepartureDateTwoHoursWaitingTime = 3,
        DepartureBeforeCurrentDateTwoHoursWaitingTime = 5,
        DepartureBeforeCurrentDateArrivalBeforeDepartureDate = 6,
        DepartureBeforeCurrentDateArrivalBeforeDepartureDateTwoHoursWaitingTime = 7
    }

    public class FlightBuilderRequest : IRequest<FlightBuilderRequest, FlightBuilderResponse>
    {
        public eFlightFilter SearchCode { get; }

        public FlightBuilderRequest( eFlightFilter searchCode)
        {
            SearchCode = searchCode;
        }
    }
}