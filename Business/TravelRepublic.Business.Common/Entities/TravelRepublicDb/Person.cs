using Eml.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using TravelRepublic.Business.Common.BaseClasses;
using TravelRepublic.Infrastructure.Contracts;

namespace TravelRepublic.Business.Common.Entities.TravelRepublicDb
{
    public class Person : PersonBase, ISearchableName, ITravelRepublicDbEntity
    {
        public string Name { get; set; }

        [NotMapped]
        public string SearchableName => $"{Name} {LastName}";
    }
}
