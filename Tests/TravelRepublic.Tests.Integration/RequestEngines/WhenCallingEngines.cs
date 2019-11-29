﻿using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;
using TravelRepublic.Business.Common.Requests;
using TravelRepublic.Tests.Integration.BaseClasses;
using Xunit;

namespace TravelRepublic.Tests.Integration.RequestEngines
{
    public class WhenCallingEngines : IntegrationTestDbBase
    {
        //[Fact]
        //public async Task HotelSearchFilter_ShouldBeExecuted()
        //{
        //    var request = new EstablishmentIndexRequest();

        //    var sut = await mediator.GetAsync(EstablishmentIndexRequest.GetNormalValues(request));

        //    sut.RecordCount.ShouldBe(1132);
        //    sut.RowsPerPage.ShouldBe(10);
        //}

        [Fact]
        public async Task HotelSearch_ShouldBeExecuted()
        {
            var request = new HotelSearchFilterAsyncRequest();

            var sut = await mediator.GetAsync(HotelSearchFilterAsyncRequest.GetNormalValues(request));

            sut.CostMax.ShouldBe(6988);
            sut.CostMin.ShouldBe(206.15);
            sut.RatingMin.ShouldBe(0);
            sut.RatingMax.ShouldBe(10);
            sut.StarFilters.ToList().Count.ShouldBe(6);
        }

        [Fact]
        public void FlightBuilder_ShouldBeExecuted()
        {
            var request = new FlightBuilderRequest(eFlightFilter.None);

            var sut = mediator.Get(request);

            sut.Flights.Count.ShouldBe(6);
        }
    }
}
