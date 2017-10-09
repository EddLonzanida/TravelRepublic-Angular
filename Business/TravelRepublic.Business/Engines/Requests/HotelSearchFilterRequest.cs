using Eml.Mediator.Contracts;
using TravelRepublic.Business.Engines.Responses;

namespace TravelRepublic.Business.Engines.Requests
{
    public class HotelSearchFilterRequest : HotelSearchRequestBase, IRequestAsync<HotelSearchFilterRequest, HotelSearchFilterResponse>
    {
        public HotelSearchFilterRequest(string name, int star, double userRating, double costMin, double costMax)
            : base(name, star, userRating, costMin, costMax)
        {
        }
    }
}
