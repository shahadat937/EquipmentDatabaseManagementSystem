export interface Procurement {
   procurementId: number,
   baseSchoolNameId: number,
   schoolName: string,
   procurementMethodId: number,
   envelopeId: number,
   paymentStatusId: number,
   paymentStatus: string,
   procurementTypeId: number,
   procurementType: string,
   groupNameId: number,
   groupName: string
   equpmentNameId: number,
   equpmentName: string,
   controlledId: number,
   controlledName: string,
   fcLcId: number,
   fcLcName: string,
   dgdpNssdId: number,
   dgdpNssdName: string,
   tecId: number,
   tecName: string,
   tenderOpeningDateTypeId: number,
   tenderOpeningDateTypeName: string,
   qty: string,
   ePrice: string,
   sentToDgdpNssdDate: Date,
   tenderOpeningDate: Date,
   offerReceivedDate: Date,
   contractMinSentDate: Date,
   contractMinReceived: Date,
   sentForContractDate: Date,
   contractSignedDate: Date,
   aip: string,
   clarificationToOemSentDate: Date,
   clarificationToOemReceivedDate: Date,
   clarificationToUserSentDate: Date,
   clarificationToUserReceivedDate: Date,
   techTecSentDate: Date,
   techTecReceivedDate: Date,
   minForFoSentDate: Date,
   minForFoReceivedDate: Date,
   sentToDtsDate: Date,
   foReceivedDate: Date,
   foTecSentDate: Date,
   foTecReceivedDate: Date,
   finalContractMinSentDate: Date,
   finalContractMinReceivedDate: Date,
   remarks: string,
   status: number,
   menuPosition: number,
   isActive: boolean,
}