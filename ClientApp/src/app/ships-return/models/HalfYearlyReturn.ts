export interface HalfYearlyReturn {
   halfYearlyReturnId: number,
   baseSchoolNameId:number,
   baseSchoolName : string, // school name is the ship Name
   equipmentCategoryId: number,
   equpmentNameId: number,
   brandId: number,
   halfYearlyRunningTime: string,
   halfYearlyProblem: string,
   inputPowerSupply: string,
   totalRunningTime: string,
   powerSupplyUnit: string,
   remarks: string,
   menuPosition: number,
   isActive: boolean
   equipmentCategory:string,
   isSatisfactory : boolean,
   shipEquipmentInfoId: number,
   manufacturerNameAndAddress: string,
   purpose : string,
   location: string,
   yearOfInstallation : string,
   acquisitionMethodName : string,
   reportingMonthId : number,
   year : number;
}