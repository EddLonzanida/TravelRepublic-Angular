var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { HotelService } from './hotel-service';
import { SearchFlow } from './dto/search-flow.enum';
var HotelsComponent = /** @class */ (function () {
    function HotelsComponent(hotelService) {
        this.hotelService = hotelService;
        // search workflow
        this.searchFlow = SearchFlow.HOME;
        this.searchFlowEnum = SearchFlow;
        // busyIndicator
        this.isBusy = false;
        this.isStartSearch = false;
        this.searchSuggestions = [];
    }
    HotelsComponent.prototype.search = function () {
        this.searchFlow = SearchFlow.SEARCHING;
        this.isStartSearch = true;
        this.isBusy = true;
    };
    HotelsComponent.prototype.getSuggestions = function (event) {
        var _this = this;
        var query = event.query;
        this.hotelService.getSuggestions(query).then(function (suggestions) {
            _this.searchSuggestions = suggestions;
        })
            .catch(function (e) {
            console.error(e);
        });
    };
    HotelsComponent.prototype.restartSearch = function () {
        this.searchFlow = SearchFlow.HOME;
    };
    HotelsComponent = __decorate([
        Component({
            selector: 'app-hotels',
            templateUrl: './hotels.component.html',
            styleUrls: ['./hotels.component.css']
        }),
        __metadata("design:paramtypes", [HotelService])
    ], HotelsComponent);
    return HotelsComponent;
}());
export { HotelsComponent };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/hotels/hotels.component.js.map