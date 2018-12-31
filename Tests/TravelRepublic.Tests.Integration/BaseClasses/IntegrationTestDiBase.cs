using Eml.ClassFactory.Contracts;
using Eml.Extensions;
using Xunit;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    [Collection(IntegrationTestDiFixture.COLLECTION_DEFINITION)]
    public abstract class IntegrationTestDiBase
    {
        protected readonly IClassFactory classFactory;

        protected IntegrationTestDiBase()
        {
            classFactory = IntegrationTestDiFixture.ClassFactory;

            classFactory.CheckNotNull("classFactory");
        }
    }
}
