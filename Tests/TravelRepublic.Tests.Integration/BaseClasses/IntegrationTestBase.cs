using Eml.ClassFactory.Contracts;
using Eml.Mef;
using NUnit.Framework;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    [TestFixture]
    public abstract class IntegrationTestBase
    {
        protected IClassFactory classFactory;

        [OneTimeSetUp]
        public void Setup()
        {
            Bootstrapper.Init("TravelRepublic*.dll");
            classFactory = ClassFactory.Get();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            ClassFactory.Dispose();
        }
    }
}
