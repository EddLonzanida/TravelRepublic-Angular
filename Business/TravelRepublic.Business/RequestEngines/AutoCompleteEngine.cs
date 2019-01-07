using Eml.Mediator.Contracts;
using System.Composition;
using System.Threading.Tasks;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Common.Requests;
using TravelRepublic.Business.Common.Responses;
using TravelRepublic.Data.Contracts;

namespace TravelRepublic.Business.RequestEngines
{
    public class AutoCompleteEngine : IRequestAsyncEngine<AutoCompleteAsyncRequest, AutoCompleteResponse>
    {
        private readonly IDataRepositorySoftDeleteInt<Establishment> repository;

        [ImportingConstructor]
        public AutoCompleteEngine(IDataRepositorySoftDeleteInt<Establishment> repository)
        {
            this.repository = repository;
        }

        public async Task<AutoCompleteResponse> GetAsync(AutoCompleteAsyncRequest request)
        {
            var searchTerm = request.SearchTerm; //) request.SearchTerm.ToLower();
            var suggestions = await repository
                .GetAutoCompleteIntellisenseAsync(r => searchTerm == "" || r.Name.Contains(searchTerm),
                r => r.Name);

            return new AutoCompleteResponse(suggestions);
        }

        public void Dispose()
        {
        }
    }
}
