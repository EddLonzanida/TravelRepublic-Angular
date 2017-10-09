using Eml.Mediator.Contracts;
using TravelRepublic.Business.Engines.Responses;

namespace TravelRepublic.Business.Engines.Requests
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
