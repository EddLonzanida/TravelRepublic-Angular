import { NgModule } from '@angular/core';
import { HotelsRoutingModule } from './hotels-routing.module';
import { HotelsHomeComponent } from './hotels-home/hotels-home.component';
import { HotelSearchComponent } from './hotel-search/hotel-search.component';
import { HotelsHomeContentsComponent } from './components/hotels-home-contents/hotels-home-contents.component';
import { AppSharedModule } from 'src/app/shared/app-shared.module';

@NgModule({
  declarations: [
    HotelsHomeComponent,
    HotelSearchComponent,
    HotelsHomeContentsComponent
  ],
  imports: [
    AppSharedModule,
    HotelsRoutingModule
  ]
})
export class HotelsModule { }
