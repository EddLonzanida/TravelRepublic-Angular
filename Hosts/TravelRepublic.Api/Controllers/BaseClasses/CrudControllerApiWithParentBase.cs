using Eml.Contracts.Entities;
using Eml.ControllerBase;
using Eml.Mediator.Contracts;
using TravelRepublic.Data;
using TravelRepublic.Data.Contracts;

namespace TravelRepublic.Api.Controllers.BaseClasses
{
    public abstract class CrudControllerApiWithParentBase<T, TRequest> : CrudControllerApiWithParentBase<int, T, TRequest, TravelRepublicDb, IDataRepositorySoftDeleteInt<T>>
        where T : class, IEntityWithParentBase<int>, ISearchableName, IEntitySoftdeletableBase
        where TRequest : class
    {
        protected CrudControllerApiWithParentBase(IDataRepositorySoftDeleteInt<T> repository) : base(repository)
        {
        }

        protected CrudControllerApiWithParentBase(IMediator mediator, IDataRepositorySoftDeleteInt<T> repository) : base(mediator, repository)
        {
        }
    }
}
