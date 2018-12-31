using Eml.Mediator.Contracts;
using System.Collections.Generic;

namespace TravelRepublic.Business.Common.Responses
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