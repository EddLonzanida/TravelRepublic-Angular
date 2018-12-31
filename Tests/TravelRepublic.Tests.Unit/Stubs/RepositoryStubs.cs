using System;
using System.Collections.Generic;

namespace TravelRepublic.Tests.Unit.Stubs
{
    public class RepositoryStubs : IDisposable
    {
        private readonly List<IDisposable> disposables;
        // public readonly IDataRepositorySoftDeleteInt<Company> CompanyRepository;
        // public readonly IDataRepositorySoftDeleteInt<ContactPerson> ContactPersonRepository;
        // public readonly IDataRepositorySoftDeleteInt<Contract> ContractRepository;
        // public readonly IDataRepositorySoftDeleteInt<PositionTitle> PositionTitleRepository;

        public RepositoryStubs()
        {
            // CompanyRepository = Substitute.For<IDataRepositorySoftDeleteInt<Company>>();
            // ContactPersonRepository = Substitute.For<IDataRepositorySoftDeleteInt<ContactPerson>>();
            // ContractRepository = Substitute.For<IDataRepositorySoftDeleteInt<Contract>>();
            // PositionTitleRepository = Substitute.For<IDataRepositorySoftDeleteInt<PositionTitle>>();

            disposables = new List<IDisposable>
            {
                // CompanyRepository,
                // ContactPersonRepository,
                // ContractRepository,
                // PositionTitleRepository
            };
        }

        public void Dispose()
        {
            disposables.ForEach(r => r?.Dispose());
        }
    }
}
