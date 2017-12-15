using TravelRepublic.Contracts.Entities.Core;

namespace TravelRepublic.Contracts.Entities
{
    public interface IEstablishment : IEntityBase
    {
        int EstablishmentId { get; set; }

        double Distance { get; set; }

        string Location { get; set; }

        string Name { get; set; }

        double UserRating { get; set; }

        string UserRatingTitle { get; set; }

        int UserRatingCount { get; set; }

        string ImageUrl { get; set; }

        string ThumbnailUrl { get; set; }

        double MinCost { get; set; }

        int Stars { get; set; }

        string EstablishmentType { get; set; }
    }
}