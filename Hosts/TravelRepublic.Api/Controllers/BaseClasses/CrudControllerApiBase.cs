using Eml.Contracts.Entities;
using Eml.ControllerBase;
using Eml.Mediator.Contracts;
using TravelRepublic.Data;
using TravelRepublic.Data.Contracts;

namespace TravelRepublic.Api.Controllers.BaseClasses
{
    public abstract class CrudControllerApiBase<T, TRequest> : CrudControllerApiBase<int, T, TRequest, TravelRepublicDb, IDataRepositorySoftDeleteInt<T>>
        where T : class, IEntityBase<int>, ISearchableName, IEntitySoftdeletableBase
        where TRequest : class
    {
        protected CrudControllerApiBase(IDataRepositorySoftDeleteInt<T> repository) : base(repository)
        {
        }

        protected CrudControllerApiBase(IMediator mediator, IDataRepositorySoftDeleteInt<T> repository) : base(mediator, repository)
        {
        }
    }
}
