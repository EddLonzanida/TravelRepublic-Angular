using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Eml.Contracts.Repositories;
using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Engines.Requests;
using TravelRepublic.Business.Engines.Responses;

namespace TravelRepublic.Business.Engines.RequestEngines
{
    public class AutoCompleteEngine : IRequestAsyncEngine<AutoCompleteRequest, AutoCompleteResponse>
    {
        private readonly IDataRepositorySoftDeleteInt<Establishment> repository;

        [ImportingConstructor]
        public AutoCompleteEngine(IDataRepositorySoftDeleteInt<Establishment> repository)
        {
            this.repository = repository;
        }

        public async Task<AutoCompleteResponse> GetAsync(AutoCompleteRequest request)
        {
            var searchTerm = request.SearchTerm.ToLower();
            var suggestions = await repository
                .GetAutoCompleteIntellisenseAsync(r => searchTerm == "" || r.Name.Contains(searchTerm), r => r.Name);

            return new AutoCompleteResponse(suggestions);
        }

        public void Dispose()
        {
            repository?.Dispose();
        }
    }
}
