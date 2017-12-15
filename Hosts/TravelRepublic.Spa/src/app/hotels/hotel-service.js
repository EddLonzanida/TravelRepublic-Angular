var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { HotelSearchRequest } from './requests/hotel-search-request';
var HotelService = /** @class */ (function () {
    function HotelService(http, httpClient, baseUrl) {
        this.http = http;
        this.httpClient = httpClient;
        this.baseUrl = baseUrl;
    }
    HotelService.prototype.getSuggestions = function (query) {
        var action = 'suggestions';
        return this.http.get(this.baseUrl + "hotel/" + action + "?search=" + query)
            .toPromise()
            .then(function (res) {
            return res.json().suggestions;
        })
            .then(function (data) { return data; });
    };
    HotelService.prototype.search = function (request) {
        var cleanedRequest = this.cleanBaseParameters(request);
        cleanedRequest.page = request.page;
        cleanedRequest.sorting = request.sorting;
        if (cleanedRequest.page > 1) { }
        else {
            cleanedRequest.page = undefined;
        }
        if (cleanedRequest.sorting > 0) { }
        else {
            cleanedRequest.sorting = undefined;
        }
        var action = 'establishments';
        var config = { params: cleanedRequest };
        return this.http.get(this.baseUrl + "hotel/" + action, config)
            .toPromise()
            .then(function (res) {
            return res.json();
        })
            .then(function (data) { return data; });
    };
    HotelService.prototype.getFilters = function (request) {
        var cleanedRequest = this.cleanBaseParameters(request);
        cleanedRequest.page = undefined;
        cleanedRequest.sorting = undefined;
        var action = 'filter';
        var config = { params: cleanedRequest };
        return this.http.get(this.baseUrl + "hotel/" + action, config)
            .toPromise()
            .then(function (res) {
            return res.json();
        })
            .then(function (data) { return data; });
    };
    HotelService.prototype.cleanBaseParameters = function (request) {
        var cleanedRequest = new HotelSearchRequest(request.name, request.star, request.userRating, request.costMin, request.costMax);
        if (cleanedRequest.name === '') {
            cleanedRequest.name = undefined;
        }
        if (cleanedRequest.star > 0) { }
        else {
            cleanedRequest.star = undefined;
        }
        if (cleanedRequest.userRating > 0) {
            cleanedRequest.userRating = request.userRating / 10;
        }
        else {
            cleanedRequest.userRating = undefined;
        }
        if (cleanedRequest.costMin > 0) { }
        else {
            cleanedRequest.costMin = undefined;
        }
        if (cleanedRequest.costMax > 0) { }
        else {
            cleanedRequest.costMax = undefined;
        }
        return cleanedRequest;
    };
    HotelService = __decorate([
        Injectable(),
        __param(2, Inject('BASE_URL')),
        __metadata("design:paramtypes", [Http, HttpClient, String])
    ], HotelService);
    return HotelService;
}());
export { HotelService };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/hotels/hotel-service.js.map