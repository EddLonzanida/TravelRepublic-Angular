using Eml.Mediator.Contracts;
using NUnit.Framework;
using Shouldly;
using TravelRepublic.Business.Engines.Requests;
using TravelRepublic.Business.Engines.Responses;
using TravelRepublic.Tests.Integration.BaseClasses;

namespace TravelRepublic.Tests.Integration.RequestEngines
{
    [TestFixture]
    public class WhenCallingEngines : IntegrationTestBase
    {

        [Test]
        public void AutoComplete_ShouldBeDiscoverable()
        {
            var exported = classfactory.GetExport<IRequestAsyncEngine<AutoCompleteRequest, AutoCompleteResponse>>();
            exported.ShouldNotBeNull();
        }

        [Test]
        public void HotelSearchFilter_ShouldBeDiscoverable()
        {
            var exported = classfactory.GetExport<IRequestAsyncEngine<HotelSearchFilterRequest, HotelSearchFilterResponse>>();
            exported.ShouldNotBeNull();
        }

        [Test]
        public void HotelSearch_ShouldBeDiscoverable()
        {
            var exported = classfactory.GetExport<IRequestAsyncEngine<HotelSearchRequest, HotelSearchResponse>>();
            exported.ShouldNotBeNull();
        }

        [Test]
        public void FlightBuilder_ShouldBeDiscoverable()
        {
            var exported = classfactory.GetExport<IRequestEngine<FlightBuilderRequest, FlightBuilderResponse>>();
            exported.ShouldNotBeNull();
        }
    }
}
