using System;using System.Collections.Generic;using System.ComponentModel.Composition;using System.Net;using System.Net.Http;using System.Web.Http;using System.Web.Http.Description;using Eml.Mediator.Contracts;using TravelRepublic.ApiHost.Api.BaseClasses;using TravelRepublic.Business.Requests;namespace TravelRepublic.ApiHost.Api.Flight
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FlightController : ApiControllerBase
    {
        [ImportingConstructor]
        protected FlightController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IList<Business.Common.Dto.Flight>))]
        public HttpResponseMessage Segments(eFlightFilter searchCode)
        {
            var request = new FlightBuilderRequest(searchCode);
            var reponse = mediator.Get(request);

            return Request.CreateResponse(HttpStatusCode.OK, reponse.Flights);
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
        }
    }
}
