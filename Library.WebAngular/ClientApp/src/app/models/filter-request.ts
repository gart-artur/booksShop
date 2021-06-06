export class FilterRequest {
    skip: number;
    take: number;

    constructor() {
        this.skip = 0;
        this.take = 10;
    }
}
