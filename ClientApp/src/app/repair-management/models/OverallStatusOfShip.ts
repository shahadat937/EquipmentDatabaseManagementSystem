export interface OverallShipStatus{
    statusOfShipId: number;
    baseSchoolNameId: number;
    baseSchoolName: string;
    operationalStatusId: number;
    reasonOfBeingNonOperation: string;
    dateFromNonOperational: Date | string;
    schoolName: string;
    operationalStatus: string;
    menuPosition: number,
    isActive: boolean
}