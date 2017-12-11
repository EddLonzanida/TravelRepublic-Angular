using Eml.Mediator.Contracts;using NUnit.Framework;using Shouldly;using TravelRepublic.Business.Requests;using TravelRepublic.Business.Responses;using TravelRepublic.Tests.Integration.BaseClasses;

namespace TravelRepublic.Tests.Integration.RequestEngines
{
    public class WhenCallingEngines : IntegrationTestDbBase
    {
        [Test]
        public void AutoComplete_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IRequestAsyncEngine<AutoCompleteRequest, AutoCompleteResponse>>();
            exported.ShouldNotBeNull();
        }

        [Test]
        public void HotelSearchFilter_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IRequestAsyncEngine<HotelSearchFilterRequest, HotelSearchFilterResponse>>();
            exported.ShouldNotBeNull();
        }

        [Test]
        public void HotelSearch_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IRequestAsyncEngine<HotelSearchRequest, HotelSearchResponse>>();
            exported.ShouldNotBeNull();
        }

        [Test]
        public void FlightBuilder_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IRequestEngine<FlightBuilderRequest, FlightBuilderResponse>>();
            exported.ShouldNotBeNull();
        }
    }
}
