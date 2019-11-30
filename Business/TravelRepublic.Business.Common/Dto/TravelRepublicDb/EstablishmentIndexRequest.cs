using Eml.Contracts.Requests;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
    public class EstablishmentIndexRequest : IndexRequest
    {
        public int Star { get; set; }

        public double UserRating { get; set; }

        public double CostMin { get; set; }

        public double CostMax { get; set; }

        public static EstablishmentIndexRequest GetNormalValues(EstablishmentIndexRequest dto)
        {
            if (dto == null)
            {
                return new EstablishmentIndexRequest { Search = string.Empty };
            }

            var page = dto.Page;
            var search = dto.Search;
            var star = dto.Star;
            var userRating = dto.UserRating;
            var costMin = dto.CostMin;
            var costMax = dto.CostMax;

            if (page < 0) page = 0;

            if (string.IsNullOrEmpty(search)) search = string.Empty;

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

            return new EstablishmentIndexRequest
            {
                Page = page,
                SortColumn = dto.SortColumn,
                IsDescending = dto.IsDescending,
                Search = search,
                HasParent = dto.HasParent,

                Star = star,
                UserRating = userRating,
                CostMin = costMin,
                CostMax = costMax
            };
        }
    }
}
