﻿using Eml.Mediator.Contracts;
using TravelRepublic.Business.Engines.Responses;

namespace TravelRepublic.Business.Engines.Requests
{
    public enum eHotelSorting
    {
        None = 0,
        NameDesc = 1,
        DistanceAsc = 2,
        DistanceDesc = 4,
        StarsAsc = 3,
        StarsDesc = 5,
        CostAsc = 6,
        UserRatingAsc = 7,
        UserRatingDesc = 8,
        CostDesc = 9
    }

    public class HotelSearchRequest : HotelSearchRequestBase, IRequestAsync<HotelSearchRequest, HotelSearchResponse>
    {
        public int Page { get; }
        public eHotelSorting HotelSorting { get; }

        public HotelSearchRequest(string name, int star, double userRating, double costMin, double costMax, int page, eHotelSorting hotelSorting)
            : base(name, star, userRating, costMin, costMax)
        {
            Page = page;
            HotelSorting = hotelSorting;

            if (Page < 0) Page = 0;
            if ((int)HotelSorting > 9) HotelSorting = eHotelSorting.None;
        }
    }
}
