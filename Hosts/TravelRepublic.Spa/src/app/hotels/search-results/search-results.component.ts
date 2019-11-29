import { Component, OnChanges, Input, EventEmitter, Output, SimpleChanges, OnDestroy } from '@angular/core';
import { SelectItem, OverlayPanel } from 'primeng/primeng';

import { HotelService } from '../hotel-service';
import { HotelSearchRequest } from '../requests/hotel-search-request';
import { HotelSearchResponse } from '../responses/hotel-search-response';
import { HotelSearchFilterResponse } from '../responses/hotel-search-filter-response';
import { HotelSorting } from '../dto/hotel-sorting.enum';
import { Establishment } from '../dto/establishment';
import { Observable, Subscription } from 'rxjs';

@Component({
    selector: 'app-search-results',
    templateUrl: './search-results.component.html',
    styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnChanges, OnDestroy {
  
    private subcriptions: Subscription = new Subscription();

    sortOrders: SelectItem[] = [];
    selectedSortOrder: number;
    isInitialized = false;
    isLocalBusy = false; // fix to ExpressionChangedAfterItHasBeenCheckedError
    canShowPager = false;
    canFilter = true;
    selectedEstablishment: Establishment;
    hasErrors = false;
    // search parameters
    searchRequest = new HotelSearchRequest();
    costMinMax: number[] = [0, 0];
    // search responses
    searchResponse = new HotelSearchResponse();

    // Filters
    searchFilterResponse = new HotelSearchFilterResponse();

    //change detection
    oldStar: number;
    oldCostMin: number;
    oldCostMax: number;
    oldUserRating: number;

    // Used with Parent Interaction
    @Input() searchName: string;
    @Input() isBusy: boolean;
    @Input() isStartSearch: boolean;
    @Output() isBusyChange = new EventEmitter<boolean>();
    @Output() isStartSearchChange = new EventEmitter<boolean>();
    @Output() restartSearch: EventEmitter<string> = new EventEmitter();

    constructor(private readonly hotelService: HotelService) {
        this.refillSortOrderList();
    }

    search(): void {

        if (this.isLocalBusy) {

            console.warn('======BUSY Please wait...');

            return;
        }

        this.setSortColumn();
        this.isLocalBusy = true;
        this.isBusy = true;
        this.hasErrors = false;
        this.isBusyChange.emit(true);
        this.searchRequest.search = this.searchName;
        this.searchRequest.costMin = this.costMinMax[0];
        this.searchRequest.costMax = this.costMinMax[1];
        this.subcriptions.add(this.hotelService.search(this.searchRequest).subscribe(r => {

            this.searchResponse = r;
            this.isStartSearch = false;
            this.isStartSearchChange.emit(false);
            this.isLocalBusy = false;
            this.isBusy = false;
            this.isBusyChange.emit(false);
            this.canShowPager = this.searchResponse.recordCount > this.searchResponse.rowsPerPage;

            if (!this.isInitialized) {

                this.hotelService.getFilters(this.searchRequest).subscribe(f => {
                    this.searchFilterResponse = f;
                    this.isInitialized = true;
                    this.costMinMax[0] = f.costMin;
                    this.costMinMax[1] = f.costMax;
                    this.searchFilterResponse.ratingMin = f.ratingMin * 10;
                    this.searchFilterResponse.ratingMax = f.ratingMax * 10;
                    this.searchRequest.userRating = f.ratingMin;

                    if (this.searchFilterResponse.starFilters[0]) {

                        this.searchRequest.star = this.searchFilterResponse.starFilters[0].star;
                        this.oldStar = this.searchRequest.star;

                    }

                    this.canFilter = this.searchResponse.recordCount > 1;
                    this.oldCostMin = this.costMinMax[0];
                    this.oldCostMax = this.costMinMax[1];
                    this.oldUserRating = this.searchRequest.userRating;

                }, error => this.handleError());
            }
        }, error => this.handleError()));
    }

    onSlideEnd(event, sender) {

        let hasChanges = false;

        if (sender === 'star') {

            if (event.value !== this.oldStar) {

                hasChanges = true;
                this.oldStar = event.value;

            }
        } else if (sender === 'costMinMax') {

            if (this.costMinMax[0] !== this.oldCostMin) {

                hasChanges = true;
                this.oldCostMin = this.costMinMax[0];

            }

            if (this.costMinMax[1] !== this.oldCostMax) {

                hasChanges = true;
                this.oldCostMax = this.costMinMax[1];

            }
        } else if (sender === 'userRating') {

            if (event.value !== this.oldUserRating) {

                hasChanges = true;
                this.oldUserRating = event.value;

            }
        }
        if (hasChanges) {

            this.searchRequest.page = 1;
            this.search();
        }
    }

    onSlideCancelled($event) {

        this.searchRequest.star = this.searchFilterResponse.starFilters[0].star;
        this.oldStar = this.searchRequest.star;
        this.search();
    }

    onLazyLoad(event) {

        if (event && event.first > 0 && event.rows > 0) {

            this.searchRequest.page = (event.first / event.rows) + 1;

        } else {

            this.searchRequest.page = 1;

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

            this.searchRequest.page = 1;
            this.search();

        } else { }
    }

    canResetUserRating(): boolean {

        return this.searchRequest.userRating !== this.searchFilterResponse.ratingMin;
    }

    resetUserRating() {

        this.searchRequest.userRating = this.searchFilterResponse.ratingMin;
        this.oldUserRating = this.searchRequest.userRating;
        this.searchRequest.page = 1;
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

    private setSortColumn(): void {

        const selectedSortOrder = this.selectedSortOrder;

        switch (selectedSortOrder) {
            case HotelSorting.Name:
                this.searchRequest.isDescending = true;
                this.searchRequest.sortColumn = null;
                break;

            case HotelSorting.DistanceAsc:
                this.searchRequest.isDescending = null;
                this.searchRequest.sortColumn = 'Distance';
                break;

            case HotelSorting.DistanceDesc:
                this.searchRequest.isDescending = true;
                this.searchRequest.sortColumn = 'Distance';
                break;

            case HotelSorting.StarsAsc:
                this.searchRequest.isDescending = null;
                this.searchRequest.sortColumn = 'Stars';
                break;

            case HotelSorting.StarsDesc:
                this.searchRequest.isDescending = true;
                this.searchRequest.sortColumn = 'Stars';
                break;

            case HotelSorting.CostAsc:
                this.searchRequest.isDescending = null;
                this.searchRequest.sortColumn = 'MinCost';
                break;

            case HotelSorting.CostDesc:
                this.searchRequest.isDescending = true;
                this.searchRequest.sortColumn = 'MinCost';
                break;

            case HotelSorting.UserRatingAsc:
                this.searchRequest.isDescending = null;
                this.searchRequest.sortColumn = 'UserRating';
                break;

            case HotelSorting.UserRatingDesc:
                this.searchRequest.isDescending = true;
                this.searchRequest.sortColumn = 'UserRating';
                break;

            default:
                this.searchRequest.isDescending = null;
                this.searchRequest.sortColumn = null;
                break;
        }
    }

    isReady(): boolean {

        let isready = true;

        if (!this.searchResponse) { isready = false; }
        if (!this.searchFilterResponse) { isready = false; }
        if (!this.searchFilterResponse.starFilters) { return false; }
        if (this.searchFilterResponse.starFilters.length === 0) { return false; }
        if (this.searchRequest.star >= 0) { } else { return false; }

        return isready;
    }

    getStarIndex(star: number): number {

        return this.searchFilterResponse.starFilters.findIndex(filter => filter.star >= star);
    }

    canSort(): boolean {

        return this.isLocalBusy || !(this.searchResponse.recordCount > 1);
    }

    starClass(): string {

        const minStar = this.searchFilterResponse.starFilters[0].star;

        if (this.searchRequest.star === this.searchFilterResponse.starFilters[0].star) {

            return `disable-star${minStar} noreset`;

        }

        return `disable-star${this.searchRequest.star}`;
    }

    private handleError() {

        this.isStartSearch = false;
        this.isStartSearchChange.emit(false);
        this.isBusy = false;
        this.isBusyChange.emit(false);
        this.isLocalBusy = false;
        this.isInitialized = false;
        this.hasErrors = true;

    }

    ngOnDestroy(): void {
        this.subcriptions.unsubscribe();
    }
}
