import {ProjectStatus} from './ProjectStatus';

export interface IProjectStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProjectStatus[];
}

export class ProjectStatusPagination implements IProjectStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProjectStatus[] = [];
}
