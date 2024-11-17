export interface MonthlyReturn {
   probableDefectTime: Date,
   monthlyReturnId: number,
   authorityId: number,
   baseNameId: number,
   baseSchoolNameId: number,
   equipmentCategoryId: number,
   equpmentNameId: number,
   reportingMonthId: number,
   returnTypeId: number,
   operationalStatusId: number,
   damageDescription: string,
   presentCondition: string,
   reportingDate: Date,
   timeOfDefect: Date,
   uploadDocument: string,
   remarks: string,
   menuPosition: number,
   isActive: boolean
   equipmentCategory:string
}