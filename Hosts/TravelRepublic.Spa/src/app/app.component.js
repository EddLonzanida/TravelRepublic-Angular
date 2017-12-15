var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewChild } from '@angular/core';
import { Menu } from "primeng/components/menu/menu";
import { Router } from "@angular/router";
var AppComponent = /** @class */ (function () {
    function AppComponent(router) {
        this.router = router;
    }
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        var handleSelected = function (event) {
            var allMenus = jQuery(event.originalEvent.target).closest('ul');
            var allLinks = allMenus.find('.menu-selected');
            allLinks.removeClass("menu-selected");
            var selected = jQuery(event.originalEvent.target).closest('a');
            selected.addClass('menu-selected');
        };
        this.menuItems = [
            { label: 'Hotels', icon: 'fa-bed', routerLink: ['/hotels'], command: function (event) { return handleSelected(event); } },
            { label: 'Flights', icon: 'fa-plane', routerLink: ['/flights'], command: function (event) { return handleSelected(event); } },
        ];
        this.miniMenuItems = [];
        this.menuItems.forEach(function (item) {
            var miniItem = { icon: item.icon, routerLink: item.routerLink };
            _this.miniMenuItems.push(miniItem);
        });
    };
    AppComponent.prototype.selectInitialMenuItemBasedOnUrl = function () {
        var path = document.location.pathname;
        var menuItem = this.menuItems.find(function (item) { return item.routerLink[0] == path; });
        if (menuItem) {
            var selectedIcon = this.bigMenu.container.querySelector("." + menuItem.icon);
            jQuery(selectedIcon).closest('li').addClass('menu-selected');
        }
    };
    AppComponent.prototype.ngAfterViewInit = function () {
        this.selectInitialMenuItemBasedOnUrl();
    };
    __decorate([
        ViewChild('bigMenu'),
        __metadata("design:type", Menu)
    ], AppComponent.prototype, "bigMenu", void 0);
    __decorate([
        ViewChild('smallMenu'),
        __metadata("design:type", Menu)
    ], AppComponent.prototype, "smallMenu", void 0);
    AppComponent = __decorate([
        Component({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        __metadata("design:paramtypes", [Router])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=C:/Visual Studio 2017 Projects/DemoProjects/TravelRepublic-Angular/Hosts/TravelRepublic.Spa/wwwroot/app/app.component.js.map