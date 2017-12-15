var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input, EventEmitter, Output } from '@angular/core';
var EmlCheckboxComponent = /** @class */ (function () {
    function EmlCheckboxComponent() {
        this.isSelected = false;
        this.class = 'eml-checkboxlabel';
        this.isSelectedChange = new EventEmitter();
    }
    EmlCheckboxComponent.prototype.ngOnChanges = function () {
    };
    __decorate([
        Input(),
        __metadata("design:type", Object)
    ], EmlCheckboxComponent.prototype, "isSelected", void 0);
    __decorate([
        Input(),
        __metadata("design:type", String)
    ], EmlCheckboxComponent.prototype, "title", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Object)
    ], EmlCheckboxComponent.prototype, "class", void 0);
    __decorate([
        Output(),
        __metadata("design:type", EventEmitter)
    ], EmlCheckboxComponent.prototype, "isSelectedChange", void 0);
    EmlCheckboxComponent = __decorate([
        Component({
            selector: 'app-checkbox',
            templateUrl: './eml-checkbox.component.html',
            styleUrls: ['./eml-checkbox.component.css']
        }),
        __metadata("design:paramtypes", [])
    ], EmlCheckboxComponent);
    return EmlCheckboxComponent;
}());
export { EmlCheckboxComponent };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/shared/eml-checkbox/eml-checkbox.component.js.map