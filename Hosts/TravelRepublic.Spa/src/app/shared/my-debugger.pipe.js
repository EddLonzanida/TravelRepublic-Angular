var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Pipe } from '@angular/core';
var MyDebuggerPipe = /** @class */ (function () {
    function MyDebuggerPipe() {
    }
    MyDebuggerPipe.prototype.transform = function (value, args) {
        console.warn('===myDebugger');
        console.warn(value);
        return value;
    };
    MyDebuggerPipe = __decorate([
        Pipe({
            name: 'myDebugger'
        })
    ], MyDebuggerPipe);
    return MyDebuggerPipe;
}());
export { MyDebuggerPipe };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/shared/my-debugger.pipe.js.map