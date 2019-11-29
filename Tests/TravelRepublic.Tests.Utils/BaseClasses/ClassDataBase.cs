using System;
using System.Collections.Generic;
using Xunit;

namespace TravelRepublic.Tests.Utils.BaseClasses
{
    public abstract class ClassDataBase<T> : TheoryData<T>
		where T : class
    {
        protected ClassDataBase(Func<List<T>> getTypes)
        {
            var concreteClasses = getTypes();

            concreteClasses.ForEach(Add);
        }
    }
}
