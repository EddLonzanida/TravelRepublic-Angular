var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Pipe } from '@angular/core';
var RatingConverterPipe = /** @class */ (function () {
    function RatingConverterPipe() {
    }
    RatingConverterPipe.prototype.transform = function (value, args) {
        return value / 10;
    };
    RatingConverterPipe = __decorate([
        Pipe({
            name: 'ratingConverter'
        })
    ], RatingConverterPipe);
    return RatingConverterPipe;
}());
export { RatingConverterPipe };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/shared/rating-converter.pipe.js.map