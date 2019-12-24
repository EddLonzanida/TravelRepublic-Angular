import { HotelSearchRequestBase } from './hotel-search-request-base';

export class HotelSearchFilterRequest extends HotelSearchRequestBase {
    constructor(public name = '',
                public star = 0,
                public userRating = 0,
                public costMin = 0,
                public costMax = 0) {
        super(name, star, userRating, costMin, costMax);
    }
}
