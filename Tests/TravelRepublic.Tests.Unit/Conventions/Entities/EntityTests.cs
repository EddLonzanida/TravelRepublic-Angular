using Shouldly;
using System;
using TravelRepublic.Business.Common.BaseClasses;
using TravelRepublic.Tests.Utils.ClassData.Conventions;
using Xunit;

namespace TravelRepublic.Tests.Unit.Conventions.Entities
{
    public class EntityTests
    {
        [Theory]
        [ClassData(typeof(InheritsFromEntityIntBaseClassData))]
        public void Entities_ShouldBeInBusinessCommon(Type type)
        {
            var isInBusinessCommonNamespace = type.Namespace.Contains(".Entities");

            isInBusinessCommonNamespace.ShouldBeTrue();
        }

        [Theory]
        [ClassData(typeof(BusinessCommonEntitiesClassData))]
        public void Entities_ShouldInheritFromEntityBase(Type type)
        {
            var isAssignableToEntityBase = typeof(EntityIntBase).IsAssignableFrom(type);

            isAssignableToEntityBase.ShouldBeTrue();
        }
    }
}
