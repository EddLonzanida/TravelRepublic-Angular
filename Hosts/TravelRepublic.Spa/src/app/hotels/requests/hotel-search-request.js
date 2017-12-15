var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
import { HotelSearchRequestBase } from './hotel-search-request-base';
var HotelSearchRequest = /** @class */ (function (_super) {
    __extends(HotelSearchRequest, _super);
    function HotelSearchRequest(name, star, userRating, costMin, costMax, page, sorting) {
        if (name === void 0) { name = ''; }
        if (star === void 0) { star = 0; }
        if (userRating === void 0) { userRating = 0; }
        if (costMin === void 0) { costMin = 0; }
        if (costMax === void 0) { costMax = 0; }
        if (page === void 0) { page = 1; }
        if (sorting === void 0) { sorting = 0; }
        var _this = _super.call(this, name, star, userRating, costMin, costMax) || this;
        _this.name = name;
        _this.star = star;
        _this.userRating = userRating;
        _this.costMin = costMin;
        _this.costMax = costMax;
        _this.page = page;
        _this.sorting = sorting;
        return _this;
    }
    return HotelSearchRequest;
}(HotelSearchRequestBase));
export { HotelSearchRequest };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/hotels/requests/hotel-search-request.js.map