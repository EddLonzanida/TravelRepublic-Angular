import { Injectable } from '@angular/core';
import { HotelSearchRequest } from '../requests/hotel-search-request';
import { HotelSearchRequestBase } from '../requests/hotel-search-request-base';
import { HotelSearchResponse } from '../responses/hotel-search-response';
import { HotelSearchFilterResponse } from '../responses/hotel-search-filter-response';
import { SearchService } from '../../../shared/services/search.service';

@Injectable(({ providedIn: 'root' }) as any)
export class HotelService {
    constructor(private readonly searchService: SearchService) {
    }

    getSuggestions(query: string) {
        const route = 'hotel';

        return this.searchService.getSuggestions(route, query);
    }

    search(request: HotelSearchRequest) {
        const controller = 'hotel';
        const route = `${controller}`;
        const cleanedRequest = this.cleanBaseParameters(request);

        cleanedRequest.page = request.page;
        cleanedRequest.sortColumn = request.sortColumn;
        cleanedRequest.isDescending = request.isDescending;

        if (cleanedRequest.page > 1) { } else { cleanedRequest.page = null; }
        if (cleanedRequest.isDescending) { } else { cleanedRequest.isDescending = null; }

        return this.searchService.request<HotelSearchResponse>('HotelService.search', route, cleanedRequest);
    }

    getFilters(request: HotelSearchRequestBase) {
        const controller = 'hotel';
        const action = 'filters';
        const route = `${controller}/${action}`;
        const cleanedRequest = this.cleanBaseParameters(request);

        cleanedRequest.page = null;
        cleanedRequest.isDescending = null;

        return this.searchService.request<HotelSearchFilterResponse>('HotelService.getFilters', route, cleanedRequest);
    }

    private cleanBaseParameters(request: HotelSearchRequestBase): HotelSearchRequest {
        const cleanedRequest = new HotelSearchRequest(request.search, request.star, request.userRating, request.costMin, request.costMax);
        if (cleanedRequest.search === '') { cleanedRequest.search = null; }
        if (cleanedRequest.star > 0) { } else { cleanedRequest.star = null; }
        if (cleanedRequest.userRating > 0) {
            cleanedRequest.userRating = request.userRating / 10;
        } else { cleanedRequest.userRating = null; }
        if (cleanedRequest.costMin > 0) { } else { cleanedRequest.costMin = null; }
        if (cleanedRequest.costMax > 0) { } else { cleanedRequest.costMax = null; }

        return cleanedRequest;
    }
}
