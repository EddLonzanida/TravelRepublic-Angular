using System;
using Eml.Contracts.Entities;
using Eml.Contracts.Requests;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
	public class PersonIndexRequest : IndexRequest, IEntityBase<int>
    {
        public int Id { get; set; }
    }
}
