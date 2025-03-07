﻿using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class GroupName : BaseDomainEntity
    {
        public GroupName()
        {
            EquipmentCategories = new HashSet<EquipmentCategory>();
            Procurements = new HashSet<Procurement>();
        }

        public int GroupNameId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EquipmentCategory> EquipmentCategories { get; set; }
        public virtual ICollection<Procurement> Procurements { get; set; }
    }
}
