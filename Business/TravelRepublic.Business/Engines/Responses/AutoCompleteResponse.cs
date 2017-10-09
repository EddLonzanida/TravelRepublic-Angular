using System.Collections.Generic;
using Eml.Mediator.Contracts;

namespace TravelRepublic.Business.Engines.Responses
{
    public class AutoCompleteResponse : IResponse
    {
        public IEnumerable<string> Suggestions { get; }

        public AutoCompleteResponse(IEnumerable<string> suggestions)
        {
            Suggestions = suggestions;
        }
    }
}
