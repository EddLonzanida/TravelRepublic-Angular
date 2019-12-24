import { StarFilter } from '../dto/star-filter';

export class HotelSearchFilterResponse {
    constructor(public starFilters: StarFilter[] = [],
                public ratingMin = 0,
                public ratingMax = 100,
                public costMin = 0,
                public costMax = 0) { }
}
