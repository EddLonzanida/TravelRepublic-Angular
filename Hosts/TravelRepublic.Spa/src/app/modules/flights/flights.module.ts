import { NgModule } from '@angular/core';
import { FlightsRoutingModule } from './flights-routing.module';
import { FlightsHomeComponent } from './flights-home/flights-home.component';
import { AppSharedModule } from 'src/app/shared/app-shared.module';

@NgModule({
  declarations: [
    FlightsHomeComponent
  ],
  imports: [
    AppSharedModule,
    FlightsRoutingModule
  ]
})
export class FlightsModule { }
