using Eml.ClassFactory.Contracts;
using Eml.Mef;
using System;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using Eml.ConfigParser.Helpers;
using Xunit;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    public class IntegrationTestDiFixture : IDisposable
    {
        public const string COLLECTION_DEFINITION = "IntegrationTestDiFixture CollectionDefinition";

        public const string APP_PREFIX = "TravelRepublic";

        public static IClassFactory ClassFactory { get; private set; }

        public IntegrationTestDiFixture()
        {
            var configuration = ConfigBuilder.GetConfiguration();

            ExportDescriptorProvider InstanceRegistration(ContainerConfiguration r) => r.WithInstance(configuration);

            ClassFactory = Bootstrapper.Init(APP_PREFIX, InstanceRegistration);
        }

        public void Dispose()
        {
            Eml.Mef.ClassFactory.Dispose(ClassFactory);
        }
    }

    [CollectionDefinition(IntegrationTestDiFixture.COLLECTION_DEFINITION)]
    public class IntegrationTestDiFixtureCollectionDefinition : ICollectionFixture<IntegrationTestDiFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
