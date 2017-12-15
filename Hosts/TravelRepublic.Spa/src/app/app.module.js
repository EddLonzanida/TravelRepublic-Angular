var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { MenuModule, PanelModule, ButtonModule, DropdownModule, SliderModule, RatingModule, CheckboxModule, AutoCompleteModule, DataListModule, SharedModule, OverlayPanelModule } from 'primeng/primeng';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HotelsComponent } from './hotels/hotels.component';
import { FlightsComponent } from './flights/flights.component';
import { EmlCheckboxComponent } from './shared/eml-checkbox/eml-checkbox.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HotelService } from './hotels/hotel-service';
import { SearchResultsComponent } from './hotels/search-results/search-results.component';
import { SearchHomeComponent } from './hotels/search-home/search-home.component';
import { BusyIndicatorComponent } from './shared/busy-indicator/busy-indicator.component';
import { MyDebuggerPipe } from './shared/my-debugger.pipe';
import { RatingConverterPipe } from './shared/rating-converter.pipe';
var appRoutes = [
    { path: '', redirectTo: '/hotels', pathMatch: 'full' },
    { path: 'hotels', component: HotelsComponent },
    { path: 'flights', component: FlightsComponent },
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        NgModule({
            declarations: [
                AppComponent,
                HotelsComponent,
                FlightsComponent,
                EmlCheckboxComponent,
                SearchResultsComponent,
                SearchHomeComponent,
                BusyIndicatorComponent,
                MyDebuggerPipe,
                RatingConverterPipe
            ],
            imports: [
                BrowserModule,
                FormsModule,
                RouterModule.forRoot(appRoutes),
                BrowserAnimationsModule,
                MenuModule,
                PanelModule,
                ButtonModule,
                DropdownModule,
                SliderModule,
                RatingModule,
                CheckboxModule,
                AutoCompleteModule,
                DataListModule,
                HttpModule,
                HttpClientModule,
                SharedModule, OverlayPanelModule
            ],
            providers: [{ provide: 'BASE_URL', useFactory: getBaseUrl }, HotelService],
            bootstrap: [AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
export { AppModule };
export function getBaseUrl() {
    return 'http://localhost:44340/';
}
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/app.module.js.map