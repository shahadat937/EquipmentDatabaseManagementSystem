import {AcquisitionMethod} from './AcquisitionMethod';

export interface IAcquisitionMethodPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: AcquisitionMethod[];
}

export class AcquisitionMethodPagination implements IAcquisitionMethodPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: AcquisitionMethod[] = [];
}
