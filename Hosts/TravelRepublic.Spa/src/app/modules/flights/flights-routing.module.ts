import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FlightsHomeComponent } from './flights-home/flights-home.component';

const routes: Routes = [
  { path: '', component: FlightsHomeComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FlightsRoutingModule { }
