﻿using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class OperationalStatusOfEquipmentSystem : BaseDomainEntity
    {
        public int OperationalStatusOfEquipmentSystemId { get; set; } 

        public int? NameOfEquipmentId { get; set; }

        public int? StateOfEquipmentId { get; set; } 

        public DateTime? DateOfDefect { get; set; } 

        public string? DurationOfDefect { get; set; }  

        public int? BaseSchoolNameId { get; set; } 

        public string? CausesOfDefect { get; set; }  

        public string? StepsTakenByShipsStaff { get; set; }  

        public string? StepsTakenByBnDockyard { get; set; }  

        public string? StepsTakenByNHQ { get; set; }  

        public string? StepsTakenByOEM { get; set; }  

        public string? PatternNo { get; set; }  

        public bool? IsSparePartsHeldInShip { get; set; } 

        public string? ProcurementStatusOfSpares { get; set; }  

        public string? ImpactOnOtherSystem { get; set; }  

        public string? RequirementOfShip { get; set; }  

        public string? Remarks { get; set; }  

        public string? DescriptionOfDefect { get; set; }  
        
        public bool? IsActive { get; set; }
        public virtual  BaseSchoolName? BaseSchoolName { get; set; }
        //public virtual OperationalStatus OperationalStatus { get; set; }
        public virtual StateOfEquipment? StateOfEquipment { get; set; }

        public virtual EqupmentName? EqupmentName { get; set; }
    }
}
