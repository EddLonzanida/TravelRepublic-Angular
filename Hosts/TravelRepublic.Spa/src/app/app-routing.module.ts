import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ModulesComponent } from './modules/modules.component';

const routes: Routes = [{
  path: '', component: ModulesComponent, children: [
    { path: '', loadChildren: () => import('./modules/hotels/hotels.module').then(m => m.HotelsModule) },
    { path: 'flights', loadChildren: () => import('./modules/flights/flights.module').then(m => m.FlightsModule) },
  ]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
