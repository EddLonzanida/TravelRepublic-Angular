using System;
using Eml.Contracts.Entities;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
    public class PersonEditCreateRequest : IEntityBase<int>
    {
        public int Id { get; set; }
    }
}
