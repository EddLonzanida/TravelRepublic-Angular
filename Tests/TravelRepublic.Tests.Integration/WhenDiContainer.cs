using Eml.ConfigParser;
using Shouldly;
using TravelRepublic.Business.Common.Entities.TravelRepublicDb;
using TravelRepublic.Business.Managers;
using TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts;
using TravelRepublic.Infrastructure.Configurations;
using TravelRepublic.Tests.Integration.BaseClasses;
using Xunit;

namespace TravelRepublic.Tests.Integration
{
    public class WhenDiContainer : IntegrationTestDiBase
    {
        [Fact]
        public void EstablishmentRepository_ShouldBeDiscoverable()
        {
            var sut = classFactory.GetExport<ITravelRepublicDataRepositorySoftDeleteInt<Establishment>>();

            sut.ShouldNotBeNull();
        }

        [Fact]
        public void IFlightBuilder_ShouldBeDiscoverable()
        {
            var sut = classFactory.GetExport<IFlightBuilder>();

            sut.ShouldNotBeNull();
        }

        [Fact]
        public void MainDbConnectionString_ShouldBeDiscoverable()
        {
            const string value = @"Server=(LocalDB)\MSSQLLocalDB;Database=TravelRepublicIntegrationTest;MultipleActiveResultSets=true;Integrated Security=True";

            var config = classFactory.GetExport<IConfigParserBase<string, TravelRepublicConnectionStringParser>>();

            config.ShouldNotBeNull();
            config.Value.ShouldBe(value);
        }

        //[Fact]
        //public void FlightController_ShouldBeDiscoverable()
        //{
        //    var sut = classFactory.GetExport<FlightController>();

        //    sut.ShouldNotBeNull();
        //}

        //[Fact]
        //public void HotelController_ShouldBeDiscoverable()
        //{
        //    var sut = classFactory.GetExport<HotelController>();

        //    sut.ShouldNotBeNull();
        //}
    }
}
