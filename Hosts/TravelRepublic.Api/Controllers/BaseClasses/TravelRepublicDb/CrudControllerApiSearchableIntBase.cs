using Eml.Contracts.Entities;
using Eml.ControllerBase;
using Eml.Contracts.Requests;
using Eml.Contracts.Responses;
using Eml.DataRepository.Contracts;
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
    public abstract class CrudControllerApiSearchableIntBase<T, TIndexRequest, TIndexResponse, TEditCreateRequest, TDetailsCreateResponse, TRepository>
        : CrudControllerApiSearchableBase<int, T, TIndexRequest, TIndexResponse, TEditCreateRequest,  TDetailsCreateResponse, Data.TravelRepublicDb, TRepository>
        where T : class, IEntityBase<int>, ISearchableName, ITravelRepublicDbEntity
        where TIndexRequest : IIndexRequest
        where TIndexResponse : ISearchResponse<T>
        where TEditCreateRequest : class, IEntityBase<int>
        where TDetailsCreateResponse : class, IEntityBase<int>
        where TRepository : IDataRepositoryBase<int, T, Data.TravelRepublicDb>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected CrudControllerApiSearchableIntBase(TRepository repository)
            : base(repository)
        {
        }

        protected string GetCurrentUser()
        {
            return "";
        }
    }
}
