using System;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
    public class Segment
    {
        public string SegmentType { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }
    }
}
