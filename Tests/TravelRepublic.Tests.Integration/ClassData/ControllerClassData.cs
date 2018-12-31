using System;
using System.Linq;
using Eml.Extensions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace TravelRepublic.Tests.Integration.ClassData
{
    public class ControllerClassData : TheoryData<Type>
    {
        private const string NAMESPACE = "TravelRepublic.Api";
       
		public ControllerClassData()
        {
            var concreteClasses = TypeExtensions.GetReferencingAssemblies(r => r.Name.StartsWith(NAMESPACE))
                .First()
                .GetClasses(type => !type.IsAbstract && typeof(Controller).IsAssignableFrom(type)); ;
            
			foreach (var type in concreteClasses)
            {
                Add(type);
            }
        }
    }
}
