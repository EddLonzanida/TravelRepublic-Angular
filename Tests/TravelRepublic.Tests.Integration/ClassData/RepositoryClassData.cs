using Eml.EntityBaseClasses;
using Eml.Extensions;
using System;
using System.Linq;
using TravelRepublic.Data.Contracts;
using Xunit;

namespace TravelRepublic.Tests.Integration.ClassData
{
    public class RepositoryClassData : TheoryData<Type>
    {
        private const string NAMESPACE = "TravelRepublic.Business.Common";

        public RepositoryClassData()
        {
            var dataRepositoryInt = typeof(IDataRepositoryInt<>);
            var concreteClasses = TypeExtensions.GetReferencingAssemblies(r => r.Name.Equals(NAMESPACE))
                .First()
                .GetClasses(type => !type.IsAbstract
                                    && typeof(EntityBaseInt).IsAssignableFrom(type)
                                    && !type.IsEnum
                                    && type.Namespace.EndsWith("Entities"))
                .Select(type =>
                {
                    Type[] typeArgs = { type };

                    return dataRepositoryInt.MakeGenericType(typeArgs);
                });

            foreach (var type in concreteClasses)
            {
                Add(type);
            }
        }
    }
}
