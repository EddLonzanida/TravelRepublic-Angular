using Eml.ControllerBase;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Composition;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;
using TravelRepublic.Business.Common.Requests;

namespace TravelRepublic.Api.Controllers
{
    [Export]
    public class FlightController : ControllerApiBase
    {
        [ImportingConstructor]
        public FlightController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        public ActionResult<List<Flight>> GetFlightSegments(eFlightFilter searchCode)
        {
            var request = new FlightBuilderRequest(searchCode);
            var response = mediator.Get(request);

            return Ok(response.Flights);
        }
    }
}
