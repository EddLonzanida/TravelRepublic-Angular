using System;
using Eml.ClassFactory.Contracts;
using Eml.Contracts.Exceptions;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.Extensions;
using Eml.Mediator.Contracts;
using Eml.Mef;
using NUnit.Framework;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    [TestFixture]
    public abstract class IntegrationTestDbBase
    {
        protected IMediator mediator;

        protected IClassFactory classFactory;

        private IMigrator dbMigration;

        [OneTimeSetUp]
        public void Setup()
        {
            classFactory = Bootstrapper.Init("TravelRepublic*.dll");
            mediator = classFactory.GetExport<IMediator>();
            dbMigration = classFactory.GetMigrator(Environments.INTEGRATIONTEST);

            if (dbMigration == null)
            {
                throw new NotFoundException("dbMigration not found..");
            }

            Console.WriteLine("DestroyDb if any..");
            dbMigration.DestroyDb();

            Console.WriteLine("CreateDb..");
            dbMigration.CreateDb();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            dbMigration.DestroyDb();

            var container = classFactory.Container;
            classFactory = null;
            container.Dispose();
        }
    }
}
