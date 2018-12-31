using Eml.DataRepository;
using Eml.Mediator.Contracts;
using TravelRepublic.Tests.Unit.Stubs;
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

        protected readonly MainDbConnectionString mainDbConnectionString;

        protected EngineTestBase()
        {
            repositoryStub = EngineTestBaseFixture.RepositoryStub;
            mainDbConnectionString = EngineTestBaseFixture.MainDbConnectionString;

            repositoryStub.CheckNotNull("repositoryStub");
            mainDbConnectionString.CheckNotNull("mainDbConnectionString");
        }

        public void Dispose()
        {
            engine?.Dispose();
        }
    }
}
