import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
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
import { MyDebuggerPipe } from './shared/my-debugger.pipe';
import { RatingConverterPipe } from './shared/rating-converter.pipe';

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
export class AppModule { }

export function getBaseUrl() {
  return 'http://localhost:44340/';
}




