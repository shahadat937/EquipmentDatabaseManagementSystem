import {Project} from './Project';

export interface IProjectPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Project[];
}

export class ProjectPagination implements IProjectPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Project[] = [];
}
