using Eml.Contracts.Entities;
using Eml.Extensions;
using System;
using System.Collections.Generic;
using TravelRepublic.Infrastructure;
using TravelRepublic.Tests.Utils.BaseClasses;

namespace TravelRepublic.Tests.Utils.ClassData.Conventions
{
    public class InheritsFromEntityIntBaseClassData : ClassDataBase<Type>
    {
        private static List<Type> _entityClasses;

        public InheritsFromEntityIntBaseClassData()
            : base(() =>
            {
                if (_entityClasses != null) return _entityClasses;

                var assemblyPattern = new UniqueStringPattern(new List<string> { $"{Constants.ApplicationId}." }).Build();
                var assemblies = TypeExtensions.GetReferencingAssemblies(assemblyPattern);

                _entityClasses = assemblies
                                .GetClasses(type => typeof(IEntityBase<int>).IsAssignableFrom(type)
                                                    && !string.IsNullOrWhiteSpace(type.Namespace) 
                                                    && !type.Namespace.Contains("Test")
                                                    && !type.Namespace.Contains(".Dto.")
                                                    && !type.IsEnum);

                return _entityClasses;
            })
        {
        }
    }
}
