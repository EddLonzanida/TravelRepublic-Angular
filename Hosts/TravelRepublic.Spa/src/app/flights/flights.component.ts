import { Component, OnInit } from '@angular/core';
import { SearchService } from "../shared/services/search.service";

@Component({
    selector: 'app-flights',
    templateUrl: './flights.component.html',
    styleUrls: ['./flights.component.css']
})
export class FlightsComponent implements OnInit {
    public airport: IAirport;

    departureBeforeCurrentDate = false;
    arrivalBeforeDepartureDate = false;
    twoHoursWaitingTime = false;

    constructor(private readonly searchService: SearchService)
    {
    }


    async search(): Promise<void> {
        const controller = 'flight';
        const action = 'segments';
        const route = `${controller}/${action}`;
        const searchCode = this.getSearchCode();
        const parameter = { searchCode: searchCode };

        this.searchService.request<IAirport>(route, parameter)
            .then(r => {
                this.airport = r;
            })
            .catch(e => {
                console.error(e);
            });

        //this.http.get(`${this.baseUrl}flight/segments?searchCode=${searchCode}`).subscribe(result => {
        //    this.airport = result.json() as IAirport;
        //}, error => console.error(error));
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
