function CreateFolder($path) {
    
    if (!(Test-Path $path)) {
        New-Item -ItemType Directory -Force -Path $path
    }
}

function GetAppName() {

    return (get-item $PSScriptRoot).Name.Replace(".Spa","")
}

function DeleteFile($path){
    
  if (Test-Path $path) { 
    try {
    
        Write-Host "Deleting $path.." -foreground "magenta" 
            Remove-Item -Path "$path" -Force -ErrorAction SilentlyContinue
            Write-Host " "
        
            return $true
        }
        catch {
            $_
            return $false
        }	
    }
    else {
        return $true
    }
}

function DeleteDirectory($path) {
    
    if (Test-Path $path) { 
        try {
    
            Write-Host "Deleting $path.." -foreground "magenta" 
            Remove-Item -Path "$path" -Force -Recurse -ErrorAction SilentlyContinue
            Write-Host " "
    
            return $true
        }
        catch {
    
            $_
            return $false
        }	
    }
    else {
        return $true
    }
}

function CreateLaunchJson($path) {
    
    $dest = "$path\.vscode"
    
    CreateFolder $dest

	#--
	$fn = "$dest\launch.json"
   
    DeleteFile $fn
   
    $code = 
'{
    "version": "0.2.0",
    "configurations": [
        {
            "type": "browser-preview",
            "request": "attach",
            "name": "Browser Preview: Attach",
            "webRoot": "${workspaceFolder}"
        },
        {
            "type": "browser-preview",
            "request": "launch",
            "name": "Browser Preview: Launch",
            "url": "http://localhost:4200",
            "webRoot": "${workspaceFolder}"
        }
    ]
}'

    $code | Set-Content $fn
}


function CreateSearchService($path) {
    
    $dest = "$path\src\app\shared\services"
	$fn = "$dest\search.service.ts"
   
    DeleteFile $fn
   
    $code = 
'import { Injectable } from "@angular/core";
import { HttpClient,HttpParams } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { SearchResponseBase } from "../responses/search-response-base";
import { appSettings } from "src/environments/environment";

@Injectable(({ providedIn: "root" }) as any)
export class SearchService {
    private readonly baseUrl: string;

    constructor(private readonly httpClient: HttpClient) {
    this.baseUrl = appSettings.apiRoot;
    }

    getSuggestions(controller: string, query: string) {
    const action = "suggestions";
    const route = `${controller}/${action}`;
    const param = { search: query };

    return this.request<string[]>("getSuggestions", route, param);
    }

    search<TRequest, TResponse>(route: string, request: TRequest) {
    return this.request<SearchResponseBase<TResponse>>("search", route, request);
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
}'

    $code | Set-Content $fn
}

function CreateSearchRequestBase($path) {
    
    $dest = "$path\src\app\shared\requests"
	$fn = "$dest\search-request-base.ts"
    DeleteFile $fn
   
    $code = 
'export class SearchRequestBase {
    constructor(
        public search = "",
        public page = 0,
        public sortColumn = 0,
        public isDescending = false) {
    }
}'

    $code | Set-Content $fn
}

function CreateSearchResponseBase($path) {
    
    $dest = "$path\src\app\shared\responses"
	$fn = "$dest\search-response-base.ts"
   
    DeleteFile $fn
   
    $code = 
'export class SearchResponseBase<T> {
    constructor(
        public recordCount = 0,
        public rowsPerPage = 0,
        public items: T[] = []
    ) { }
}'

    $code | Set-Content $fn
}

function CreateAssetFolders($path) {
    
    $img = "$path\src\assets\img"
    
    CreateFolder $img
}

function CreateEditorConfig($path) {
    $fn = "$path\.editorconfig"
    $successfullDelete = DeleteFile $fn
   
    $code = 
    '# Editor configuration, see https://editorconfig.org
root = true

[*]
charset = utf-8
indent_style = space
indent_size = 4
insert_final_newline = true
trim_trailing_whitespace = true

[*.md]
max_line_length = off
trim_trailing_whitespace = true'

    $code | Set-Content $fn
}

function CreateEnvironmentConfigs($path) {

    $fn = "$path\src\environments\environment.ts"
    $successfullDelete = DeleteFile $fn
   
    $code = 
'export const environment = {
    production: false
};

export const appSettings = {
    apiRoot: "http://localhost:44312/api/",
    identityProvider: "http://localhost:44312/api/"
}'

    $code | Set-Content $fn

    $fn = "$path\src\environments\environment.prod.ts"
    $successfullDelete = DeleteFile $fn
   
    $code = 
'export const environment = {
    production: true
};

export const appSettings = {
    apiRoot: "http://localhost:44312/api/",
    identityProvider: "http://localhost:44312/api/"
}'

    $code | Set-Content $fn
}

function CreateBusyIndicatorComponent($path) {
    
    $dest = "$path\src\app\shared"
	#--
	$path2 = "$dest\busy-indicator"

	CreateBusyIndicatorTs $path2
	CreateBusyIndicatorHtml $path2
	CreateBusyIndicatorCss $path2
}

function CreateBusyIndicatorTs($path){
	$fn = "$path\busy-indicator.component.ts"
    $successfullDelete = DeleteFile $fn
   
    $code = 
'import { Component, Input } from "@angular/core";

@Component({
    selector: "app-busy-indicator",
    templateUrl: "./busy-indicator.component.html",
    styleUrls: ["./busy-indicator.component.css"]
})
export class BusyIndicatorComponent {
    
	@Input() title: string;
    @Input() isBusy: boolean;
    
	constructor() { }
}'

    $code | Set-Content $fn
}

function CreateBusyIndicatorHtml($path){
	$fn = "$path\busy-indicator.component.html"
    $successfullDelete =  DeleteFile $fn
   
    $code = 
'<div *ngIf="isBusy" class="busy-indicator-container">
    <div class="middle">
        <div class="inner">
            <div class="splash animated bounce rubberBand" >
                <div class="message fadeInDown">{{title}}</div>
                <div class="preloader-wrapper big active">
                    <div class="spinner-layer spinner-blue">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div><div class="gap-patch">
                            <div class="circle"></div>
                        </div><div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>

                    <div class="spinner-layer spinner-red">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div><div class="gap-patch">
                            <div class="circle"></div>
                        </div><div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>

                    <div class="spinner-layer spinner-yellow">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div><div class="gap-patch">
                            <div class="circle"></div>
                        </div><div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>

                    <div class="spinner-layer spinner-green">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div><div class="gap-patch">
                            <div class="circle"></div>
                        </div><div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>'

    $code | Set-Content $fn
}

function CreateBusyIndicatorCss($path){
	$fn = "$path\busy-indicator.component.css"
    $successfullDelete = DeleteFile $fn
   
    $code = 
    'html {
    box-sizing: border-box;
}

*, *:before, *:after {
    box-sizing: inherit;
}

body {
    margin: 0;
}

.busy-indicator-container {
    text-align: center;
    display: table;
    position: absolute;
    height: 95%;
    width: 95%;
    z-index: 999;
}

.middle {
    display: table-cell;
    vertical-align: middle;
}

.inner {
    margin-left: auto;
    margin-right: auto; 

}
.splash {
    text-align: center;
    margin: 0 0 0 0;
    padding-bottom: 0;
    box-sizing: border-box;
}
 
    .splash .message {
        padding: .5em;
        color: #005EB8;
        font-family: "Roboto";
        font-size: large;
        margin-bottom: 6px;
    }


.preloader-wrapper {
    display: inline-block;
    position: relative;
    width: 48px;
    height: 48px;
}

    .preloader-wrapper.small {
        width: 36px;
        height: 36px;
    }

    .preloader-wrapper.big {
        width: 64px;
        height: 64px;
    }

    .preloader-wrapper.active {
        -webkit-animation: container-rotate 1568ms linear infinite;
        animation: container-rotate 1568ms linear infinite;
    }

@-webkit-keyframes container-rotate {
    to {
        -webkit-transform: rotate(360deg);
    }
}

@keyframes container-rotate {
    to {
        -webkit-transform: rotate(360deg);
        transform: rotate(360deg);
    }
}

.spinner-layer {
    position: absolute;
    width: 100%;
    height: 100%;
    opacity: 0;
    border-color: #26a69a;
}

.spinner-blue,
.spinner-blue-only {
    border-color: #4285f4;
}

.spinner-red,
.spinner-red-only {
    border-color: #db4437;
}

.spinner-yellow,
.spinner-yellow-only {
    border-color: #f4b400;
}

.spinner-green,
.spinner-green-only {
    border-color: #0f9d58;
}

.active .spinner-layer.spinner-blue {
    -webkit-animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both, blue-fade-in-out 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
    animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both, blue-fade-in-out 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
}

.active .spinner-layer.spinner-red {
    -webkit-animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both, red-fade-in-out 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
    animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both, red-fade-in-out 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
}

.active .spinner-layer.spinner-yellow {
    -webkit-animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both, yellow-fade-in-out 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
    animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both, yellow-fade-in-out 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
}

.active .spinner-layer.spinner-green {
    -webkit-animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both, green-fade-in-out 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
    animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both, green-fade-in-out 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
}

.active .spinner-layer,
.active .spinner-layer.spinner-blue-only,
.active .spinner-layer.spinner-red-only,
.active .spinner-layer.spinner-yellow-only,
.active .spinner-layer.spinner-green-only {
    opacity: 1;
    -webkit-animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
    animation: fill-unfill-rotate 5332ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
}

@-webkit-keyframes fill-unfill-rotate {
    12.5% {
        -webkit-transform: rotate(135deg);
    }
    25% {
        -webkit-transform: rotate(270deg);
    }
    37.5% {
        -webkit-transform: rotate(405deg);
    }
    50% {
        -webkit-transform: rotate(540deg);
    }
    62.5% {
        -webkit-transform: rotate(675deg);
    }
    75% {
        -webkit-transform: rotate(810deg);
    }
    87.5% {
        -webkit-transform: rotate(945deg);
    }
    to {
        -webkit-transform: rotate(1080deg);
    }
}

@keyframes fill-unfill-rotate {
    12.5% {
        -webkit-transform: rotate(135deg);
        transform: rotate(135deg);
    }
    25% {
        -webkit-transform: rotate(270deg);
        transform: rotate(270deg);
    }
    37.5% {
        -webkit-transform: rotate(405deg);
        transform: rotate(405deg);
    }
    50% {
        -webkit-transform: rotate(540deg);
        transform: rotate(540deg);
    }
    62.5% {
        -webkit-transform: rotate(675deg);
        transform: rotate(675deg);
    }
    75% {
        -webkit-transform: rotate(810deg);
        transform: rotate(810deg);
    }
    87.5% {
        -webkit-transform: rotate(945deg);
        transform: rotate(945deg);
    }
    to {
        -webkit-transform: rotate(1080deg);
        transform: rotate(1080deg);
    }
}

@-webkit-keyframes blue-fade-in-out {
    from {
        opacity: 1;
    }

    25% {
        opacity: 1;
    }

    26% {
        opacity: 0;
    }

    89% {
        opacity: 0;
    }

    90% {
        opacity: 1;
    }

    100% {
        opacity: 1;
    }
}

@keyframes blue-fade-in-out {
    from {
        opacity: 1;
    }

    25% {
        opacity: 1;
    }

    26% {
        opacity: 0;
    }

    89% {
        opacity: 0;
    }

    90% {
        opacity: 1;
    }

    100% {
        opacity: 1;
    }
}

@-webkit-keyframes red-fade-in-out {
    from {
        opacity: 0;
    }

    15% {
        opacity: 0;
    }

    25% {
        opacity: 1;
    }

    50% {
        opacity: 1;
    }

    51% {
        opacity: 0;
    }
}

@keyframes red-fade-in-out {
    from {
        opacity: 0;
    }

    15% {
        opacity: 0;
    }

    25% {
        opacity: 1;
    }

    50% {
        opacity: 1;
    }

    51% {
        opacity: 0;
    }
}

@-webkit-keyframes yellow-fade-in-out {
    from {
        opacity: 0;
    }

    40% {
        opacity: 0;
    }

    50% {
        opacity: 1;
    }

    75% {
        opacity: 1;
    }

    76% {
        opacity: 0;
    }
}

@keyframes yellow-fade-in-out {
    from {
        opacity: 0;
    }

    40% {
        opacity: 0;
    }

    50% {
        opacity: 1;
    }

    75% {
        opacity: 1;
    }

    76% {
        opacity: 0;
    }
}

@-webkit-keyframes green-fade-in-out {
    from {
        opacity: 0;
    }

    65% {
        opacity: 0;
    }

    75% {
        opacity: 1;
    }

    90% {
        opacity: 1;
    }

    100% {
        opacity: 0;
    }
}

@keyframes green-fade-in-out {
    from {
        opacity: 0;
    }

    65% {
        opacity: 0;
    }

    75% {
        opacity: 1;
    }

    90% {
        opacity: 1;
    }

    100% {
        opacity: 0;
    }
}

/**
 * Patch the gap that appear between the two adjacent div.circle-clipper while the
 * spinner is rotating (appears on Chrome 38, Safari 7.1, and IE 11).
 */
.gap-patch {
    position: absolute;
    top: 0;
    left: 45%;
    width: 10%;
    height: 100%;
    overflow: hidden;
    border-color: inherit;
}

    .gap-patch .circle {
        width: 1000%;
        left: -450%;
    }

.circle-clipper {
    display: inline-block;
    position: relative;
    width: 50%;
    height: 100%;
    overflow: hidden;
    border-color: inherit;
}

    .circle-clipper .circle {
        width: 200%;
        height: 100%;
        border-width: 3px;
        /* STROKEWIDTH */
        border-style: solid;
        border-color: inherit;
        border-bottom-color: transparent !important;
        border-radius: 50%;
        -webkit-animation: none;
        animation: none;
        position: absolute;
        top: 0;
        right: 0;
        bottom: 0;
    }

    .circle-clipper.left .circle {
        left: 0;
        border-right-color: transparent !important;
        -webkit-transform: rotate(129deg);
        transform: rotate(129deg);
    }

    .circle-clipper.right .circle {
        left: -100%;
        border-left-color: transparent !important;
        -webkit-transform: rotate(-129deg);
        transform: rotate(-129deg);
    }

.active .circle-clipper.left .circle {
    /* duration: ARCTIME */
    -webkit-animation: left-spin 1333ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
    animation: left-spin 1333ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
}

.active .circle-clipper.right .circle {
    /* duration: ARCTIME */
    -webkit-animation: right-spin 1333ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
    animation: right-spin 1333ms cubic-bezier(0.4, 0, 0.2, 1) infinite both;
}

@-webkit-keyframes left-spin {
    from {
        -webkit-transform: rotate(130deg);
    }

    50% {
        -webkit-transform: rotate(-5deg);
    }

    to {
        -webkit-transform: rotate(130deg);
    }
}

@keyframes left-spin {
    from {
        -webkit-transform: rotate(130deg);
        transform: rotate(130deg);
    }

    50% {
        -webkit-transform: rotate(-5deg);
        transform: rotate(-5deg);
    }

    to {
        -webkit-transform: rotate(130deg);
        transform: rotate(130deg);
    }
}

@-webkit-keyframes right-spin {
    from {
        -webkit-transform: rotate(-130deg);
    }

    50% {
        -webkit-transform: rotate(5deg);
    }

    to {
        -webkit-transform: rotate(-130deg);
    }
}

@keyframes right-spin {
    from {
        -webkit-transform: rotate(-130deg);
        transform: rotate(-130deg);
    }

    50% {
        -webkit-transform: rotate(5deg);
        transform: rotate(5deg);
    }

    to {
        -webkit-transform: rotate(-130deg);
        transform: rotate(-130deg);
    }
}

#spinnerContainer.cooldown {
    /* duration: SHRINK_TIME */
    -webkit-animation: container-rotate 1568ms linear infinite, fade-out 400ms cubic-bezier(0.4, 0, 0.2, 1);
    animation: container-rotate 1568ms linear infinite, fade-out 400ms cubic-bezier(0.4, 0, 0.2, 1);
}

@-webkit-keyframes fade-out {
    from {
        opacity: 1;
    }

    to {
        opacity: 0;
    }
}

@keyframes fade-out {
    from {
        opacity: 1;
    }

    to {
        opacity: 0;
    }
}

.slider {
    position: relative;
    height: 400px;
    width: 100%;
}

    .slider.fullscreen {
        height: 100%;
        width: 100%;
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
    }

        .slider.fullscreen ul.slides {
            height: 100%;
        }

        .slider.fullscreen ul.indicators {
            z-index: 2;
            bottom: 30px;
        }

    .slider .slides {
        background-color: #9e9e9e;
        margin: 0;
        height: 400px;
    }

        .slider .slides li {
            opacity: 0;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 1;
            width: 100%;
            height: inherit;
            overflow: hidden;
        }

            .slider .slides li img {
                height: 100%;
                width: 100%;
                background-size: cover;
                background-position: center;
            }

            .slider .slides li .caption {
                color: #fff;
                position: absolute;
                top: 15%;
                left: 15%;
                width: 70%;
                opacity: 0;
            }

                .slider .slides li .caption p {
                    color: #e0e0e0;
                }

            .slider .slides li.active {
                z-index: 2;
            }

    .slider .indicators {
        position: absolute;
        text-align: center;
        left: 0;
        right: 0;
        bottom: 0;
        margin: 0;
    }

        .slider .indicators .indicator-item {
            display: inline-block;
            position: relative;
            cursor: pointer;
            height: 16px;
            width: 16px;
            margin: 0 12px;
            background-color: #e0e0e0;
            transition: background-color .3s;
            border-radius: 50%;
        }

            .slider .indicators .indicator-item.active {
                background-color: #4CAF50;
            }

.carousel {
    overflow: hidden;
    position: relative;
    width: 100%;
    height: 400px;
    -webkit-perspective: 500px;
    perspective: 500px;
    -webkit-transform-style: preserve-3d;
    transform-style: preserve-3d;
    -webkit-transform-origin: 0% 50%;
    transform-origin: 0% 50%;
}

    .carousel.carousel-slider {
        top: 0;
        left: 0;
        height: 0;
    }

        .carousel.carousel-slider .carousel-fixed-item {
            position: absolute;
            left: 0;
            right: 0;
            bottom: 20px;
            z-index: 1;
        }

            .carousel.carousel-slider .carousel-fixed-item.with-indicators {
                bottom: 68px;
            }

        .carousel.carousel-slider .carousel-item {
            width: 100%;
            height: 100%;
            min-height: 400px;
            position: absolute;
            top: 0;
            left: 0;
        }

            .carousel.carousel-slider .carousel-item h2 {
                font-size: 24px;
                font-weight: 500;
                line-height: 32px;
            }

            .carousel.carousel-slider .carousel-item p {
                font-size: 15px;
            }

    .carousel .carousel-item {
        display: none;
        width: 200px;
        height: 200px;
        position: absolute;
        top: 0;
        left: 0;
    }

        .carousel .carousel-item img {
            width: 100%;
        }

    .carousel .indicators {
        position: absolute;
        text-align: center;
        left: 0;
        right: 0;
        bottom: 0;
        margin: 0;
    }

        .carousel .indicators .indicator-item {
            display: inline-block;
            position: relative;
            cursor: pointer;
            height: 8px;
            width: 8px;
            margin: 24px 4px;
            background-color: rgba(255, 255, 255, 0.5);
            transition: background-color .3s;
            border-radius: 50%;
        }

            .carousel .indicators .indicator-item.active {
                background-color: #fff;
            }

.tap-target-wrapper {
    width: 800px;
    height: 800px;
    position: fixed;
    z-index: 1000;
    visibility: hidden;
    transition: visibility 0s .3s;
}

    .tap-target-wrapper.open {
        visibility: visible;
        transition: visibility 0s;
    }

        .tap-target-wrapper.open .tap-target {
            -webkit-transform: scale(1);
            transform: scale(1);
            opacity: .95;
            transition: opacity 0.3s cubic-bezier(0.42, 0, 0.58, 1), -webkit-transform 0.3s cubic-bezier(0.42, 0, 0.58, 1);
            transition: transform 0.3s cubic-bezier(0.42, 0, 0.58, 1), opacity 0.3s cubic-bezier(0.42, 0, 0.58, 1);
            transition: transform 0.3s cubic-bezier(0.42, 0, 0.58, 1), opacity 0.3s cubic-bezier(0.42, 0, 0.58, 1), -webkit-transform 0.3s cubic-bezier(0.42, 0, 0.58, 1);
        }

        .tap-target-wrapper.open .tap-target-wave::before {
            -webkit-transform: scale(1);
            transform: scale(1);
        }

        .tap-target-wrapper.open .tap-target-wave::after {
            visibility: visible;
            -webkit-animation: pulse-animation 1s cubic-bezier(0.24, 0, 0.38, 1) infinite;
            animation: pulse-animation 1s cubic-bezier(0.24, 0, 0.38, 1) infinite;
            transition: opacity .3s, visibility 0s 1s, -webkit-transform .3s;
            transition: opacity .3s, transform .3s, visibility 0s 1s;
            transition: opacity .3s, transform .3s, visibility 0s 1s, -webkit-transform .3s;
        }

.tap-target {
    position: absolute;
    font-size: 1rem;
    border-radius: 50%;
    background-color: #ee6e73;
    box-shadow: 0 20px 20px 0 rgba(0, 0, 0, 0.14), 0 10px 50px 0 rgba(0, 0, 0, 0.12), 0 30px 10px -20px rgba(0, 0, 0, 0.2);
    width: 100%;
    height: 100%;
    opacity: 0;
    -webkit-transform: scale(0);
    transform: scale(0);
    transition: opacity 0.3s cubic-bezier(0.42, 0, 0.58, 1), -webkit-transform 0.3s cubic-bezier(0.42, 0, 0.58, 1);
    transition: transform 0.3s cubic-bezier(0.42, 0, 0.58, 1), opacity 0.3s cubic-bezier(0.42, 0, 0.58, 1);
    transition: transform 0.3s cubic-bezier(0.42, 0, 0.58, 1), opacity 0.3s cubic-bezier(0.42, 0, 0.58, 1), -webkit-transform 0.3s cubic-bezier(0.42, 0, 0.58, 1);
}

@media only screen and (max-width: 600px) {
    .tap-target {
        width: 600px;
        height: 600px;
    }
}

.tap-target-content {
    position: relative;
    display: table-cell;
}

.tap-target-wave {
    position: absolute;
    border-radius: 50%;
    z-index: 10001;
}

    .tap-target-wave::before, .tap-target-wave::after {
        content: '';
        display: block;
        position: absolute;
        width: 100%;
        height: 100%;
        border-radius: 50%;
        background-color: #ffffff;
    }

    .tap-target-wave::before {
        -webkit-transform: scale(0);
        transform: scale(0);
        transition: -webkit-transform .3s;
        transition: transform .3s;
        transition: transform .3s, -webkit-transform .3s;
    }

    .tap-target-wave::after {
        visibility: hidden;
        transition: opacity .3s, visibility 0s, -webkit-transform .3s;
        transition: opacity .3s, transform .3s, visibility 0s;
        transition: opacity .3s, transform .3s, visibility 0s, -webkit-transform .3s;
        z-index: -1;
    }

.tap-target-origin {
    top: 50%;
    left: 50%;
    -webkit-transform: translate(-50%, -50%);
    transform: translate(-50%, -50%);
    z-index: 10002;
    position: absolute !important;
}

    .tap-target-origin:not(.btn):not(.btn-large), .tap-target-origin:not(.btn):not(.btn-large):hover {
        background: none;
    }

.pulse {
    overflow: initial;
}

    .pulse::before {
        content: '';
        display: block;
        position: absolute;
        width: 100%;
        height: 100%;
        background-color: inherit;
        border-radius: inherit;
        transition: opacity .3s, -webkit-transform .3s;
        transition: opacity .3s, transform .3s;
        transition: opacity .3s, transform .3s, -webkit-transform .3s;
        -webkit-animation: pulse-animation 1s cubic-bezier(0.24, 0, 0.38, 1) infinite;
        animation: pulse-animation 1s cubic-bezier(0.24, 0, 0.38, 1) infinite;
        z-index: -1;
    }

@-webkit-keyframes pulse-animation {
    0% {
        opacity: 1;
        -webkit-transform: scale(1);
        transform: scale(1);
    }

    50% {
        opacity: 0;
        -webkit-transform: scale(1.5);
        transform: scale(1.5);
    }

    100% {
        opacity: 0;
        -webkit-transform: scale(1.5);
        transform: scale(1.5);
    }
}

@keyframes pulse-animation {
    0% {
        opacity: 1;
        -webkit-transform: scale(1);
        transform: scale(1);
    }

    50% {
        opacity: 0;
        -webkit-transform: scale(1.5);
        transform: scale(1.5);
    }

    100% {
        opacity: 0;
        -webkit-transform: scale(1.5);
        transform: scale(1.5);
    }
}

ai-dialog-container {
    display: flex;
    justify-content: center;
    align-items: center;
}

    ai-dialog-container > div {
        margin: auto !important;
    }'

    $code | Set-Content $fn
}

function CreateEmlCheckBoxComponent($path) {
    
    $dest = "$path\src\app\shared"
	$path2 = "$dest\eml-checkbox"

	CreateEmlCheckboxTs $path2
	CreateEmlCheckboxHtml $path2
	CreateEmlCheckboxCss $path2
}

function CreateEmlCheckboxTs($path){
	$fn = "$path\eml-checkbox.component.ts"
    $successfullDelete = DeleteFile $fn
   
    $code = 
    'import { Component, OnChanges, Input, EventEmitter, Output } from "@angular/core";

@Component({
    selector: "app-checkbox",
    templateUrl: "./eml-checkbox.component.html",
    styleUrls: ["./eml-checkbox.component.css"]
})
export class EmlCheckboxComponent implements OnChanges {
    
	@Input() isSelected = false;
    @Input() title: string;
    @Input() class = "eml-checkboxlabel";
    @Output() isSelectedChange = new EventEmitter<boolean>();
    
	constructor() { }
    ngOnChanges(): void { }
}'

    $code | Set-Content $fn
}

function CreateEmlCheckboxHtml($path){
	$fn = "$path\eml-checkbox.component.html"
    $successfullDelete = DeleteFile $fn
   
    $code = 
    '<div class="eml-checkbox">
    <label class="text-nowrap eml-checkboxlabel">
            <input type="checkbox" (change)= "isSelected = !isSelected; isSelectedChange.emit(isSelected)">
            <span class="cr"><i class="cr-icon fa fa-check"></i></span>
            <div style="padding-top: 2px;">
                    <span class="eml-checkbox-caption">{{ title }}</span>
            </div>
    </label>
</div>'

    $code | Set-Content $fn
}

function CreateEmlCheckboxCss($path){
	$fn = "$path\eml-checkbox.component.css"
    $successfullDelete = DeleteFile $fn
   
    $code = 
    '
/*checkbox*/
.eml-checkboxlabel {
    padding-left: 0;
    cursor: pointer
}

.eml-checkbox label:after,
.radio label:after {
    content: "";
    display: table;
    clear: both;
}

.eml-checkbox .cr,
.radio .cr {
    position: relative;
    display: inline-block;
    border: 1px solid #a9a9a9;
    border-radius: .25em;
    width: 1.3em;
    height: 1.3em;
    float: left;
    margin-right: .5em;
}

.radio .cr {
    border-radius: 50%;
}

.eml-checkbox .cr .cr-icon,
.radio .cr .cr-icon {
    position: absolute;
    font-size: 1em;
    line-height: 0;
    top: 45%;
    left: 16%;
}

.radio .cr .cr-icon {
    margin-left: 0.04em;
}

.eml-checkbox label input[type="checkbox"],
.radio label input[type="radio"] {
    display: none;
}

.eml-checkbox label input[type="checkbox"] + .cr > .cr-icon,
.radio label input[type="radio"] + .cr > .cr-icon {
    transform: scale(3) rotateZ(-20deg);
    opacity: 0;
    transition: all .3s ease-in;
}

.eml-checkbox label input[type="checkbox"]:checked + .cr > .cr-icon,
.radio label input[type="radio"]:checked + .cr > .cr-icon {
    transform: scale(1) rotateZ(0deg);
    opacity: 1;
}

.eml-checkbox label input[type="checkbox"]:disabled + .cr,
.radio label input[type="radio"]:disabled + .cr {
    opacity: .5;
}
.eml-checkbox-caption{
    font-weight: bold;
}'

    $code | Set-Content $fn
}

function CreateDebuggerPipe($path) {
    
    $dest = "$path\src\app\shared\pipes"
	$fn = "$dest\debugger.pipe.ts"
    $successfullDelete = DeleteFile $fn
   
    $code = 
    'import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "debugger"
})
export class DebuggerPipe implements PipeTransform {

    transform(value: any, args?: any): any {

        console.warn("===myDebugger");
        console.warn(value);

        return value;

    }
}'
    $code | Set-Content $fn
}

function CreateAngular($path, $angularAppName) {
  
    Clear-Host
    Write-Host 
    Write-Host $angularAppName": $path" -foreground Cyan
    Write-Host 

    Set-Location $path
    
    DeleteFile "$path\browserslist"
    DeleteFile "$path\karma.conf.js"
    DeleteFile "$path\tsconfig.app.json"
    DeleteFile "$path\tsconfig.spec.json"
    DeleteFile "$path\angular.json"
    DeleteFile "$path\.editorconfig"
    DeleteFile "$path\.gitignore"
    DeleteFile "$path\package.json"
    DeleteFile "$path\angular.json"
    DeleteFile "$path\README.md"
    DeleteFile "$path\tsconfig.json"
    DeleteFile "$path\tslint.json"
    DeleteFile "$path\package-lock.json"
    DeleteDirectory "$path\node_modules"
    DeleteDirectory "$path\e2e"
    DeleteDirectory "$path\src"

    ng new $angularAppName --directory=./ --routing --skip-install --skip-git --style=css --interactive=false --skipTests

    CreateLaunchJson $path
    
    npm i @angular/cdk@8.2.3 jquery@3.4.1 popper.js@1.16.0 bootstrap@^4.4.1 animate.css@3.7.0 font-awesome@4.7.0 roboto-fontface@0.10.0 moment@2.23.0 primeng@7.0.3 primeicons@1.0.0 dexie@2.0.4 --save
    npm i typescript@3.5.3 ts-helpers@1.1.2 eslint@6.7.2 --save-dev
  
    # app is the root folder
    ng g m shared/AppShared --module=app --flat=true
    ng g component shared/BusyIndicator --skipTests --module=shared/app-shared
    ng g component shared/EmlCheckbox --skipTests --module=shared/app-shared
    ng g pipe shared/pipes/Debugger --skipTests --flat=true
    
    ng g service shared/services/SearchService --skipTests

    ng g component Modules --skipTests --module=app
    ng g m modules/Hotels --routing 
    ng g component modules/hotels/HotelsHome --skipTests --module=modules/hotels
    ng g component modules/hotels/HotelSearch --skipTests --module=modules/hotels
    ng g component modules/hotels/SearchResults --skipTests --module=modules/hotels
    
    ng g component modules/hotels/components/HotelsHomeContents --skipTests --module=modules/hotels
    
    ng g m modules/Flights --routing 
    ng g component modules/flights/FlightsHome --skipTests --module=modules/flights

    ng g class shared/requests/SearchRequestBase --skipTests
    ng g class shared/responses/SearchResponseBase --skipTests

    CreateDebuggerPipe $path
    CreateBusyIndicatorComponent $path
    CreateSearchService $path
    CreateSearchRequestBase $path
    CreateSearchResponseBase $path
    CreateEmlCheckBoxComponent $path
    CreateAssetFolders $path
    CreateEditorConfig $path
    CreateEnvironmentConfigs $path
    #https://baswanders.com/angular-cli-cheatsheet-an-overview-of-the-most-used-commands/
    #https://angular.io/guide/styleguide
    # # typescript versions: https://www.npmjs.com/package/typescript

    Write-Host 
    Write-Host 
}

$projectPath = (get-item $PSScriptRoot)
$appName = 'TravelRepublic' #GetAppName

CreateAngular $projectPath $appName
