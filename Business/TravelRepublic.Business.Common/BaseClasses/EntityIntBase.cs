using Eml.Contracts.Entities;

namespace TravelRepublic.Business.Common.BaseClasses
{
    public abstract class EntityIntBase : IEntityBase<int>
    {
		public virtual int Id { get; set; }
    }
}