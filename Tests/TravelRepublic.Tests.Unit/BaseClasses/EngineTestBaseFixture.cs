using Eml.ConfigParser.Helpers;
using Eml.DataRepository;
using TravelRepublic.Tests.Unit.Stubs;
using System;
using Xunit;

namespace TravelRepublic.Tests.Unit.BaseClasses
{
    public class EngineTestBaseFixture : IDisposable
    {
        public const string COLLECTION_DEFINITION = "EngineTestBaseFixture CollectionDefinition";

        public static RepositoryStubs RepositoryStub { get; private set; }

        public static MainDbConnectionString MainDbConnectionString { get; private set; }

        public EngineTestBaseFixture()
        {
            var configuration = ConfigBuilder.GetConfiguration();

            MainDbConnectionString = new MainDbConnectionString(configuration);
            RepositoryStub = new RepositoryStubs();
        }

        public void Dispose()
        {
            RepositoryStub?.Dispose();
        }
    }

    [CollectionDefinition(EngineTestBaseFixture.COLLECTION_DEFINITION)]
    public class IntegrationTestDiFixtureCollectionDefinition : ICollectionFixture<EngineTestBaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
