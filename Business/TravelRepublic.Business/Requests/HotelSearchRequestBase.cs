using TravelRepublic.Contracts.Entities;namespace TravelRepublic.Business.Requests
{
    public abstract class HotelSearchRequestBase : IHotelSearchRequestBase
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
            if (Star < 0) Star = 0;
            if (UserRating < 0) UserRating = 0;
            if (CostMin > CostMax) CostMin = CostMax;
            if (CostMax < CostMin)
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
