
export class HotelSearchRequestBase {
    constructor(public Name = '',
        public Star = 0,
        public UserRating = 0,
        public CostMin = 0,
        public CostMax = 0) {
    }
}
