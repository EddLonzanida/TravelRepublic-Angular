using Eml.ClassFactory.Contracts;
using Eml.Mef;
using NUnit.Framework;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    [TestFixture]
    public abstract class IntegrationTestDiBase
    {
        protected IClassFactory classFactory;

        [OneTimeSetUp]
        public void Setup()
        {
            classFactory = Bootstrapper.Init("TravelRepublic*.dll");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            var container = classFactory.Container;
            classFactory = null;
            container.Dispose();
        }
    }
}
