export interface Procurement {
   procurementId: number,
   baseSchoolNameId: number,
   schoolName: string,
   groupNameId: number,
   groupName: string
   equpmentNameId: number,
   equpmentName: string,
   controlledId: number,
   controlledName: string,
   fcLcId: number,
   fcLcName: string,
   dgdpNssdId: number,
   budgetCode: string
   financialYearId: number
   dgdpNssdName: string,
   qty: string,
   ePrice: string,
   sentForAIPDate : string,
   aipApprovalDate : string,
   indentSentDate : string,
   tenderFloatedDate : string
   numberOfTenderOpening : string;
   offerReceivedDateAndUpdateEvaluation : string
   sentForContractDate: Date,
   contractSignedDate: Date,
   isActive: boolean,
   remarks: string,
   procurementTenderOpeningDto : any
}