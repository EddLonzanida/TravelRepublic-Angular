
export class HotelSearchRequestBase {
    constructor(public name = '',
        public star = 0,
        public userRating = 0,
        public costMin = 0,
        public costMax = 0) {
    }
}
