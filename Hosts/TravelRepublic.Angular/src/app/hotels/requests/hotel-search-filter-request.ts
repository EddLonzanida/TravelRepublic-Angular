import { HotelSearchRequestBase } from './hotel-search-request-base';
export class HotelSearchFilterRequest extends HotelSearchRequestBase {
    constructor(public Name = '',
                public Star = 0,
                public UserRating = 0,
                public CostMin = 0,
                public CostMax = 0) {
        super(Name, Star, UserRating, CostMin, CostMax);
    }
}
