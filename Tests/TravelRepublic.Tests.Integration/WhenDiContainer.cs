using Eml.ConfigParser;
using Eml.DataRepository;
using Eml.DataRepository.Contracts;
using Shouldly;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Managers;
using TravelRepublic.Tests.Integration.BaseClasses;
using Xunit;

namespace TravelRepublic.Tests.Integration
{
    public class WhenDiContainer : IntegrationTestDiBase
    {
        [Fact]
        public void EstablishmentRepository_ShouldBeDiscoverable()
        {
            var sut = classFactory.GetExport<IDataRepositorySoftDeleteInt<Establishment>>();

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

            var config = classFactory.GetExport<IConfigBase<string, MainDbConnectionString>>();

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
