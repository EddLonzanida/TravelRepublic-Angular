using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.Responses;

namespace TravelRepublic.Business.Common.Requests
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
        /// <summary>
        /// This request will be processed by <see cref="FlightBuilderEngine"/>.
        /// </summary>
        public eFlightFilter SearchCode { get; }

        public FlightBuilderRequest(eFlightFilter searchCode)
        {
            SearchCode = searchCode;
        }

    }
}
