using Shouldly;
using System;
using TravelRepublic.Tests.Integration.BaseClasses;
using TravelRepublic.Tests.Integration.ClassData;
using Xunit;

namespace TravelRepublic.Tests.Integration.DataRepositories
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Theory]
        [ClassData(typeof(RepositoryClassData))]
        public void Repository_ShouldBeDiscoverable(Type type)
        {
			classFactory.Container.TryGetExport(type, out var sut);

            sut.ShouldNotBeNull();
        }
    }
}
