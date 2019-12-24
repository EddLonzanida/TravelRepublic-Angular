import { Establishment } from '../dto/establishment';
export class HotelSearchResponse {
    constructor(
        public recordCount = 0,
        public rowsPerPage = 0,
        public items: Establishment[] = []
    ) { }
}
