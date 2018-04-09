using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Eml.DataRepository.Contracts;
using Eml.Mediator.Contracts;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Requests;
using TravelRepublic.Business.Responses;

namespace TravelRepublic.Business.RequestEngines
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
            var searchTerm = request.SearchTerm; //) request.SearchTerm.ToLower();
            var suggestions = await repository
                .GetAutoCompleteIntellisenseAsync(r => searchTerm == "" || r.Name.Contains(searchTerm), 
                r => r.OrderBy(s => s.Name),
                r => r.Name);

            return new AutoCompleteResponse(suggestions);
        }

        public void Dispose()
        {
            repository?.Dispose();
        }
    }
}
