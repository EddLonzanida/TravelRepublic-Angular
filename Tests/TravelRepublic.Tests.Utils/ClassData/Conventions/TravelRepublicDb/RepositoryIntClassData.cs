using Eml.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelRepublic.Business.Common.BaseClasses;
using TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts;
using TravelRepublic.Infrastructure.Contracts;
using TravelRepublic.Tests.Utils.BaseClasses;

namespace TravelRepublic.Tests.Utils.ClassData.Conventions.TravelRepublicDb
{
    public class RepositoryIntClassData : ClassDataBase<Type>
    {
        private static List<Type> _repositories;

        public RepositoryIntClassData()
            : base(() =>
            {
                var dataRepositoryInt = typeof(ITravelRepublicDataRepositoryInt<>);

                return _repositories ?? (_repositories = typeof(EntityIntBase).Assembly
                           .GetClasses(type => typeof(ITravelRepublicDbEntity).IsAssignableFrom(type)
                                               && !type.IsAbstract
                                               && !type.IsEnum
                                               && type.Namespace != null
                                               && type.Namespace.Contains("Business.Common.Entities"))
                           .Select(type =>
                           {
                               Type[] typeArgs = { type };

                               return dataRepositoryInt.MakeGenericType(typeArgs);
                           })
                           .ToList());
            })
        { }
    }
}
