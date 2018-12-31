using System;
using System.Data;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using Xunit;
using Eml.ClassFactory.Contracts;
using Eml.Mef;
using Eml.ConfigParser.Helpers;
using Eml.DataRepository.Extensions;
using Eml.DataRepository.Attributes;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    public class IntegrationTestDbFixture : IDisposable
    {
        public const string COLLECTION_DEFINITION = "IntegrationTestDbFixture CollectionDefinition";

        public const string APP_PREFIX = "TravelRepublic";

        private const string DB_DIRECTORY = "DataBase";

        public static IClassFactory ClassFactory { get; private set; }

        private readonly IMigrator migrator;

        public IntegrationTestDbFixture()
        {
            var configuration = ConfigBuilder.GetConfiguration();

            ExportDescriptorProvider InstanceRegistration(ContainerConfiguration r) => r.WithInstance(configuration);

            ClassFactory = Bootstrapper.Init(APP_PREFIX, InstanceRegistration);

            migrator = GetTestDbMigration();

            if (migrator == null)
            {
                throw new NoNullAllowedException("dbMigration not found..");
            }

            migrator.Execute(DB_DIRECTORY, false);
        }

        public void Dispose()
        {
            //migrator?.DestroyDb();

            Eml.Mef.ClassFactory.Dispose(ClassFactory);
        }

        private static IMigrator GetTestDbMigration()
        {
            return ClassFactory.GetMigrator(Environments.PRODUCTION);
        }
    }

    [CollectionDefinition(IntegrationTestDbFixture.COLLECTION_DEFINITION)]
    public class IntegrationTestDbFixtureCollectionDefinition : ICollectionFixture<IntegrationTestDbFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
