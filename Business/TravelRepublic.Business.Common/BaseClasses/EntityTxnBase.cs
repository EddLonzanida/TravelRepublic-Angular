using Eml.Contracts.Entities;
using Eml.EntityBaseClasses;

namespace TravelRepublic.Business.Common.BaseClasses
{
	public class EntityTxnBase<T> : EntityTxnBaseInt<T>
        where T : IEntityBase<int>
    {
    }
}
