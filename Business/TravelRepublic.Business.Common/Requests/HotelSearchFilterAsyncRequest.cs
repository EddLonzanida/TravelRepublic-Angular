using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.Responses;

namespace TravelRepublic.Business.Common.Requests
{
    public class HotelSearchFilterAsyncRequest : IRequestAsync<HotelSearchFilterAsyncRequest, HotelSearchFilterResponse>
    {
        public string Name { get; set; }

        public int Star { get; set; }

        public double UserRating { get; set; }

        public double CostMin { get; set; }

        public double CostMax { get; set; }

        /// <summary>
        /// This request will be processed by HotelSearchFilterEngine.
        /// </summary>
        public HotelSearchFilterAsyncRequest()
        {
        }

        public static HotelSearchFilterAsyncRequest GetNormalValues(HotelSearchFilterAsyncRequest dto)
        {
            var name = dto.Name;
            var star = dto.Star;
            var userRating = dto.UserRating;
            var costMin = dto.CostMin;
            var costMax = dto.CostMax;

            if (string.IsNullOrEmpty(name)) name = "";

            if (star < 0) star = 0;

            if (userRating < 0) userRating = 0;

            if (costMin > costMax) costMin = costMax;

            if (costMax < costMin)
            {
                var min = costMin;
                var max = costMax;

                //just swap
                costMin = max;
                costMax = min;
            }

            return new HotelSearchFilterAsyncRequest
            {
                Name = name,
                Star = star,
                UserRating = userRating,
                CostMin = costMin,
                CostMax = costMax
            };
        }
    }
}
