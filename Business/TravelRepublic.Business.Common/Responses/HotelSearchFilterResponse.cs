using Eml.Mediator.Contracts;
using System.Collections.Generic;
using TravelRepublic.Business.Common.Dto;

namespace TravelRepublic.Business.Common.Responses
{
    public class HotelSearchFilterResponse : IResponse
    {
        public IEnumerable<StarFilter> StarFilters { get; }

        public double RatingMin { get; }

        public double RatingMax { get; }

        public double CostMin { get; }

        public double CostMax { get; }

        public HotelSearchFilterResponse(IEnumerable<StarFilter> starFilters, double ratingMin, double ratingMax, double costMin, double costMax)
        {
            StarFilters = starFilters;
            RatingMin = ratingMin;
            RatingMax = ratingMax;
            CostMin = costMin;
            CostMax = costMax;
        }
    }
}