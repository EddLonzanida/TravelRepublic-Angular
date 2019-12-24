import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MenuModule,
  PanelModule,
  ButtonModule,
  DropdownModule,
  SliderModule,
  RatingModule,
  CheckboxModule,
  AutoCompleteModule,
  DataListModule,
  SharedModule,
  OverlayPanelModule } from 'primeng/primeng';
import { HttpClientModule } from '@angular/common/http';
import { BusyIndicatorComponent } from './busy-indicator/busy-indicator.component';
import { DebuggerPipe } from './debugger.pipe';
import { EmlCheckboxComponent } from './eml-checkbox/eml-checkbox.component';
import { RatingConverterPipe } from './rating-converter.pipe';

@NgModule({
  declarations: [
    BusyIndicatorComponent,
    DebuggerPipe,
    RatingConverterPipe,
    EmlCheckboxComponent
  ],
  imports: [
    PanelModule,
    CommonModule,
  ],
  exports: [
    BusyIndicatorComponent,
    DebuggerPipe,
    RatingConverterPipe,
    EmlCheckboxComponent,
    CommonModule,
    FormsModule,
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
    OverlayPanelModule,
  ]
})
export class AppSharedModule { }
