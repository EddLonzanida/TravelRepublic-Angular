import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {
  MenuModule,
  PanelModule,
  ButtonModule,
  DropdownModule,
  SliderModule,
  RatingModule,
  CheckboxModule,
  AutoCompleteModule,
  DataListModule,
  SharedModule,
  OverlayPanelModule
} from 'primeng/primeng';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { HotelsComponent } from './hotels/hotels.component';
import { FlightsComponent } from './flights/flights.component';
import { EmlCheckboxComponent } from './shared/eml-checkbox/eml-checkbox.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HotelService } from './hotels/hotel-service';
import { SearchResultsComponent } from './hotels/search-results/search-results.component';
import { SearchHomeComponent } from './hotels/search-home/search-home.component';
import { BusyIndicatorComponent } from './shared/busy-indicator/busy-indicator.component';
import { DebuggerPipe } from './shared/debugger.pipe';
import { RatingConverterPipe } from './shared/rating-converter.pipe';
import { SearchService } from './shared/services/search.service';

const appRoutes: Routes = [
  { path: '', redirectTo: '/hotels', pathMatch: 'full' },
  { path: 'hotels', component: HotelsComponent },
  { path: 'flights', component: FlightsComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    HotelsComponent,
    FlightsComponent,
    EmlCheckboxComponent,
    SearchResultsComponent,
    SearchHomeComponent,
    BusyIndicatorComponent,
    DebuggerPipe,
    RatingConverterPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
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
    HttpClientModule,
    SharedModule, 
    OverlayPanelModule
  ],
  providers: [{ provide: 'BASE_URL', useFactory: getBaseUrl }, SearchService, HotelService],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
    return 'https://localhost:44312/api/';
}
