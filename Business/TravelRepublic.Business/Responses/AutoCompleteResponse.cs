using System.Collections.Generic;using Eml.Mediator.Contracts;namespace TravelRepublic.Business.Responses
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
