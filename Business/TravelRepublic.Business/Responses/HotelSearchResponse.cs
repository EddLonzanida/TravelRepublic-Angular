using System.Collections.Generic;using Eml.Mediator.Contracts;using TravelRepublic.Business.Common.Entities;namespace TravelRepublic.Business.Responses
{
    public class HotelSearchResponse : IResponse
    {
        public IEnumerable<Establishment> Establishments { get; }

        public int RecordCount { get; }

        public int RowsPerPage { get; }

        public HotelSearchResponse(IEnumerable<Establishment> establishments, int recordCount, int rowsPerPage)
        {
            Establishments = establishments;
            RecordCount = recordCount;
            RowsPerPage = rowsPerPage;
        }
    }
}
