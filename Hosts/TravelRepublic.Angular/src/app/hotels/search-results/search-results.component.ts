import { Component, OnChanges, Input, EventEmitter, Output, SimpleChanges } from '@angular/core';
import { SelectItem, OverlayPanel } from 'primeng/primeng';

import { HotelService } from '../hotel-service';
import { HotelSearchRequest } from '../requests/hotel-search-request';
import { HotelSearchFilterRequest } from '../requests/hotel-search-filter-request';
import { HotelSearchResponse } from '../responses/hotel-search-response';
import { HotelSearchFilterResponse } from '../responses/hotel-search-filter-response';
import { HotelSorting } from '../dto/hotel-sorting.enum';
import { Establishment } from '../dto/establishment';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnChanges {
  sortOrders: SelectItem[] = [];
  isInitialized = false;
  isLocalBusy = false; // fix to ExpressionChangedAfterItHasBeenCheckedError
  canShowPager = false;
  // isReady = false;
  selectedEstablishment: Establishment;
  hasErrors = false;
  // search parameters
  searchRequest = new HotelSearchRequest();
  costMinMax: number[] = [0, 0];
  // search responses
  searchResponse = new HotelSearchResponse();

  // Filters
  searchFilterResponse = new HotelSearchFilterResponse();

  // Used with Parent Interaction
  @Input() searchName: string;
  @Input() isBusy: boolean;
  @Input() isStartSearch: boolean;
  @Output() isBusyChange: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() isStartSearchChange: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() restartSearch: EventEmitter<string> = new EventEmitter();

  constructor(private hotelService: HotelService) {
    this.refillSortOrderList();
  }

  search(): void {
    if (this.isLocalBusy) {
      console.warn('======BUSY Please wait...');
      return;
    }
    this.isLocalBusy = true;
    this.isBusy = true;
    this.isBusyChange.emit(true);
    this.searchRequest.Name = this.searchName;
    this.searchRequest.CostMin = this.costMinMax[0];
    this.searchRequest.CostMax = this.costMinMax[1];
    this.hasErrors = false;
    this.hotelService.search(this.searchRequest)
      .then(r => {
        this.searchResponse = r;
        this.isStartSearch = false;
        this.isStartSearchChange.emit(false);
        this.isLocalBusy = false;
        this.isBusy = false;
        this.isBusyChange.emit(false);

        this.canShowPager = this.searchResponse.RecordCount > this.searchResponse.RowsPerPage;
      })
      .then(r => {
        if (this.isInitialized) { return r; }
        this.hotelService.getFilters(this.searchRequest)
          .then(f => {
            this.searchFilterResponse = f;
            this.isInitialized = true;
            this.costMinMax[0] = f.CostMin;
            this.costMinMax[1] = f.CostMax;
            this.searchFilterResponse.RatingMin = f.RatingMin * 10;
            this.searchFilterResponse.RatingMax = f.RatingMax * 10;
          });
      })
      .catch(e => {
        this.isStartSearch = false;
        this.isStartSearchChange.emit(false);
        this.isBusy = false;
        this.isBusyChange.emit(false);
        this.isLocalBusy = false;
        this.isInitialized = false;
        this.hasErrors = true;
      });
  }
  onSlideEnd(event) {
    this.searchRequest.Page = 1;
    this.search();
  }
  onSlideCancelled($event) {
    this.searchRequest.Star = 0;
    this.search();
  }
  onLazyLoad(event) {
    if (event && event.first > 0 && event.rows > 0) {
      this.searchRequest.Page = (event.first / event.rows) + 1;
    } else {
      this.searchRequest.Page = 1;
    }
    this.search();
  }
  restart(): void {
    this.restartSearch.emit();
  }
  selectEstablishment(event, item: Establishment, overlaypanel: OverlayPanel) {
    this.selectedEstablishment = item;
    overlaypanel.toggle(event);
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes.isStartSearch && changes.isStartSearch.currentValue) {
      this.searchRequest.Page = 1;
      this.search();
    } else {
    }
  }
  resetUserRating() {
    this.searchRequest.UserRating = this.searchFilterResponse.RatingMin;
    this.searchRequest.Page = 1;
    this.search();
  }
  private refillSortOrderList(): void {
    this.sortOrders = [];
    this.sortOrders.push({ label: '-- Select Sorting --', value: null });
    this.sortOrders.push({ label: 'Name Desc', value: HotelSorting.Name });
    this.sortOrders.push({ label: 'Name Asc', value: HotelSorting.Default });
    this.sortOrders.push({ label: 'Farthest', value: HotelSorting.DistanceDesc });
    this.sortOrders.push({ label: 'Nearest', value: HotelSorting.DistanceAsc });
    this.sortOrders.push({ label: 'Stars - Highest', value: HotelSorting.StarsDesc });
    this.sortOrders.push({ label: 'Stars - Lowest', value: HotelSorting.StarsAsc });
    this.sortOrders.push({ label: 'Cost - Cheapest', value: HotelSorting.CostAsc });
    this.sortOrders.push({ label: 'Cost - Wealthiest', value: HotelSorting.CostDesc });
    this.sortOrders.push({ label: 'Rating - Highest', value: HotelSorting.UserRatingDesc });
    this.sortOrders.push({ label: 'Rating - Lowest', value: HotelSorting.UserRatingAsc });
  }
  isReady(): boolean {
    let isready = true;
    if (!this.searchResponse) { isready = false; }
    if (!this.searchFilterResponse) { isready = false; }
    if (this.searchFilterResponse.StarFilters.length === 0) { return false; }
    if (this.searchRequest.Star >= 0) { } else { return false; }
    return isready;
  }
  getStarIndex(star: number): number {
    return this.searchFilterResponse.StarFilters.findIndex(filter => filter.Star >= star);
  }
}
