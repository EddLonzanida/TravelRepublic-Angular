using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eml.ControllerBase;
using Eml.Mediator.Contracts;
using TravelRepublic.Business.Requests;
using TravelRepublic.Business.Responses;

namespace TravelRepublic.ApiHost.Api.Hotel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HotelController : ControllerApiBase
    {
        [ImportingConstructor]
        protected HotelController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(HotelSearchResponse))]
        public async Task<HttpResponseMessage> Establishments(string name = "",
                                                            int star = 0,
                                                            double userRating = 0,
                                                            double costMin = 0,
                                                            double costMax = 0,
                                                            int page = 1,
                                                            int sorting = 0)
        {
            var request = new HotelSearchRequest(name, star, userRating, costMin, costMax, page, (eHotelSorting)sorting);
            var response = await mediator.GetAsync(request);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IList<string>))]
        public async Task<HttpResponseMessage> Suggestions(string search = "")
        {
            var request = new AutoCompleteRequest(search);
            var response = await mediator.GetAsync(request);

            return Request.CreateResponse(HttpStatusCode.OK, response.Suggestions);
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(HotelSearchFilterResponse))]
        public async Task<HttpResponseMessage> Filter(string name = "",
                                                    int star = 0,
                                                    double userRating = 0,
                                                    double costMin = 0,
                                                    double costMax = 0)
        {
            var request = new HotelSearchFilterRequest(name, star, userRating, costMin, costMax);
            var response = await mediator.GetAsync(request);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
        }
    }
}