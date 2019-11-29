using Eml.ClassFactory.Contracts;
using Eml.ConfigParser.Helpers;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.Extensions;
using Eml.Mef;
using TravelRepublic.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.Data;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    public class IntegrationTestDbFixture : IDisposable
    {
        public const string COLLECTION_DEFINITION = "IntegrationTestDbFixture CollectionDefinition";

        public const string APP_PREFIX = Constants.ApplicationId;

        public static IClassFactory ClassFactory { get; private set; }

        public IntegrationTestDbFixture()
        {
            var loggerFactory = new LoggerFactory();
            var configuration = ConfigBuilder.GetConfiguration()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
                
            ConnectionStrings.SetOneTime(configuration);
            ApplicationSettings.SetOneTime(configuration);

            var instanceRegistrations = new List<Func<ContainerConfiguration, ExportDescriptorProvider>>
            {
                r => r.WithInstance(loggerFactory),
                r => r.WithInstance(configuration)
            };

            ClassFactory = Bootstrapper.Init(APP_PREFIX, instanceRegistrations);

            var migrator = GetTestDbMigration();

            if (migrator == null)
            {
                throw new NoNullAllowedException("dbMigration not found..");
            }

            migrator.Execute();
        }

        public void Dispose()
        {
            Eml.Mef.ClassFactory.Dispose(ClassFactory);
        }

        private static IMigrator GetTestDbMigration()
        {
            return ClassFactory.GetMigrator(DbNames.TravelRepublic);
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
