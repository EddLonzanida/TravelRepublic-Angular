using Eml.Extensions;
using Shouldly;
using System;
using Xunit;
using TravelRepublic.Tests.Utils.ClassData.Conventions;

namespace TravelRepublic.Tests.Unit.Conventions.BaseClasses
{
    public class AbstractClassTests
    {
        private const string BASE = "Base";

        [Theory]
        [ClassData(typeof(AbstractClassData))]
        public void ClassNamePostfix_ShouldBeBase(Type type)
        {
            var postFix = GetNamePostFix(type.Name);

            postFix.ShouldBe(BASE);
        }

        private static string GetNamePostFix(string typeName)
        {
            const string GENERICS = "`";

            return typeName.TrimRight(GENERICS).GetLast(BASE.Length);
        }
    }
}
