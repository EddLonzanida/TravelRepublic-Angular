import { Injectable, Inject } from '@angular/core';
import { Http, Response, URLSearchParams } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { Establishment } from './dto/establishment';
import { HotelSearchRequest } from './requests/hotel-search-request';
import { HotelSearchRequestBase } from './requests/hotel-search-request-base';
import { HotelSearchResponse } from './responses/hotel-search-response';
import { HotelSearchFilterResponse } from './responses/hotel-search-filter-response';


@Injectable()
export class HotelService {
    private baseUrl: string;
    constructor(private http: Http, private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }
    getSuggestions(query: string) {
        const action = 'suggestions';
        return this.http.get(`${this.baseUrl}hotel/${action}?search=${query}`)
            .toPromise()
            .then(res => {
                return <string[]>res.json().Suggestions;
            })
            .then(data => { return data; })
            ;
    }
    search(request: HotelSearchRequest) {
        const cleanedRequest = this.cleanBaseParameters(request);
        cleanedRequest.Page = request.Page;
        cleanedRequest.Sorting = request.Sorting;
        if (cleanedRequest.Page > 1) { } else { cleanedRequest.Page = undefined; }
        if (cleanedRequest.Sorting > 0) { } else { cleanedRequest.Sorting = undefined; }
        const action = 'establishments';
        const config = { params: cleanedRequest }
        return this.http.get(`${this.baseUrl}hotel/${action}`, config)
            .toPromise()
            .then(res => {
                return <HotelSearchResponse>res.json();
            })
            .then(data => { return data; })
            ;
    }

    getFilters(request: HotelSearchRequestBase) {
        const cleanedRequest = this.cleanBaseParameters(request);
        cleanedRequest.Page = undefined;
        cleanedRequest.Sorting = undefined;
        const action = 'filter';
        const config = { params: cleanedRequest }
        return this.http.get(`${this.baseUrl}hotel/${action}`, config)
            .toPromise()
            .then(res => {
                return <HotelSearchFilterResponse>res.json();
            })
            .then(data => { return data; })
            ;
    }
    private cleanBaseParameters(request: HotelSearchRequestBase): HotelSearchRequest {
        const cleanedRequest = new HotelSearchRequest(request.Name, request.Star, request.UserRating, request.CostMin, request.CostMax);
        if (cleanedRequest.Name === '') { cleanedRequest.Name = undefined; }
        if (cleanedRequest.Star > 0) { } else { cleanedRequest.Star = undefined; }
        if (cleanedRequest.UserRating > 0) {
            cleanedRequest.UserRating = request.UserRating / 10;
        } else { cleanedRequest.UserRating = undefined; }
        if (cleanedRequest.CostMin > 0) { } else { cleanedRequest.CostMin = undefined; }
        if (cleanedRequest.CostMax > 0) { } else { cleanedRequest.CostMax = undefined; }

        return cleanedRequest;
    }
}
