using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class OperationalStatus : BaseDomainEntity
    {
        public OperationalStatus()
        {
            ShipInformations = new HashSet<ShipInformation>();
            MonthlyReturns = new HashSet<MonthlyReturn>();
            //OperationalStatusOfEquipmentSystems = new HashSet<OperationalStatusOfEquipmentSystem>();
        }
        public int OperationalStatusId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ShipInformation> ShipInformations { get; set; }
        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }
        //public virtual ICollection<OperationalStatusOfEquipmentSystem> OperationalStatusOfEquipmentSystems { get; set; }
    }
}
