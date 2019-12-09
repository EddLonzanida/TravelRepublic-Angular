using System.ComponentModel.DataAnnotations;
using TravelRepublic.Business.Common.BaseClasses;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb
{
    public class EstablishmentEditCreateRequest : EntitySoftDeletableIntBase
    {
        public int EstablishmentId { get; set; }

        public double Distance { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public int Stars { get; set; }

        public string EstablishmentType { get; set; }

        public double UserRating { get; set; }

        public string UserRatingTitle { get; set; }

        public int UserRatingCount { get; set; }

        [DataType(DataType.Url)]
        public string ImageUrl { get; set; }

        [DataType(DataType.Url)]
        public string ThumbnailUrl { get; set; }

        public double MinCost { get; set; }
    }
}
