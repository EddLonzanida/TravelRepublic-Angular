using Eml.Extensions;
using TravelRepublic.Tests.Integration.BaseClasses;
using TravelRepublic.Tests.Integration.ClassData;
using System;
using System.Composition;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace TravelRepublic.Tests.Integration.Controllers
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Theory]
        [ClassData(typeof(ControllerClassData))]
        public void Controller_ShouldBeExportable(Type type)
        {
            classFactory.Container.TryGetExport(type, out var sut);

            sut.ShouldNotBeNull();
        }

        [Theory]
        [ClassData(typeof(ControllerClassData))]
        public void Controller_ShouldHaveExportAttributes(Type type)
        {
            var sut = type.GetClassAttribute<ExportAttribute>();

            sut.ShouldNotBeNull();
        }
    }
}
