import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-busy-indicator',
  templateUrl: './busy-indicator.component.html',
  styleUrls: ['./busy-indicator.component.css']
})
export class BusyIndicatorComponent  {
  @Input() title: string;
  @Input() isBusy: boolean;
  constructor() { }
}
