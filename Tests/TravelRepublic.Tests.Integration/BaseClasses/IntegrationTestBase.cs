using Eml.ClassFactory.Contracts;
using Eml.Mef;
using NUnit.Framework;

namespace TravelRepublic.Tests.Integration.BaseClasses
{
    public abstract class IntegrationTestBase
    {
        protected IClassFactory classfactory;

        [OneTimeSetUp]
        public void Setup()
        {
            Bootstrapper.Init(new[] { "TravelRepublic*.dll" });
            classfactory = ClassFactory.MefContainer.GetExportedValue<IClassFactory>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            ClassFactory.MefContainer?.Dispose();
        }
    }
}
