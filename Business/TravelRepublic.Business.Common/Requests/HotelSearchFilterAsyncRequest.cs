using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.BaseClasses;
using TravelRepublic.Business.Common.Responses;

namespace TravelRepublic.Business.Common.Requests
{
    public class HotelSearchFilterAsyncRequest : HotelSearchRequestBase, IRequestAsync<HotelSearchFilterAsyncRequest, HotelSearchFilterResponse>
    {
        /// <summary>
        /// This request will be processed by <see cref="HotelSearchFilterEngine"/>.
        /// </summary>
        public HotelSearchFilterAsyncRequest(string name, int star, double userRating, double costMin, double costMax)
           : base(name, star, userRating, costMin, costMax)
        {
        }
    }
}
