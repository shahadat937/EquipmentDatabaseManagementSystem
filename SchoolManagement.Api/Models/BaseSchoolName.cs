using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class BaseSchoolName
    {
        public BaseSchoolName()
        {
            Brands = new HashSet<Brand>();
            EquipmentCategories = new HashSet<EquipmentCategory>();
            EqupmentNames = new HashSet<EqupmentName>();
            MonthlyReturns = new HashSet<MonthlyReturn>();
            Procurements = new HashSet<Procurement>();
            ShipDrowingAuthorities = new HashSet<ShipDrowing>();
            ShipDrowingBaseNames = new HashSet<ShipDrowing>();
            ShipDrowingBaseSchoolNames = new HashSet<ShipDrowing>();
            ShipEquipmentInfos = new HashSet<ShipEquipmentInfo>();
            ShipInformationAuthorityNavigations = new HashSet<ShipInformation>();
            ShipInformationBaseNames = new HashSet<ShipInformation>();
            ShipInformationBaseSchoolNames = new HashSet<ShipInformation>();
        }

        public int BaseSchoolNameId { get; set; }
        public int? BaseNameId { get; set; }
        public string SchoolName { get; set; }
        public string ShortName { get; set; }
        public string SchoolLogo { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public int? BranchLevel { get; set; }
        public int? FirstLevel { get; set; }
        public int? SecondLevel { get; set; }
        public int? ThirdLevel { get; set; }
        public int? FourthLevel { get; set; }
        public int? FifthLevel { get; set; }
        public string ServerName { get; set; }

        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<EquipmentCategory> EquipmentCategories { get; set; }
        public virtual ICollection<EqupmentName> EqupmentNames { get; set; }
        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }
        public virtual ICollection<Procurement> Procurements { get; set; }
        public virtual ICollection<ShipDrowing> ShipDrowingAuthorities { get; set; }
        public virtual ICollection<ShipDrowing> ShipDrowingBaseNames { get; set; }
        public virtual ICollection<ShipDrowing> ShipDrowingBaseSchoolNames { get; set; }
        public virtual ICollection<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
        public virtual ICollection<ShipInformation> ShipInformationAuthorityNavigations { get; set; }
        public virtual ICollection<ShipInformation> ShipInformationBaseNames { get; set; }
        public virtual ICollection<ShipInformation> ShipInformationBaseSchoolNames { get; set; }
    }
}
