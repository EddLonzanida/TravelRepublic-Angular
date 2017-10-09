using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Engines.Requests;
using TravelRepublic.Business.Engines.Responses;
using System.Linq;
using System.Linq.Expressions;
using Eml.Contracts.Repositories;
using Eml.DataRepository;

namespace TravelRepublic.Business.Engines.RequestEngines
{
    public class HotelSearchEngine : IRequestAsyncEngine<HotelSearchRequest, HotelSearchResponse>
    {
        private readonly IDataRepositorySoftDeleteInt<Establishment> repository;
        private readonly ITableMaintenance<Establishment> tableMaintenanceRepository;

        [ImportingConstructor]
        public HotelSearchEngine(IDataRepositorySoftDeleteInt<Establishment> repository)
        {
            this.repository = repository;
            tableMaintenanceRepository = (ITableMaintenance<Establishment>)repository;
        }

        public async Task<HotelSearchResponse> GetAsync(HotelSearchRequest request)
        {
            var name = request.Name.ToLower();
            var currentPage = request.Page;
            var stars = request.Star;
            var userRating = request.UserRating;
            var costMin = request.CostMin;
            var costMax = request.CostMax;
            var orderBy = GetOrderBy(request.HotelSorting);

            Expression<Func<Establishment, bool>> whereClause = r => (name == "" || r.Name.Contains(name))
                                                                     && r.Stars >= stars
                                                                     && r.UserRating >= userRating;
            if (costMax > costMin)
            {
                whereClause = r => (name == "" || r.Name.Contains(name))
                                   && r.Stars >= stars
                                   && r.UserRating >= userRating
                                   && r.MinCost >= costMin && r.MinCost <= costMax;
            }


            var establishments = await tableMaintenanceRepository
                .GetPagedListWithCountAsync(currentPage, whereClause,
                r => orderBy(r));

            return new HotelSearchResponse(establishments.ToList(), establishments.TotalItemCount, repository.PageSize);
        }

        public void Dispose()
        {
        }

        public static Func<IQueryable<Establishment>, IQueryable<Establishment>> GetOrderBy(eHotelSorting sorting)
        {
            Func<IQueryable<Establishment>, IQueryable<Establishment>> orderBy = r => r.OrderBy(x => x.Name);

            switch (sorting)
            {
                case eHotelSorting.None:
                    break;
                case eHotelSorting.NameDesc:
                    orderBy = r => r.OrderByDescending(x => x.Name);
                    break;
                case eHotelSorting.DistanceAsc:
                    orderBy = r => r.OrderBy(x => x.Distance);
                    break;
                case eHotelSorting.DistanceDesc:
                    orderBy = r => r.OrderByDescending(x => x.Distance);
                    break;
                case eHotelSorting.StarsAsc:
                    orderBy = r => r.OrderBy(x => x.Stars);
                    break;
                case eHotelSorting.StarsDesc:
                    orderBy = r => r.OrderByDescending(x => x.Stars);
                    break;
                case eHotelSorting.CostAsc:
                    orderBy = r => r.OrderBy(x => x.MinCost);
                    break;
                case eHotelSorting.UserRatingAsc:
                    orderBy = r => r.OrderBy(x => x.UserRating);
                    break;
                case eHotelSorting.UserRatingDesc:
                    orderBy = r => r.OrderByDescending(x => x.UserRating);
                    break;
                case eHotelSorting.CostDesc:
                    orderBy = r => r.OrderByDescending(x => x.MinCost);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return orderBy;
        }
    }
}
