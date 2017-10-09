using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Business.Engines.RequestEngines;
using TravelRepublic.Business.Engines.Requests;
using TravelRepublic.Contracts.Repositories;

namespace TravelRepublic.Tests.Unit.RequestEngines
{
    [TestFixture]
    public class DuplicateContractAsyncEngineTests
    {
        private List<Contract> contractList;
        private IDataRepositoryBase<Contract> dataRepository;
        private DuplicateContractAsyncEngine engine;

        [SetUp]
        public void Setup()
        {
            contractList = new List<Contract>
            {
                new Contract
                {
                    Id = 1,
                    CompanyId = 1,
                    DateSigned = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false)),
                    RenewalDate = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false)),
                    EndDate = DateTime.Parse("18/01/2018", new CultureInfo("en-AU", false)),
                    Price = 100,
                    ContractType = "Master contact"
                },
                new Contract
                {
                    Id = 2,
                    CompanyId = 1,
                    DateSigned = DateTime.Parse("19/01/2018", new CultureInfo("en-AU", false)),
                    RenewalDate = DateTime.Parse("19/01/2018", new CultureInfo("en-AU", false)),
                    EndDate = DateTime.Parse("19/01/2019", new CultureInfo("en-AU", false)),
                    Price = 100,
                    ContractType = "Standard contact"
                }
            };
            dataRepository = Substitute.For<IDataRepositoryBase<Contract>>();
            dataRepository.GetAsync(Arg.Any<Expression<Func<Contract, bool>>>()).Returns(contractList.AsEnumerable());
            engine = new DuplicateContractAsyncEngine(dataRepository);
        }

        [Test]
        public async Task Engine_ShouldGetDuplicatesWhenEditing()
        {
            const string actionName = "Edit";
            const int companyId = 1;
            const int contractId = 1;
            var renewalDate = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false));
            var endDate = DateTime.Parse("18/01/2018", new CultureInfo("en-AU", false));
            var request = new DuplicateContractAsyncRequest(actionName, companyId, contractId, renewalDate, endDate);

            var result = await engine.GetAsync(request);

            await dataRepository.Received().GetAsync(Arg.Any<Expression<Func<Contract, bool>>>());
            var items = result.Contracts.ToList();
            items.Count.ShouldBe(1);
        }

        [Test]
        public async Task Engine_ShouldGetDuplicatesWhenCreating()
        {
            const string actionName = "Create";
            const int companyId = 1;
            const int contractId = -1;
            var renewalDate = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false));
            var endDate = DateTime.Parse("19/01/2019", new CultureInfo("en-AU", false));
            var request = new DuplicateContractAsyncRequest(actionName, companyId, contractId, renewalDate, endDate);

            var result = await engine.GetAsync(request);

            await dataRepository.Received().GetAsync(Arg.Any< Expression<Func<Contract, bool>>>());
            var items = result.Contracts.ToList();
            items.Count.ShouldBe(2);
        }
    }
}
