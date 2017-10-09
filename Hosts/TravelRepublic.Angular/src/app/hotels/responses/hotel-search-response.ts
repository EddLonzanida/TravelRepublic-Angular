import { Establishment } from '../dto/establishment';
export class HotelSearchResponse {
    constructor(public RecordCount = 0,
        public RowsPerPage = 0,
        public Establishments: Establishment[] = []) { }
}
