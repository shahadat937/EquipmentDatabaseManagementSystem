export interface MonthlyReturn {
   authorityName: any;
   returnType: string;
   probableDefectTime: Date | string; // Change to string to handle Date
   monthlyReturnId: number;
   authorityId: number;
   baseNameId: number;
   baseSchoolNameId: number;
   equipmentCategoryId: number;
   equpmentNameId: number;
   reportingMonthId: number;
   returnTypeId: number;
   operationalStatusId: number;
   damageDescription: string;
   presentCondition: string;
   reportingDate: Date | string; // Change to string
   timeOfDefect: Date | string; // Change to string
   uploadDocument: string;
   remarks: string;
   menuPosition: number;
   isActive: boolean;
   equipmentCategory: string;
 }
 