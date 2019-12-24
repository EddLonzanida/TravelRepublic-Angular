import { Component, OnInit } from '@angular/core';
import { SearchService } from '../../../shared/services/search.service';

@Component({
  selector: 'app-flights-home',
  templateUrl: './flights-home.component.html',
  styleUrls: ['./flights-home.component.css']
})
export class FlightsHomeComponent implements OnInit {

    public airport: IAirport;

    departureBeforeCurrentDate = false;
    arrivalBeforeDepartureDate = false;
    twoHoursWaitingTime = false;

    constructor(private readonly searchService: SearchService) {
    }

    async search(): Promise<void> {

        const controller = 'flight';
        const route = `${controller}`;
        const searchCode = this.getSearchCode();
        const parameter = { searchCode: searchCode };

        this.searchService.request<IAirport>('FlightsComponent.search', route, parameter).subscribe(r => {

            this.airport = r;

        });
    }

    ngOnInit() {
    }

    private getSearchCode(): number {

        let searchCode: number;
        // truth table
        if (!this.departureBeforeCurrentDate && !this.arrivalBeforeDepartureDate && this.twoHoursWaitingTime) {

            searchCode = 1;

        } else if (!this.departureBeforeCurrentDate && this.arrivalBeforeDepartureDate && !this.twoHoursWaitingTime) {

            searchCode = 2;

        } else if (!this.departureBeforeCurrentDate && this.arrivalBeforeDepartureDate && this.twoHoursWaitingTime) {

            searchCode = 3;

        } else if (this.departureBeforeCurrentDate && !this.arrivalBeforeDepartureDate && !this.twoHoursWaitingTime) {

            searchCode = 4;

        } else if (this.departureBeforeCurrentDate && !this.arrivalBeforeDepartureDate && this.twoHoursWaitingTime) {

            searchCode = 5;

        } else if (this.departureBeforeCurrentDate && this.arrivalBeforeDepartureDate && !this.twoHoursWaitingTime) {

            searchCode = 6;

        } else if (this.departureBeforeCurrentDate && this.arrivalBeforeDepartureDate && this.twoHoursWaitingTime) {

            searchCode = 7;

        } else { searchCode = 0; }

        return searchCode;
    }
}

interface ISegment {
    departureDate: string;
    arrivalDate: string;
    segmentType: string;
}

interface IFlight {
    segments: Array<ISegment>;
}

interface IAirport {
    flights: Array<IFlight>;
}
