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
                return <string[]>res.json().suggestions;
            })
            .then(data => { return data; })
            ;
    }
    search(request: HotelSearchRequest) {
        const cleanedRequest = this.cleanBaseParameters(request);
        cleanedRequest.page = request.page;
        cleanedRequest.sorting = request.sorting;
        if (cleanedRequest.page > 1) { } else { cleanedRequest.page = undefined; }
        if (cleanedRequest.sorting > 0) { } else { cleanedRequest.sorting = undefined; }
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
        cleanedRequest.page = undefined;
        cleanedRequest.sorting = undefined;
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
        const cleanedRequest = new HotelSearchRequest(request.name, request.star, request.userRating, request.costMin, request.costMax);
        if (cleanedRequest.name === '') { cleanedRequest.name = undefined; }
        if (cleanedRequest.star > 0) { } else { cleanedRequest.star = undefined; }
        if (cleanedRequest.userRating > 0) {
            cleanedRequest.userRating = request.userRating / 10;
        } else { cleanedRequest.userRating = undefined; }
        if (cleanedRequest.costMin > 0) { } else { cleanedRequest.costMin = undefined; }
        if (cleanedRequest.costMax > 0) { } else { cleanedRequest.costMax = undefined; }

        return cleanedRequest;
    }
}
