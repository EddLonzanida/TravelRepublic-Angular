using TravelRepublic.Tests.Unit.Stubs;
using System;
using Eml.Mediator.Contracts;
using NSubstitute;
using Xunit;

namespace TravelRepublic.Tests.Unit.BaseClasses
{
    public class ControllerTestBaseFixture : IDisposable
    {
        public const string COLLECTION_DEFINITION = "ControllerTestBaseFixture CollectionDefinition";

        public static RepositoryStubs RepositoryStub { get; private set; }

        public static IMediator Mediator { get; private set; }

        public ControllerTestBaseFixture()
        {
            RepositoryStub = new RepositoryStubs();
            Mediator = Substitute.For<IMediator>();
        }

        public void Dispose()
        {
            RepositoryStub?.Dispose();
        }
    }

    [CollectionDefinition(ControllerTestBaseFixture.COLLECTION_DEFINITION)]
    public class ControllerTestBaseFixtureCollectionDefinition : ICollectionFixture<ControllerTestBaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
