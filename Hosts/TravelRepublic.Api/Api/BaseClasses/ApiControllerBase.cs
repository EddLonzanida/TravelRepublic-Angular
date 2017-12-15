using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Description;
using Eml.ExplicitDispose.Api;
using Eml.Mediator.Contracts;
using Eml.MefDependencyResolver.Api;

namespace TravelRepublic.ApiHost.Api.BaseClasses
{
    [ExplicitDispose]
    public abstract class ApiControllerBase : ApiController, IDisposeAware
    {
        protected readonly IMediator mediator;

        protected ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected void ValidateAuthorizedUser(string userRequested)
        {
            var userLoggedIn = User.Identity.Name;

            if (userLoggedIn != userRequested)
                throw new SecurityException("Attempting to access data for another user.");
        }

        protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        {
            HttpResponseMessage response = null;

            try
            {
                response = codeToExecute.Invoke();
            }
            catch (SecurityException ex)
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (FaultException<AuthorizationValidationException> ex)
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Ex: Disposables.Add(Concrete classes that implements IDisposable);
        /// </summary>
        /// <param name="disposables"></param>
        protected abstract void RegisterIDisposable(List<IDisposable> disposables);

        public List<IDisposable> Disposables { get; private set; } = new List<IDisposable>();

        [ApiExplorerSettings(IgnoreApi = true)]
        public void RegisterDisposables(List<IDisposable> disposables)
        {
            RegisterIDisposable(disposables);
        }

    }
}