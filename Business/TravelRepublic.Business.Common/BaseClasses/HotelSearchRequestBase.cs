﻿namespace TravelRepublic.Business.Common.BaseClasses
{
    public abstract class HotelSearchRequestBase 
    {
        public string Name { get; }

        public int Star { get; }

        public double UserRating { get; }

        public double CostMin { get; }

        public double CostMax { get; }

        public HotelSearchRequestBase(string name, int star, double userRating, double costMin, double costMax)
        {
            Name = name;
            Star = star;
            UserRating = userRating;
            CostMin = costMin;
            CostMax = costMax;

            if (string.IsNullOrEmpty(Name)) Name = "";




            {
                var min = CostMin;
                var max = CostMax;
                //just swap
                CostMin = max;
                CostMax = min;
            }
        }
    }
}