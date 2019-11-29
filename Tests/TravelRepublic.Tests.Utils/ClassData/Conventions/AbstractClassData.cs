using Eml.Extensions;
using TravelRepublic.Infrastructure;
using TravelRepublic.Tests.Utils.BaseClasses;
using System;
using System.Collections.Generic;

namespace TravelRepublic.Tests.Utils.ClassData.Conventions
{
    public class AbstractClassData : ClassDataBase<Type>
    {
        private static List<Type> _abstractClasses;

        public AbstractClassData()
        : base(() =>
        {
            if (_abstractClasses != null) return _abstractClasses;

            var assemblyPattern = new UniqueStringPattern(new List<string> { $"{Constants.ApplicationId}." }).Build();
			var assemblies = TypeExtensions.GetReferencingAssemblies(assemblyPattern);

            _abstractClasses = assemblies.GetClasses(type => type.IsAbstract && !type.IsSealed, true);

            return _abstractClasses;
        }) 
        { }
    }
}
