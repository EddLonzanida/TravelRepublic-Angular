import { Component, OnChanges, Input, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-checkbox',
    templateUrl: './eml-checkbox.component.html',
    styleUrls: ['./eml-checkbox.component.css']
})
export class EmlCheckboxComponent implements OnChanges {
    @Input() isSelected = false;
    @Input() title: string;
    @Input() class = 'eml-checkboxlabel';
    @Output() isSelectedChange = new EventEmitter<boolean>();
    constructor() {
    }
    ngOnChanges(): void {
    }
}
