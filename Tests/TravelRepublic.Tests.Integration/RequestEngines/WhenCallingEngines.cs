using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using TravelRepublic.Business.Requests;
using TravelRepublic.Tests.Integration.BaseClasses;

namespace TravelRepublic.Tests.Integration.RequestEngines
{
    public class WhenCallingEngines : IntegrationTestDbBase
    {
        [Test]
        public async Task  AutoComplete_ShouldBeExecuted()
        {
            var request = new AutoCompleteRequest("a");

            var sut = await mediator.GetAsync(request);

            sut.Suggestions.ToList().Count.ShouldBe(15);
        }

        [Test]
        public async Task HotelSearchFilter_ShouldBeExecuted()
        {
            var request = new HotelSearchRequest("", 0, 0, 0, 0, 1, eHotelSorting.None);

            var sut = await mediator.GetAsync(request);

            sut.RecordCount.ShouldBe(1132);
            sut.RowsPerPage.ShouldBe(8);
        }

        [Test]
        public async Task HotelSearch_ShouldBeExecuted()
        {
            var request = new HotelSearchFilterRequest("", 0, 0, 0, 0);
            
            var sut = await mediator.GetAsync(request);

            sut.CostMax.ShouldBe(6988);
            sut.CostMin.ShouldBe(206.15);
            sut.RatingMin.ShouldBe(0);
            sut.RatingMax.ShouldBe(10);
            sut.StarFilters.ToList().Count.ShouldBe(6);
        }

        [Test]
        public void FlightBuilder_ShouldBeExecuted()
        {
            var request = new FlightBuilderRequest(eFlightFilter.None);

            var sut = mediator.Get(request);

            sut.Flights.Count.ShouldBe(6);
        }
    }
}
