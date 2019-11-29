using Eml.Contracts.Entities;
using Eml.ControllerBase;
using Eml.Contracts.Requests;
using Eml.Contracts.Responses;
using Eml.DataRepository.MsSql.Contracts;
using TravelRepublic.Infrastructure.Contracts;
using TravelRepublic.Api.Helpers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace TravelRepublic.Api.Controllers.BaseClasses.TravelRepublicDb
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
	[ApiConventionType(typeof(CustomApiConventions))]
    public abstract class CrudControllerApiSoftDeletableIntBase<T, TIndexRequest, TIndexResponse, TEditCreateRequest, TDetailsCreateResponse, TRepository>
        : CrudControllerApiSoftDeletableBase<int, T, TIndexRequest, TIndexResponse, TEditCreateRequest, TDetailsCreateResponse, Data.TravelRepublicDb, TRepository>
        where T : class, IEntityBase<int>, ISearchableName, IEntitySoftdeletableBase, ITravelRepublicDbEntity
        where TIndexRequest : IIndexRequest
        where TIndexResponse : ISearchResponse<T>
        where TEditCreateRequest : class, IEntityBase<int>
        where TDetailsCreateResponse : class, IEntityBase<int>
        where TRepository : IDataRepositoryMsSqlSoftDeleteBase<int, T, Data.TravelRepublicDb>
    {
        protected CrudControllerApiSoftDeletableIntBase(TRepository repository)
            : base(repository)
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
