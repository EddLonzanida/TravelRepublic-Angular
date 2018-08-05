using System.Collections.Generic;using System.Linq;
using Eml.Contracts.Controllers;
using Eml.Mediator.Contracts;using TravelRepublic.Business.Common.Entities;namespace TravelRepublic.Business.Responses
{
    public class HotelSearchResponse : IResponse, ISearchResponse<Establishment>
    {
        public List<Establishment> Items { get; }

        public int RecordCount { get; }

        public int RowsPerPage { get; }

        public HotelSearchResponse(IEnumerable<Establishment> establishments, int recordCount, int rowsPerPage)
        {
            Items = establishments.ToList();
            RecordCount = recordCount;
            RowsPerPage = rowsPerPage;
        }
    }
}
