using System;
using System.Collections.Generic;
using Eml.Contracts.Entities;
using Eml.Contracts.Responses;
using TravelRepublic.Business.Common.Entities.TravelRepublicDb;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
    public class PersonIndexResponse : SearchResponse<Person>, IEntityBase<int>
    {
        public int Id { get; set; }

        public PersonIndexResponse(IEnumerable<Person> items, int recordCount, int rowsPerPage) 
            : base(items, recordCount, rowsPerPage)
        {
        }
    }
}
