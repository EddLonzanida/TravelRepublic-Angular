import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { HttpClient } from "@angular/common/http";
import "rxjs/add/operator/toPromise";
import { SearchResponse } from "../responses/search-response";

@Injectable()
export class SearchService {
    private readonly baseUrl: string;

    constructor(private readonly http: Http, private readonly httpClient: HttpClient, @Inject("BASE_URL") baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getSuggestions(controller: string, query: string) {
        const action = "suggestions";
        const route = `${controller}/${action}`;
        const param = {search: query};

        return this.request<string[]>(route, param);
    }

    search<TRequest, TResponse>(route: string, request: TRequest) {
        const config = { params: request }

        return this.request<SearchResponse<TResponse>>(route, config);
    }

    request<TResponse>(route: string, params?: any) {
        const config = { params: params }

        return this.http.get(`${this.baseUrl}${route}`, config)
            .toPromise()
            .then(res => {
                return res.json() as TResponse;
            })
            .then(data => { return data; });
    }
}
