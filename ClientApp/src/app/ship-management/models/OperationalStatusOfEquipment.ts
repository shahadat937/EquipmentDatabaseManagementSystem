export interface OperationalStatusOfEquipmentSystem {
    operationalStatusOfEquipmentSystemId: number;
    nameOfEquipmentId: number;
    nameOfEquipment: string;
    operationalStatusId: number;
    operationalStatus: string;
    dateOfDefect: Date;
    durationOfDefect: string;
    baseSchoolNameId: number;
    baseSchoolName: string;
    causesOfDefect: string;
    stepsTakenByShipsStaff: string;
    stepsTakenByBnDockyard: string;
    stepsTakenByNHQ: string;
    stepsTakenByOEM: string;
    patternNo: string;
    isSparePartsHeldInShip: boolean;
    procurementStatusOfSpares: string;
    impactOnOtherSystem: string;
    requirementOfShip: string;
    remarks: string;
    descriptionOfDefect: string;
    isActive: boolean;
  }
  