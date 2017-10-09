import { StarFilter } from '../dto/star-filter';

export class HotelSearchFilterResponse {
    constructor(public StarFilters: StarFilter[] = [],
                public RatingMin = 0,
                public RatingMax = 100,
                public CostMin = 0,
                public CostMax = 0) { }
}
