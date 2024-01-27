import {LetterType} from './LetterType';

export interface ILetterTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: LetterType[];
}

export class LetterTypePagination implements ILetterTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: LetterType[] = [];
}
