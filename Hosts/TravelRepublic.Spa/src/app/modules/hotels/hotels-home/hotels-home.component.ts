import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import { SearchFlow } from '../dto/search-flow.enum';
import { HotelService } from '../services/hotel-service';

@Component({
  selector: 'app-hotels-home',
  templateUrl: './hotels-home.component.html',
  styleUrls: ['./hotels-home.component.css']
})
export class HotelsHomeComponent {
    // search parameters
    searchName: string;

    // auto complete
    searchSuggestions: string[];

    // search workflow
    searchFlow = SearchFlow.HOME;
    searchFlowEnum = SearchFlow;

    // busyIndicator
    isBusy = false;
    isStartSearch = false;

    constructor(private readonly hotelService: HotelService, private readonly cd: ChangeDetectorRef) {
    }

    search(): void {

        this.searchFlow = SearchFlow.SEARCHING;
        this.isStartSearch = true;
        this.isBusy = true;
        this.cd.detectChanges();
    }

    getSuggestions(event: { query: any; }): void {

        const query = event.query;

        this.hotelService.getSuggestions(query).subscribe(data => {

            this.searchSuggestions = data;
            this.cd.detectChanges();

        });
    }

    restartSearch(): void {

        this.searchFlow = SearchFlow.HOME;
        this.cd.detectChanges();
    }
}
