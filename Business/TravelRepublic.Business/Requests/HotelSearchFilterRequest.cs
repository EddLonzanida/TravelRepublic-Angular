using Eml.Mediator.Contracts;
using TravelRepublic.Business.Responses;

namespace TravelRepublic.Business.Requests
{
    public class HotelSearchFilterRequest : HotelSearchRequestBase, IRequestAsync<HotelSearchFilterRequest, HotelSearchFilterResponse>
    {
        public HotelSearchFilterRequest(string name, int star, double userRating, double costMin, double costMax)
            : base(name, star, userRating, costMin, costMax)
        {
        }
    }
}
