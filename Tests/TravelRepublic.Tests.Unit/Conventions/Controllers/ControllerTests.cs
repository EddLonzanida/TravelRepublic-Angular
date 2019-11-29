using System;
using System.Composition;
using Eml.Extensions;
using Shouldly;
using TravelRepublic.Tests.Utils.ClassData.Conventions;
using Xunit;
    
    namespace TravelRepublic.Tests.Unit.Conventions.Controllers
    {
        public class ControllerTests
        {
            [Theory]
            [ClassData(typeof(ControllerClassData))]
            public void Controller_ShouldHaveExportAttributes(Type type)
            {
                var sut = type.GetClassAttribute<ExportAttribute>();
    
                sut.ShouldNotBeNull();
            }
        }
    }
    
