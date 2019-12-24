import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HotelsHomeComponent } from './hotels-home/hotels-home.component';
import { HotelSearchComponent } from './hotel-search/hotel-search.component';

const routes: Routes = [
  { path: 'hotels', component: HotelSearchComponent, pathMatch: 'full' },
  { path: '', component: HotelsHomeComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class HotelsRoutingModule { }
