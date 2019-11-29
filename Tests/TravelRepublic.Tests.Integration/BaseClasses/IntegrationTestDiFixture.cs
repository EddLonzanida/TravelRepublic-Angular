using Eml.ClassFactory.Contracts;
using Eml.Mef;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using Eml.ConfigParser.Helpers;
using Microsoft.Extensions.Configuration;
using Xunit;
using TravelRepublic.Infrastructure;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    public class IntegrationTestDiFixture : IDisposable
    {
        public const string COLLECTION_DEFINITION = "IntegrationTestDiFixture CollectionDefinition";

        private const string APP_PREFIX = Constants.ApplicationId;

        public static IClassFactory ClassFactory { get; private set; }

        public IntegrationTestDiFixture()
        {
            var loggerFactory = new LoggerFactory();
            var configuration = ConfigBuilder.GetConfiguration(Constants.CurrentEnvironment)
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
