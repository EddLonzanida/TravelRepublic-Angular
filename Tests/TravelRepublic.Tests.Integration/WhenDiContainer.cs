using Eml.ConfigParser;
using Eml.Contracts.Repositories;
using Eml.DataRepository;
using NUnit.Framework;
using Shouldly;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Managers;
using TravelRepublic.Tests.Integration.BaseClasses;

namespace TravelRepublic.Tests.Integration
{
    public class WhenDiContainer : IntegrationTestBase
    {
        [Test]
        public void EstablishmentRepository_ShouldBeDiscoverable()
        {
            var sut = classFactory.GetExport<IDataRepositorySoftDeleteInt<Establishment>>();

            sut.ShouldNotBeNull();
        }

        [Test]
        public void IFlightBuilder_ShouldBeDiscoverable()
        {
            var sut = classFactory.GetExport<IFlightBuilder>();

            sut.ShouldNotBeNull();
        }
        
        [Test]
        public void MainDbConnectionString_ShouldBeDiscoverable()
        {
            const string value = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=IntegrationTestDb;Integrated Security=True" ;
            
            var config = classFactory.GetExport<IConfigBase<string, MainDbConnectionString>>();

            config.ShouldNotBeNull();
            config.Value.ShouldBe(value);
        }
    }
}
