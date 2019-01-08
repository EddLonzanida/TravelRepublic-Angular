import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable, of, throwError } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";

import { SearchResponse } from "../responses/search-response";

@Injectable(({ providedIn: "root" }) as any)
export class SearchService {
  private readonly baseUrl: string;

  constructor(private readonly httpClient: HttpClient, @Inject("BASE_URL") baseUrl: string) {

    this.baseUrl = baseUrl;

  }

  getSuggestions(controller: string, query: string) {

    const action = "suggestions";
    const route = `${controller}/${action}`;
    const param = { search: query };

    return this.request<string[]>("getSuggestions", route, param);
  }

  search<TRequest, TResponse>(route: string, request: TRequest) {

    const config = { params: request }

    return this.request<SearchResponse<TResponse>>("search", route, config);

  }

  request<TResponse>(operation: string, route: string, params?: any): Observable<TResponse> {

    const httpParams = this.toHttpParams(params);
    const config = { params: httpParams }
    const url = `${this.baseUrl}${route}`;

    return this.httpClient.get<TResponse>(url, config).pipe(

      catchError(this.handleError<TResponse>(operation, {} as TResponse))
      
    );
  }


  private toHttpParams(obj: Object): HttpParams {

    let params = new HttpParams();

    if (!obj) return params;

    for (const key in obj) {

      if (obj.hasOwnProperty(key)) {

        const val = obj[key];

        if (val !== null && val !== undefined) {

          params = params.append(key, val.toString());

        }
      }
    }
    return params;
  }

  private log(message: string) {

    console.log(message);

  }


  private handleError<T>(operation = "operation", result?: T) {

    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      // console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      // return of(result as T);
      return throwError(error);

    };
  }
}
