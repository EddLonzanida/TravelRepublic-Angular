var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input, EventEmitter, Output } from '@angular/core';
import { HotelService } from '../hotel-service';
import { HotelSearchRequest } from '../requests/hotel-search-request';
import { HotelSearchResponse } from '../responses/hotel-search-response';
import { HotelSearchFilterResponse } from '../responses/hotel-search-filter-response';
import { HotelSorting } from '../dto/hotel-sorting.enum';
var SearchResultsComponent = /** @class */ (function () {
    function SearchResultsComponent(hotelService) {
        this.hotelService = hotelService;
        this.sortOrders = [];
        this.isInitialized = false;
        this.isLocalBusy = false; // fix to ExpressionChangedAfterItHasBeenCheckedError
        this.canShowPager = false;
        this.canFilter = true;
        this.hasErrors = false;
        // search parameters
        this.searchRequest = new HotelSearchRequest();
        this.costMinMax = [0, 0];
        // search responses
        this.searchResponse = new HotelSearchResponse();
        // Filters
        this.searchFilterResponse = new HotelSearchFilterResponse();
        this.isBusyChange = new EventEmitter();
        this.isStartSearchChange = new EventEmitter();
        this.restartSearch = new EventEmitter();
        this.refillSortOrderList();
    }
    SearchResultsComponent.prototype.search = function () {
        var _this = this;
        if (this.isLocalBusy) {
            console.warn('======BUSY Please wait...');
            return;
        }
        this.isLocalBusy = true;
        this.isBusy = true;
        this.isBusyChange.emit(true);
        this.searchRequest.name = this.searchName;
        this.searchRequest.costMin = this.costMinMax[0];
        this.searchRequest.costMax = this.costMinMax[1];
        this.hasErrors = false;
        this.hotelService.search(this.searchRequest)
            .then(function (r) {
            _this.searchResponse = r;
            _this.isStartSearch = false;
            _this.isStartSearchChange.emit(false);
            _this.isLocalBusy = false;
            _this.isBusy = false;
            _this.isBusyChange.emit(false);
            _this.canShowPager = _this.searchResponse.recordCount > _this.searchResponse.rowsPerPage;
        })
            .then(function (r) {
            if (_this.isInitialized) {
                return r;
            }
            _this.hotelService.getFilters(_this.searchRequest)
                .then(function (f) {
                _this.searchFilterResponse = f;
                _this.isInitialized = true;
                _this.costMinMax[0] = f.costMin;
                _this.costMinMax[1] = f.costMax;
                _this.searchFilterResponse.ratingMin = f.ratingMin * 10;
                _this.searchFilterResponse.ratingMax = f.ratingMax * 10;
                _this.searchRequest.userRating = f.ratingMin;
                if (_this.searchFilterResponse.starFilters[0]) {
                    _this.searchRequest.star = _this.searchFilterResponse.starFilters[0].star;
                    _this.oldStar = _this.searchRequest.star;
                }
                _this.canFilter = _this.searchResponse.recordCount > 1;
                _this.oldCostMin = _this.costMinMax[0];
                _this.oldCostMax = _this.costMinMax[1];
                _this.oldUserRating = _this.searchRequest.userRating;
            });
        })
            .catch(function (e) {
            _this.isStartSearch = false;
            _this.isStartSearchChange.emit(false);
            _this.isBusy = false;
            _this.isBusyChange.emit(false);
            _this.isLocalBusy = false;
            _this.isInitialized = false;
            _this.hasErrors = true;
        });
    };
    SearchResultsComponent.prototype.onSlideEnd = function (event, sender) {
        var hasChanges = false;
        if (sender === 'star') {
            if (event.value !== this.oldStar) {
                hasChanges = true;
                this.oldStar = event.value;
            }
        }
        else if (sender === 'costMinMax') {
            if (this.costMinMax[0] !== this.oldCostMin) {
                hasChanges = true;
                this.oldCostMin = this.costMinMax[0];
            }
            if (this.costMinMax[1] !== this.oldCostMax) {
                hasChanges = true;
                this.oldCostMax = this.costMinMax[1];
            }
        }
        else if (sender === 'userRating') {
            if (event.value !== this.oldUserRating) {
                hasChanges = true;
                this.oldUserRating = event.value;
            }
        }
        if (hasChanges) {
            this.searchRequest.page = 1;
            this.search();
        }
    };
    SearchResultsComponent.prototype.onSlideCancelled = function ($event) {
        this.searchRequest.star = this.searchFilterResponse.starFilters[0].star;
        this.oldStar = this.searchRequest.star;
        this.search();
    };
    SearchResultsComponent.prototype.onLazyLoad = function (event) {
        if (event && event.first > 0 && event.rows > 0) {
            this.searchRequest.page = (event.first / event.rows) + 1;
        }
        else {
            this.searchRequest.page = 1;
        }
        this.search();
    };
    SearchResultsComponent.prototype.restart = function () {
        this.restartSearch.emit();
    };
    SearchResultsComponent.prototype.selectEstablishment = function (event, item, overlaypanel) {
        this.selectedEstablishment = item;
        overlaypanel.toggle(event);
    };
    SearchResultsComponent.prototype.ngOnChanges = function (changes) {
        if (changes.isStartSearch && changes.isStartSearch.currentValue) {
            this.searchRequest.page = 1;
            this.search();
        }
        else {
        }
    };
    SearchResultsComponent.prototype.canResetUserRating = function () {
        return this.searchRequest.userRating !== this.searchFilterResponse.ratingMin;
    };
    SearchResultsComponent.prototype.resetUserRating = function () {
        this.searchRequest.userRating = this.searchFilterResponse.ratingMin;
        this.oldUserRating = this.searchRequest.userRating;
        this.searchRequest.page = 1;
        this.search();
    };
    SearchResultsComponent.prototype.refillSortOrderList = function () {
        this.sortOrders = [];
        this.sortOrders.push({ label: '-- Select Sorting --', value: null });
        this.sortOrders.push({ label: 'Name Desc', value: HotelSorting.Name });
        this.sortOrders.push({ label: 'Name Asc', value: HotelSorting.Default });
        this.sortOrders.push({ label: 'Farthest', value: HotelSorting.DistanceDesc });
        this.sortOrders.push({ label: 'Nearest', value: HotelSorting.DistanceAsc });
        this.sortOrders.push({ label: 'Stars - Highest', value: HotelSorting.StarsDesc });
        this.sortOrders.push({ label: 'Stars - Lowest', value: HotelSorting.StarsAsc });
        this.sortOrders.push({ label: 'Cost - Cheapest', value: HotelSorting.CostAsc });
        this.sortOrders.push({ label: 'Cost - Wealthiest', value: HotelSorting.CostDesc });
        this.sortOrders.push({ label: 'Rating - Highest', value: HotelSorting.UserRatingDesc });
        this.sortOrders.push({ label: 'Rating - Lowest', value: HotelSorting.UserRatingAsc });
    };
    SearchResultsComponent.prototype.isReady = function () {
        var isready = true;
        if (!this.searchResponse) {
            isready = false;
        }
        if (!this.searchFilterResponse) {
            isready = false;
        }
        if (!this.searchFilterResponse.starFilters) {
            return false;
        }
        if (this.searchFilterResponse.starFilters.length === 0) {
            return false;
        }
        if (this.searchRequest.star >= 0) { }
        else {
            return false;
        }
        return isready;
    };
    SearchResultsComponent.prototype.getStarIndex = function (star) {
        return this.searchFilterResponse.starFilters.findIndex(function (filter) { return filter.star >= star; });
    };
    SearchResultsComponent.prototype.canSort = function () {
        return this.isLocalBusy || !(this.searchResponse.recordCount > 1);
    };
    SearchResultsComponent.prototype.starClass = function () {
        var minStar = this.searchFilterResponse.starFilters[0].star;
        if (this.searchRequest.star === this.searchFilterResponse.starFilters[0].star) {
            return "disable-star" + minStar + " noreset";
        }
        return "disable-star" + minStar;
    };
    __decorate([
        Input(),
        __metadata("design:type", String)
    ], SearchResultsComponent.prototype, "searchName", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Boolean)
    ], SearchResultsComponent.prototype, "isBusy", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Boolean)
    ], SearchResultsComponent.prototype, "isStartSearch", void 0);
    __decorate([
        Output(),
        __metadata("design:type", Object)
    ], SearchResultsComponent.prototype, "isBusyChange", void 0);
    __decorate([
        Output(),
        __metadata("design:type", Object)
    ], SearchResultsComponent.prototype, "isStartSearchChange", void 0);
    __decorate([
        Output(),
        __metadata("design:type", EventEmitter)
    ], SearchResultsComponent.prototype, "restartSearch", void 0);
    SearchResultsComponent = __decorate([
        Component({
            selector: 'app-search-results',
            templateUrl: './search-results.component.html',
            styleUrls: ['./search-results.component.css']
        }),
        __metadata("design:paramtypes", [HotelService])
    ], SearchResultsComponent);
    return SearchResultsComponent;
}());
export { SearchResultsComponent };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/hotels/search-results/search-results.component.js.map