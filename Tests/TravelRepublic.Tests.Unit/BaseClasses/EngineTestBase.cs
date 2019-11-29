using Eml.Mediator.Contracts;
using TravelRepublic.Tests.Unit.Stubs;
using TravelRepublic.Infrastructure.Configurations;
using System;
using Eml.Extensions;
using Xunit;


namespace TravelRepublic.Tests.Unit.BaseClasses
{
    [Collection(EngineTestBaseFixture.COLLECTION_DEFINITION)]
    public abstract class EngineTestBase<T1, T2> : IDisposable
        where T1 : IRequestAsync<T1, T2> where T2 : IResponse
    {
        protected IRequestAsyncEngine<T1, T2> engine;

        protected readonly RepositoryStubs repositoryStub;

        protected readonly TravelRepublicConnectionStringParser connectionString;

        protected EngineTestBase()
        {
            repositoryStub = EngineTestBaseFixture.RepositoryStub;
            connectionString = EngineTestBaseFixture.TravelRepublicConnectionString;

            repositoryStub.CheckNotNull("repositoryStub");
            connectionString.CheckNotNull("connectionString");
        }

        public void Dispose()
        {
            engine?.Dispose();
        }
    }
}
