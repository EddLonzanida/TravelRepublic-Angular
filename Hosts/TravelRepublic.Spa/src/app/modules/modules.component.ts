import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/primeng';

@Component({
  selector: 'app-modules',
  templateUrl: './modules.component.html',
  styleUrls: ['./modules.component.css']
})
export class ModulesComponent implements OnInit {

  menuItems: MenuItem[];
  miniMenuItems: MenuItem[];

  constructor() {
  }

  ngOnInit() {

    this.menuItems = [
      { label: 'Flights', icon: 'fa fa-plane', routerLink: ['/flights'], routerLinkActiveOptions: { exact: true } },
      { label: 'Hotels', icon: 'fa fa-bed', routerLink: [''], routerLinkActiveOptions: { exact: true } }
    ];

    this.miniMenuItems = [];
    this.menuItems.forEach((item: MenuItem) => {

      const miniItem = { icon: item.icon, routerLink: item.routerLink };

      this.miniMenuItems.push(miniItem);
    });
  }
}
