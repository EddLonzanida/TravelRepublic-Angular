using TravelRepublic.Business.Common.Entities.TravelRepublicDb;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb.EntityHelpers
{
    public static class EstablishmentExtensions
    {
        public static Establishment ToEntity(this EstablishmentEditCreateRequest dto)
        {
            return new Establishment
            {
                Id = dto.Id,
                EstablishmentId = dto.EstablishmentId,
                Distance = dto.Distance,
                Location = dto.Location,
                Name = dto.Name,
                Stars = dto.Stars,
                EstablishmentType = dto.EstablishmentType,
                UserRating = dto.UserRating,
                UserRatingTitle = dto.UserRatingTitle,
                UserRatingCount = dto.UserRatingCount,
                ImageUrl = dto.ImageUrl,
                ThumbnailUrl = dto.ThumbnailUrl,
                MinCost = dto.MinCost
            };
        }

        public static EstablishmentDetailsCreateResponse ToDto(this Establishment entity)
        {
            return new EstablishmentDetailsCreateResponse
            {
                Id = entity.Id,
                EstablishmentId = entity.EstablishmentId,
                Distance = entity.Distance,
                Location = entity.Location,
                Name = entity.Name,
                Stars = entity.Stars,
                EstablishmentType = entity.EstablishmentType,
                UserRating = entity.UserRating,
                UserRatingTitle = entity.UserRatingTitle,
                UserRatingCount = entity.UserRatingCount,
                ImageUrl = entity.ImageUrl,
                ThumbnailUrl = entity.ThumbnailUrl,
                MinCost = entity.MinCost
            };
        }
    }
}
