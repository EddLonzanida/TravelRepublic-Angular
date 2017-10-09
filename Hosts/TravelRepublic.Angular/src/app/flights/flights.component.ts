import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { CheckboxModule } from 'primeng/primeng';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-flights',
  templateUrl: './flights.component.html',
  styleUrls: ['./flights.component.css']
})
export class FlightsComponent implements OnInit {
  private baseUrl: string;
  private http: Http;
  public airport: IAirport;

  departureBeforeCurrentDate = false;
  arrivalBeforeDepartureDate = false;
  twoHoursWaitingTime = false;
  constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }
  async search(): Promise<void> {
    const searchCode = this.getSearchCode();
    this.http.get(`${this.baseUrl}flight/segments?searchCode=${searchCode}`).subscribe(result => {
      this.airport = result.json() as IAirport;
    }, error => console.error(error));
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
  DepartureDate: string;
  ArrivalDate: string;
  SegmentType: string;
}
interface IFlight {
  Segments: Array<ISegment>;
}
interface IAirport {
  Flights: Array<IFlight>;
}
