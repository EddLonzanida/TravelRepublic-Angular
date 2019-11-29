using Eml.Extensions;
using Eml.Mediator.Contracts;
using TravelRepublic.Tests.Unit.Stubs;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace TravelRepublic.Tests.Unit.BaseClasses
{
    [Collection(ControllerTestBaseFixture.COLLECTION_DEFINITION)]
    public abstract class ControllerTestBase<T> : IDisposable
        where T : ControllerBase
    {
        protected readonly IMediator mediator;

        protected readonly RepositoryStubs repositoryStub;

        protected T controller;

        protected ControllerTestBase()
        {
            mediator = ControllerTestBaseFixture.Mediator;
            repositoryStub = ControllerTestBaseFixture.RepositoryStub;

            repositoryStub.CheckNotNull("mediator");
            repositoryStub.CheckNotNull("repositoryStub");
        }

        public void Dispose()
        {
        }
    }
}
