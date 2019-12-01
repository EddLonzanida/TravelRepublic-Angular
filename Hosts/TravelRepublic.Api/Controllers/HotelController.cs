using Eml.Contracts.Responses;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelRepublic.Api.Controllers.BaseClasses.TravelRepublicDb;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb.EntityHelpers;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb.SortEnums;
using TravelRepublic.Business.Common.Entities.TravelRepublicDb;
using TravelRepublic.Business.Common.Requests;
using TravelRepublic.Business.Common.Responses;
using TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts;

namespace TravelRepublic.Api.Controllers
{
    [Export]
    public class HotelController : CrudControllerApiSoftDeletableIntBase<Establishment
        , EstablishmentIndexRequest
        , EstablishmentIndexResponse
        , EstablishmentEditCreateRequest
        , EstablishmentDetailsCreateResponse
        , ITravelRepublicDataRepositorySoftDeleteInt<Establishment>>
    {
        [ImportingConstructor]
        public HotelController(IMediator mediator, ITravelRepublicDataRepositorySoftDeleteInt<Establishment> repository)
            : base(mediator, repository)
        {
        }

        [HttpGet("Filters")]
        public async Task<ActionResult<HotelFiltersResponse>> GetFilters([FromQuery]HotelFiltersAsyncRequest request)
        {
            var filters = HotelFiltersAsyncRequest.GetNormalValues(request);

            var response = await mediator.GetAsync(filters);

            return Ok(response);
        }

        #region CRUD
        [HttpGet]
        public override async Task<ActionResult<EstablishmentIndexResponse>> Index([FromQuery]EstablishmentIndexRequest request)
        {
            return await DoIndexAsync(request);
        }

        [HttpGet("Suggestions")]
        public override async Task<ActionResult<List<string>>> Suggestions(string search = "")
        {
            return await DoSuggestionsAsync(search);
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<EstablishmentDetailsCreateResponse>> Details([FromRoute]int id)
        {
            return await DoDetailsAsync(id);
        }

        [HttpPost]
        public override async Task<ActionResult<EstablishmentDetailsCreateResponse>> Create([FromBody]EstablishmentEditCreateRequest request)
        {
            return await DoCreateAsync(request);
        }

        [HttpPut]
        public override async Task<ActionResult> Edit([FromBody]EstablishmentEditCreateRequest request)
        {
            return await DoEditAsync(request);
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete([FromRoute]int id, [FromBody]string reason)
        {
            return await DoDeleteAsync(id, reason);
        }
        #endregion // CRUD

        #region CRUD HELPERS
        protected override async Task<EstablishmentDetailsCreateResponse> EditItemAsync(EstablishmentEditCreateRequest request)
        {
            var entity = request.ToEntity();

            await repository.UpdateAsync(entity);

            return entity.ToDto();
        }

        protected override async Task<EstablishmentDetailsCreateResponse> AddItemAsync(EstablishmentEditCreateRequest request)
        {
            var entity = request.ToEntity();

            var newEntity = await repository.AddAsync(entity);

            return newEntity.ToDto();
        }

        protected override async Task<List<string>> GetSuggestionsAsync(string search = "")
        {
            search = string.IsNullOrWhiteSpace(search) ? string.Empty : search;

            return await repository
                .GetAutoCompleteIntellisenseAsync(r => search == "" || r.Name.Contains(search)
                    , r => r.Name);
        }

        protected override async Task<EstablishmentIndexResponse> GetItemsAsync(EstablishmentIndexRequest request)
        {
            request = EstablishmentIndexRequest.GetNormalValues(request);

            var search = request.Search.ToLower();
            var stars = request.Star;
            var userRating = request.UserRating;
            var costMin = request.CostMin;
            var costMax = request.CostMax;

            Expression<Func<Establishment, bool>> whereClause = r => (search == "" || r.Name.ToLower().Contains(search))
                                                                     && r.Stars >= stars
                                                                     && r.UserRating >= userRating;
            if (costMax > costMin)
            {
                whereClause = r => (search == "" || r.Name.ToLower().Contains(search))
                                   && r.Stars >= stars
                                   && r.UserRating >= userRating
                                   && r.MinCost >= costMin && r.MinCost <= costMax;
            }

            var items = await GetItemsAsync(request, whereClause);

            return new EstablishmentIndexResponse(items.Items, items.RecordCount, items.RowsPerPage);
        }

        protected async Task<SearchResponse<Establishment>> GetItemsAsync(EstablishmentIndexRequest request, Expression<Func<Establishment, bool>> whereClause)
        {
            var orderBy = GetOrderBy(request.SortColumn, request.IsDescending);
            var result = await repository.GetPagedListAsync(request.Page, whereClause, orderBy);
            var response = new SearchResponse<Establishment>(result.ToList(), result.TotalItemCount, result.PageSize);

            return response;
        }

        protected Func<IQueryable<Establishment>, IOrderedQueryable<Establishment>> GetOrderBy(string sortColumn, bool isDesc)
        {
            Func<IQueryable<Establishment>, IOrderedQueryable<Establishment>> orderBy;

            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                sortColumn = "Name"; //Default sort column
            }

            var eSortColumn = (eEstablishment)Enum.Parse(typeof(eEstablishment), sortColumn, true);

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eEstablishment.Name:

                        orderBy = r => r.OrderByDescending(x => x.Name);
                        break;

                    case eEstablishment.Distance:

                        orderBy = r => r.OrderByDescending(x => x.Distance);
                        break;

                    case eEstablishment.Stars:

                        orderBy = r => r.OrderByDescending(x => x.Stars);
                        break;

                    case eEstablishment.MinCost:

                        orderBy = r => r.OrderByDescending(x => x.MinCost);
                        break;

                    case eEstablishment.UserRating:

                        orderBy = r => r.OrderByDescending(x => x.UserRating);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException($"sortColumn: [{sortColumn}] is not supported.");
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eEstablishment.Name:

                    orderBy = r => r.OrderBy(x => x.Name);
                    break;

                case eEstablishment.Distance:

                    orderBy = r => r.OrderBy(x => x.Distance);
                    break;

                case eEstablishment.Stars:

                    orderBy = r => r.OrderBy(x => x.Stars);
                    break;

                case eEstablishment.MinCost:

                    orderBy = r => r.OrderBy(x => x.MinCost);
                    break;

                case eEstablishment.UserRating:

                    orderBy = r => r.OrderBy(x => x.UserRating);
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"sortColumn: [{sortColumn}] is not supported.");
            }

            return orderBy;
        }

        protected override async Task<EstablishmentDetailsCreateResponse> GetItemAsync(int id)
        {
            var item = await repository.GetAsync(id);

            return item?.ToDto();
        }
        #endregion // CRUD HELPERS
    }
}
