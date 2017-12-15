var HotelSearchResponse = /** @class */ (function () {
    function HotelSearchResponse(recordCount, rowsPerPage, establishments) {
        if (recordCount === void 0) { recordCount = 0; }
        if (rowsPerPage === void 0) { rowsPerPage = 0; }
        if (establishments === void 0) { establishments = []; }
        this.recordCount = recordCount;
        this.rowsPerPage = rowsPerPage;
        this.establishments = establishments;
    }
    return HotelSearchResponse;
}());
export { HotelSearchResponse };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/hotels/responses/hotel-search-response.js.map