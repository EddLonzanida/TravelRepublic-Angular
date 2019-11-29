using Shouldly;
using System;
using TravelRepublic.Tests.Integration.BaseClasses;
using TravelRepublic.Tests.Utils.ClassData.Conventions.TravelRepublicDb;
using Xunit;

namespace TravelRepublic.Tests.Integration.DataRepositories.TravelRepublicDb
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Theory]
        [ClassData(typeof(RepositoryIntClassData))]
        public void TravelRepublicRepositoryInt_ShouldBeDiscoverable(Type type)
        {
			classFactory.Container.TryGetExport(type, out var sut);

            sut.ShouldNotBeNull();
        }
    }
}
