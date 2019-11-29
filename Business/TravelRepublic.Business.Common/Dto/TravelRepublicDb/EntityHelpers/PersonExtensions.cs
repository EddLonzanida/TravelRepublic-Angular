using TravelRepublic.Business.Common.Entities.TravelRepublicDb;

namespace TravelRepublic.Business.Common.Dto.TravelRepublicDb.EntityHelpers
{
    public static class PersonExtensions
    {
        public static Person ToEntity(this PersonEditCreateRequest dto)
        {
            return new Person
            {
                //Id = Guid.NewGuid(),
                //Name = dto.Name
            };
        }

        public static PersonDetailsCreateResponse ToDto(this Person entity)
        {
            return new PersonDetailsCreateResponse
            {
                //Id = Guid.NewGuid(),
                //Name = dto.Name
            };
        }
    }
}
