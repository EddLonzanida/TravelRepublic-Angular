using Eml.Mediator.Contracts;
using TravelRepublic.Business.Responses;

namespace TravelRepublic.Business.Requests
{
    public class AutoCompleteRequest : IRequestAsync<AutoCompleteRequest, AutoCompleteResponse>
    {
        public string SearchTerm { get; }
          
        public AutoCompleteRequest(string ssearchTerm)
        {
            SearchTerm = ssearchTerm;
        }
    }
}
