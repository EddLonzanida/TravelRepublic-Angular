using System.Collections.Generic;
using Eml.Contracts.Responses;
using TravelRepublic.Business.Common.Entities.TravelRepublicDb;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
    public class EstablishmentIndexResponse : SearchResponse<Establishment>
    {
        public EstablishmentIndexResponse(IEnumerable<Establishment> items, int recordCount, int rowsPerPage) 
            : base(items, recordCount, rowsPerPage)
        {
        }
    }
}
