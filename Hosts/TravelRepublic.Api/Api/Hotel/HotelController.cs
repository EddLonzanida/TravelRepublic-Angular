using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eml.Contracts.Controllers;
using Eml.ControllerBase;
using Eml.DataRepository.Contracts;
using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Requests;
using TravelRepublic.Business.Responses;

namespace TravelRepublic.ApiHost.Api.Hotel
{
    [RoutePrefix("Hotel")]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HotelController : CrudControllerApiBase<int, Establishment, HotelSearchRequest>
    {
        [ImportingConstructor]
        public HotelController(IMediator mediator, IDataRepositorySoftDeleteInt<Establishment> repository)
            : base(mediator, repository)
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IHttpActionResult> Index(int? page = 1, bool? desc = false, int? sortColumn = 0, string search = "")
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Establishments")]
        [ResponseType(typeof(HotelSearchResponse))]
        public async Task<IHttpActionResult> Establishments(string name = "",
                                                            int star = 0,
                                                            double userRating = 0,
                                                            double costMin = 0,
                                                            double costMax = 0,
                                                            int page = 1,
                                                            int sorting = 0)
        {
            var request = new HotelSearchRequest(name, star, userRating, costMin, costMax, page, (eHotelSorting)sorting);
            var response = await DoIndexAsync(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("Suggestions")]
        [ResponseType(typeof(IList<string>))]
        public override async Task<IHttpActionResult> Suggestions(string search = "")
        {
            var suggestions = await GetSuggestionsAsync(search);

            return Ok(suggestions);
        }

        [HttpGet]
        [Route("Filter")]
        [ResponseType(typeof(HotelSearchFilterResponse))]
        public async Task<IHttpActionResult> Filter(string name = "",
                                                    int star = 0,
                                                    double userRating = 0,
                                                    double costMin = 0,
                                                    double costMax = 0)
        {
            var request = new HotelSearchFilterRequest(name, star, userRating, costMin, costMax);
            var response = await mediator.GetAsync(request);

            return Ok(response);
        }


        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(Establishment))]
        public override async Task<IHttpActionResult> Details(int id)
        {
            return await DoDetailsAsync(id);
        }

        [Route("{id}")]
        [HttpPut]
        public override async Task<IHttpActionResult> Put(int id, [FromBody]Establishment item)
        {
            return await DoPutAsync(id, item);
        }

        [HttpPost]
        [ResponseType(typeof(Establishment))]
        public override async Task<IHttpActionResult> Post([FromBody]Establishment item)
        {
            return await DoPostAsync(item);
        }

        [Route("{id}")]
        [HttpDelete]
        public override async Task<IHttpActionResult> Delete(int id, string reason = "")
        {
            return await DoDeleteAsync(id, reason);
        }

        protected override async Task<List<string>> GetSuggestionsAsync(string search = "")
        {
            var request = new AutoCompleteRequest(search);
            var response = await mediator.GetAsync(request);

            return response.Suggestions.ToList();
        }

        protected override Func<IQueryable<Establishment>, IQueryable<Establishment>> GetOrderBy(int sortColumn, bool isDesc)
        {
            throw new NotImplementedException();
        }

        protected override async Task<ISearchResponse<Establishment>> GetItemsAsync(HotelSearchRequest request)
        {
            var response = await mediator.GetAsync(request);

            return response;
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
        }
    }
}