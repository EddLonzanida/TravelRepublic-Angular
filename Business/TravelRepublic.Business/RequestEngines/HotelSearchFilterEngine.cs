using Eml.DataRepository.Contracts;
using Eml.Mediator.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelRepublic.Business.Common.Dto;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Common.Requests;
using TravelRepublic.Business.Common.Responses;

namespace TravelRepublic.Business.RequestEngines
{
    public class HotelSearchFilterEngine : IRequestAsyncEngine<HotelSearchFilterAsyncRequest, HotelSearchFilterResponse>
    {
        private readonly IDataRepositorySoftDeleteInt<Establishment> repository;

        [ImportingConstructor]
        public HotelSearchFilterEngine(IDataRepositorySoftDeleteInt<Establishment> repository)
        {
            this.repository = repository;
        }

        public async Task<HotelSearchFilterResponse> GetAsync(HotelSearchFilterAsyncRequest request)
        {
            var name = request.Name.ToLower();
            var stars = request.Star;
            var userRating = request.UserRating;
            var costMin = request.CostMin;
            var costMax = request.CostMax;
            var dbSet = repository.DbSet;

            Expression<Func<Establishment, bool>> starFiltersWhereClause = r => (name == "" || r.Name.Contains(name))
                                                                                && r.UserRating >= userRating
                                                                                && r.MinCost >= costMin;
            Expression<Func<Establishment, bool>> ratingFilterWhereClause = r => (name == "" || r.Name.Contains(name))
                                                                                 && r.Stars >= stars
                                                                                 && r.MinCost >= costMin;
            if (costMax > costMin)
            {
                starFiltersWhereClause = r => (name == "" || r.Name.Contains(name))
                                              && r.UserRating >= userRating
                                              && r.MinCost >= costMin && r.MinCost <= costMax;

                ratingFilterWhereClause = r => (name == "" || r.Name.Contains(name))
                                            && r.Stars >= stars
                                            && r.MinCost >= costMin && r.MinCost <= costMax;
            }

            var starFilters = await dbSet.Where(starFiltersWhereClause)
                .GroupBy(r => new { r.Stars })
                .Select(g => new StarFilter { Star = g.Key.Stars, Cost = g.Min(m => m.MinCost) })
                .OrderByDescending(r => r.Cost)
                .ToListAsync();

            if (starFilters.Any()) starFilters.Last().IsCheapest = true;
            starFilters = starFilters
                .OrderBy(r => r.Star)
                .ToList();

            var ratingFilter = await dbSet
                .Where(ratingFilterWhereClause)
                .GroupBy(r => new { column = "tmp" })
                .Select(g => new { Minimum = g.Min(m => m.UserRating), Maximum = g.Max(m => m.UserRating) })
                .FirstOrDefaultAsync();

            var costFilter = await dbSet.Where(r => (name == "" || r.Name.Contains(name))
                                                    && r.Stars >= stars
                                                    && r.UserRating >= userRating)
                .GroupBy(r => new { column = "tmp" })
                .Select(g => new { Minimum = g.Min(m => m.MinCost), Maximum = g.Max(m => m.MinCost) })
                .FirstOrDefaultAsync();

            var ratingFilterMin = ratingFilter?.Minimum ?? 0;
            var ratingFilterMax = ratingFilter?.Maximum ?? 0;
            var costFilterMin = costFilter?.Minimum ?? 0;
            var costFilterMax = costFilter?.Maximum ?? 0;
            return new HotelSearchFilterResponse(starFilters, ratingFilterMin, ratingFilterMax, costFilterMin, costFilterMax);
        }

        public void Dispose()
        {
        }
    }
}
