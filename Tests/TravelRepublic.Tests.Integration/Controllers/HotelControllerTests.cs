using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TravelRepublic.Api.Controllers;
using TravelRepublic.Business.Common.Dto.TravelRepublicDb;
using TravelRepublic.Business.Common.Entities.TravelRepublicDb;
using TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts;
using TravelRepublic.Infrastructure;
using TravelRepublic.Tests.Integration.BaseClasses;
using TravelRepublic.Tests.Utils.Extensions;
using Xunit;

namespace TravelRepublic.Tests.Integration.Controllers
{
    public class HotelControllerTests : IntegrationTestDbBase
    {
        [Fact]
        public async Task HotelController_ShouldGetItems()
        {
            var controller = classFactory.GetExport<HotelController>();

            var sut = await controller.Index(null);

            sut.ShouldNotBeNull();
            var response = sut.GetValue();
            response.ShouldNotBeNull();
            response.RecordCount.ShouldBe(1132);
            response.RowsPerPage.ShouldBe(10);
        }

        [Fact]
        public async Task HotelController_ShouldGetSuggestions()
        {
            var controller = classFactory.GetExport<HotelController>();

            var sut = await controller.Suggestions(null);

            sut.ShouldNotBeNull();
            var response = sut.GetValue();
            response.ShouldNotBeNull();
            response.Count.ShouldBe(ApplicationSettings.Config.IntellisenseCount);
        }

        [Fact]
        public async Task HotelController_ShouldPerformCrudOperations()
        {
            const string NAME = "IntegrationTest Name";
            const string NAME_UPDATED = "IntegrationTest Name Updated";
            const int DEFAULT_ID = default;

            //ensure no remnants of failed tests
            var repository = classFactory.GetExport<ITravelRepublicDataRepositorySoftDeleteInt<Establishment>>();
            var context = await repository.GetDb();

            using (var connection = context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    var sql = $"DELETE FROM Establishments WHERE [Name] LIKE '%{NAME}%';";

                    command.CommandText = sql;

                    await command.ExecuteNonQueryAsync();
                }
            }

            //CREATE
            var createEstablishmentRequest = new EstablishmentEditCreateRequest { Name = NAME };
            var controller = classFactory.GetExport<HotelController>();

            var sutDetailsCreate = await controller.Create(createEstablishmentRequest);

            sutDetailsCreate.GetStatusCode().ShouldBe(StatusCodes.Status201Created);

            var insertedItem = sutDetailsCreate.GetValue();
            var insertedItemId = insertedItem.Id; //store id for future use

            insertedItemId.ShouldNotBe(DEFAULT_ID);

            //DETAILS
            sutDetailsCreate = await controller.Details(insertedItemId);

            sutDetailsCreate.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            insertedItem = sutDetailsCreate.GetValue();
            insertedItem.ShouldNotBeNull();
            insertedItem.Name.ShouldBe(NAME);

            //EDIT
            var updateEstablishmentRequest = new EstablishmentEditCreateRequest
            {
                Id = insertedItemId,
                Name = NAME_UPDATED,
            };

            sutDetailsCreate = await controller.Edit(updateEstablishmentRequest);

            sutDetailsCreate.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            //SUGGESTIONS
            var sutSuggestions = await controller.Suggestions(string.Empty);

            sutSuggestions.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            var suggestions = sutSuggestions.GetValue();

            suggestions.ShouldNotBeNull();
            suggestions.Count.ShouldBeGreaterThan(0);

            //INDEX
            var sutIndex = await controller.Index(null);

            sutIndex.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            var pagedRows = sutIndex.GetValue();

            pagedRows.ShouldNotBeNull();
            pagedRows.Items.Count.ShouldBeGreaterThan(0);

            //DELETE
            var sutDelete = await controller.Delete(insertedItemId, "Integration Test");

            sutDelete.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            //Retest after Delete
            //INDEX search deleted row
            var indexRequest = new EstablishmentIndexRequest { Search = NAME_UPDATED };

            sutIndex = await controller.Index(indexRequest);

            sutIndex.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            pagedRows = sutIndex.GetValue();

            pagedRows.ShouldNotBeNull();

            var rowCountAfterDelete = pagedRows.Items.Count;

            rowCountAfterDelete.ShouldBe(0);

            // Validate the the row has been soft deleted
            sutDetailsCreate = await controller.Details(insertedItemId);

            sutDetailsCreate.GetStatusCode().ShouldBe(StatusCodes.Status404NotFound);

            var deletedItem = sutDetailsCreate.GetValue();

            deletedItem.ShouldBeNull();
        }
    }
}
