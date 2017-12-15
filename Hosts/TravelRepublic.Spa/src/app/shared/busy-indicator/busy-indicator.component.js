var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input } from '@angular/core';
var BusyIndicatorComponent = /** @class */ (function () {
    function BusyIndicatorComponent() {
    }
    __decorate([
        Input(),
        __metadata("design:type", String)
    ], BusyIndicatorComponent.prototype, "title", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Boolean)
    ], BusyIndicatorComponent.prototype, "isBusy", void 0);
    BusyIndicatorComponent = __decorate([
        Component({
            selector: 'app-busy-indicator',
            templateUrl: './busy-indicator.component.html',
            styleUrls: ['./busy-indicator.component.css']
        }),
        __metadata("design:paramtypes", [])
    ], BusyIndicatorComponent);
    return BusyIndicatorComponent;
}());
export { BusyIndicatorComponent };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/shared/busy-indicator/busy-indicator.component.js.map