using Eml.Contracts.Response;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using TravelRepublic.Api.Controllers.BaseClasses;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Common.Requests;
using TravelRepublic.Business.Common.Responses;
using TravelRepublic.Data;
using TravelRepublic.Data.Contracts;

namespace TravelRepublic.Api.Controllers
{
    [Export]
    public class HotelController : CrudControllerApiBase<Establishment, HotelSearchAsyncRequest>
    {
        [ImportingConstructor]
        public HotelController(IMediator mediator, IDataRepositorySoftDeleteInt<Establishment> repository)
            : base(mediator, repository)
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IActionResult> Index(int? page = 1, bool? desc = false, int? sortColumn = 0, string search = "")
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Establishments")]
        [Produces(typeof(HotelSearchResponse))]
        public async Task<IActionResult> Establishments(string name = "",
                                                            int star = 0,
                                                            double userRating = 0,
                                                            double costMin = 0,
                                                            double costMax = 0,
                                                            int page = 1,
                                                            int sorting = 0)
        {
            var request = new HotelSearchAsyncRequest(name, star, userRating, costMin, costMax, page, (eHotelSorting)sorting);
            var response = await DoIndexAsync(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("Suggestions")]
        [Produces(typeof(IList<string>))]
        public override async Task<IActionResult> Suggestions(string search = "")
        {
            var suggestions = await GetSuggestionsAsync(search);

            return Ok(suggestions);
        }

        [HttpGet]
        [Route("Filter")]
        [Produces(typeof(HotelSearchFilterResponse))]
        public async Task<IActionResult> Filter(string name = "",
                                                    int star = 0,
                                                    double userRating = 0,
                                                    double costMin = 0,
                                                    double costMax = 0)
        {
            var request = new HotelSearchFilterAsyncRequest(name, star, userRating, costMin, costMax);
            var response = await mediator.GetAsync(request);

            return Ok(response);
        }


        [Route("{id}")]
        [HttpGet]
        [Produces(typeof(Establishment))]
        public override async Task<IActionResult> Details(int id)
        {
            return await DoDetailsAsync(id);
        }

        [Route("{id}")]
        [HttpPut]
        public override async Task<IActionResult> Edit(int id, [FromBody]Establishment item)
        {
            return await DoEditAsync(id, item);
        }

        [HttpPost]
        [Produces(typeof(Establishment))]
        public override async Task<IActionResult> Create([FromBody]Establishment item)
        {
            return await DoCreateAsync(item);
        }

        [Route("{id}")]
        [HttpDelete]
        public override async Task<IActionResult> Delete(int id, string reason = "")
        {
            return await DoDeleteAsync(id, reason);
        }

        protected override async Task<List<string>> GetSuggestionsAsync(string search = "")
        {
            var request = new AutoCompleteAsyncRequest(search);
            var response = await mediator.GetAsync(request);

            return response.Suggestions.ToList();
        }

        protected override Func<IQueryable<Establishment>, IOrderedQueryable<Establishment>> GetOrderBy(int sortColumn, bool isDesc)
        {
            throw new NotImplementedException();
        }

        protected override async Task<ISearchResponse<Establishment>> GetItemsAsync(HotelSearchAsyncRequest request)
        {
            var response = await mediator.GetAsync(request);

            return response;
        }

        protected override async Task<Establishment> DeleteItemAsync(TravelRepublicDb db, int id, string reason)
        {
            return await repository.DeleteAsync(db, id);
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
        }
    }
}