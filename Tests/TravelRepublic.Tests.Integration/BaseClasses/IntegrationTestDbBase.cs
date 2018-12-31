using Eml.Mediator.Contracts;
using Eml.ClassFactory.Contracts;
using Xunit;
using Eml.Extensions;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    [Collection(IntegrationTestDbFixture.COLLECTION_DEFINITION)]
    public abstract class IntegrationTestDbBase
    {
        protected readonly IMediator mediator;

        protected readonly IClassFactory classFactory;

        protected IntegrationTestDbBase()
        {
            classFactory = IntegrationTestDbFixture.ClassFactory;

            classFactory.CheckNotNull("classFactory");

            mediator = classFactory.GetExport<IMediator>();
        }
    }
}
