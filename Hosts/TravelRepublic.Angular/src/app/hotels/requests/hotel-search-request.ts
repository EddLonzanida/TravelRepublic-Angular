import { HotelSearchRequestBase } from './hotel-search-request-base';
export class HotelSearchRequest extends HotelSearchRequestBase {
    constructor(public Name = '',
                public Star = 0,
                public UserRating = 0,
                public CostMin = 0,
                public CostMax = 0,
                public Page = 1,
                public Sorting = 0) {
        super(Name, Star, UserRating, CostMin, CostMax);
    }
}
