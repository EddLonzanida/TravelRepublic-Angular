using Eml.Contracts.Entities;
using Eml.Contracts.Requests;
using Eml.Contracts.Responses;
using Eml.ControllerBase;
using Eml.DataRepository.MsSql.Contracts;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;
using TravelRepublic.Api.Helpers;
using TravelRepublic.Infrastructure.Contracts;

namespace TravelRepublic.Api.Controllers.BaseClasses.TravelRepublicDb
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
	[ApiConventionType(typeof(CustomApiConventions))]
    public abstract class CrudControllerApiSoftDeletableIntBase<T, TIndexRequest, TIndexResponse, TEditCreateRequest, TDetailsCreateResponse, TRepository>
        : CrudControllerApiSoftDeletableBase<int, T, TIndexRequest, TIndexResponse, TEditCreateRequest, TDetailsCreateResponse, Data.TravelRepublicDb, TRepository>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase, ITravelRepublicDbEntity
        where TIndexRequest : IIndexRequest, new()
        where TIndexResponse : ISearchResponse<T>
        where TEditCreateRequest : class, IEntityBase<int>
        where TDetailsCreateResponse : class, IEntityBase<int>
        where TRepository : IDataRepositoryMsSqlSoftDeleteBase<int, T, Data.TravelRepublicDb>
    {
        protected CrudControllerApiSoftDeletableIntBase(TRepository repository)
            : base(repository)
        {
        }
        protected CrudControllerApiSoftDeletableIntBase(IMediator mediator, TRepository repository)
            : base(mediator, repository)
        {
        }

        protected string GetCurrentUser()
        {
            return "";
        }

        protected override string GetDeletedBy()
        {
            return GetCurrentUser();
        }
    }
}
