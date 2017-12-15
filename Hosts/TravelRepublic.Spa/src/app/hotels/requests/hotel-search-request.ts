import { HotelSearchRequestBase } from './hotel-search-request-base';
export class HotelSearchRequest extends HotelSearchRequestBase {
    constructor(public name = '',
                public star = 0,
                public userRating = 0,
                public costMin = 0,
                public costMax = 0,
                public page = 1,
                public sorting = 0) {
        super(name, star, userRating, costMin, costMax);
    }
}
