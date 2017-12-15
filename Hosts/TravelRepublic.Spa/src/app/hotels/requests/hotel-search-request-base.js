var HotelSearchRequestBase = /** @class */ (function () {
    function HotelSearchRequestBase(name, star, userRating, costMin, costMax) {
        if (name === void 0) { name = ''; }
        if (star === void 0) { star = 0; }
        if (userRating === void 0) { userRating = 0; }
        if (costMin === void 0) { costMin = 0; }
        if (costMax === void 0) { costMax = 0; }
        this.name = name;
        this.star = star;
        this.userRating = userRating;
        this.costMin = costMin;
        this.costMax = costMax;
    }
    return HotelSearchRequestBase;
}());
export { HotelSearchRequestBase };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/hotels/requests/hotel-search-request-base.js.map