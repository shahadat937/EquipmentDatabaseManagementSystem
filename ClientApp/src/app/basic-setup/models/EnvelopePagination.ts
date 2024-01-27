import {Envelope} from './Envelope';

export interface IEnvelopePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Envelope[];
}

export class EnvelopePagination implements IEnvelopePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Envelope[] = [];
}
