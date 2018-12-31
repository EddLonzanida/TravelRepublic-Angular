using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.Responses;

namespace TravelRepublic.Business.Common.Requests
{
    public class AutoCompleteAsyncRequest : IRequestAsync<AutoCompleteAsyncRequest, AutoCompleteResponse>
    {
        public string SearchTerm { get; }
   
        /// <summary>
        /// This request will be processed by <see cref="AutoCompleteEngine"/>.
        /// </summary>
        public AutoCompleteAsyncRequest(string searchTerm)
        {
            SearchTerm = searchTerm;

        }
    }
}
