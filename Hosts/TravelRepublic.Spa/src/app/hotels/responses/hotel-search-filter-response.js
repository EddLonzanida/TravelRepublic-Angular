var HotelSearchFilterResponse = /** @class */ (function () {
    function HotelSearchFilterResponse(starFilters, ratingMin, ratingMax, costMin, costMax) {
        if (starFilters === void 0) { starFilters = []; }
        if (ratingMin === void 0) { ratingMin = 0; }
        if (ratingMax === void 0) { ratingMax = 100; }
        if (costMin === void 0) { costMin = 0; }
        if (costMax === void 0) { costMax = 0; }
        this.starFilters = starFilters;
        this.ratingMin = ratingMin;
        this.ratingMax = ratingMax;
        this.costMin = costMin;
        this.costMax = costMax;
    }
    return HotelSearchFilterResponse;
}());
export { HotelSearchFilterResponse };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/hotels/responses/hotel-search-filter-response.js.map