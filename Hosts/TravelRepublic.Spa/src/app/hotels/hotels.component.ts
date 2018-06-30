import { Component, ChangeDetectorRef } from '@angular/core';
//import { Http } from '@angular/http';
import { HotelService } from './hotel-service';
import { SearchFlow } from './dto/search-flow.enum';
//import { AutoComplete } from 'primeng/primeng';
//import { SearchResultsComponent } from './search-results/search-results.component';

@Component({
    selector: 'app-hotels',
    templateUrl: './hotels.component.html',
    styleUrls: ['./hotels.component.css']
})
export class HotelsComponent {
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

    constructor(private readonly hotelService: HotelService, private readonly cd: ChangeDetectorRef)
    {
    }

    search(): void {
        this.searchFlow = SearchFlow.SEARCHING;
        this.isStartSearch = true;
        this.isBusy = true;
        this.cd.detectChanges();
    }

    getSuggestions(event): void {
        const query = event.query;
        this.hotelService.getSuggestions(query)
            .then(suggestions => {
                this.searchSuggestions = suggestions;
                this.cd.detectChanges();
            })
            .catch(e => {
                console.warn("==getSuggestions error:");
                console.error(e);
            });
    }

    restartSearch(): void {
        this.searchFlow = SearchFlow.HOME;
        this.cd.detectChanges();
    }
}
