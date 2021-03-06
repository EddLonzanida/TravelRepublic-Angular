﻿using Eml.Mediator.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;
using TravelRepublic.Business.Common.Entities.TravelRepublicDb;
using TravelRepublic.Business.Common.Requests;
using TravelRepublic.Business.Common.Responses;
using TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts;

namespace TravelRepublic.Business.RequestEngines
{
    public class HotelSearchFilterEngine : IRequestAsyncEngine<HotelFiltersAsyncRequest, HotelFiltersResponse>
    {
        private readonly ITravelRepublicDataRepositorySoftDeleteInt<Establishment> repository;

        [ImportingConstructor]
        public HotelSearchFilterEngine(ITravelRepublicDataRepositorySoftDeleteInt<Establishment> repository)
        {
            this.repository = repository;
        }

        public async Task<HotelFiltersResponse> GetAsync(HotelFiltersAsyncRequest request)
        {
            var name = request.Name.ToLower();
            var stars = request.Star;
            var userRating = request.UserRating;
            var costMin = request.CostMin;
            var costMax = request.CostMax;

            return await repository.ExecuteAsync(async dbSet =>
            {
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

                var unGroupedCostFilter = await dbSet.Where(r => (name == "" || r.Name.Contains(name))
                                                                 && r.Stars >= stars
                                                                 && r.UserRating >= userRating)
                    .GroupBy(r => new { column = "tmp" })
                    .Select(g => new { Minimum = g.Min(m => m.MinCost), Maximum = g.Max(m => m.MinCost) })
                    .ToListAsync();

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

                return new HotelFiltersResponse(starFilters, ratingFilterMin, ratingFilterMax, costFilterMin, costFilterMax);
            });
        }

        public void Dispose()
        {
        }
    }
}
