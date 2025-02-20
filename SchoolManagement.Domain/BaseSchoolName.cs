using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class BaseSchoolName : BaseDomainEntity
    {
        public BaseSchoolName()
        {
            EqupmentNames = new HashSet<EqupmentName>();
            EquipmentCategories = new HashSet<EquipmentCategory>();
            Brands = new HashSet<Brand>();
            ShipEquipmentInfos = new HashSet<ShipEquipmentInfo>();
            ShipInformations = new HashSet<ShipInformation>();
            ShipInformationAuthorityNavigations = new HashSet<ShipInformation>();
            ShipInformationBaseNames = new HashSet<ShipInformation>();
            Procurements = new HashSet<Procurement>();
            ShipDrowingAuthorities = new HashSet<ShipDrowing>();
            ShipDrowingBaseNames = new HashSet<ShipDrowing>();
            ShipDrowingBaseSchoolNames = new HashSet<ShipDrowing>();
            MonthlyReturns = new HashSet<MonthlyReturn>();
            BookUserManualBRInfos = new HashSet<BookUserManualBRInfo>();
            HalfYearlyReturns = new HashSet<HalfYearlyReturn>();
            QuarterlyReturns = new HashSet<QuarterlyReturn>();
            OperationalStatusOfEquipmentSystems = new HashSet<OperationalStatusOfEquipmentSystem>();
        }
        public int BaseSchoolNameId { get; set; }
        public int? BaseNameId { get; set; }
        public string? SchoolName { get; set; }
        public string? ShortName { get; set; }
        public string? SchoolLogo { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public int? BranchLevel { get; set; }
        public int? FirstLevel { get; set; }
        public int? SecondLevel { get; set; }
        public int? ThirdLevel { get; set; }
        public int? FourthLevel { get; set; }
        public int? FifthLevel { get; set; }
        public string? ServerName { get; set; }
        public virtual ICollection<EqupmentName> EqupmentNames { get; set; }
        public virtual ICollection<EquipmentCategory> EquipmentCategories { get; set; }
        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<HalfYearlyReturn> HalfYearlyReturns { get; set; }
        public virtual ICollection<Procurement> Procurements { get; set; }
        public virtual ICollection<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
        public virtual ICollection<ShipInformation> ShipInformations { get; set; }
        public virtual ICollection<ShipInformation> ShipInformationAuthorityNavigations { get; set; }
        public virtual ICollection<ShipInformation> ShipInformationBaseNames { get; set; }
        public virtual ICollection<ShipDrowing> ShipDrowingAuthorities { get; set; }
        public virtual ICollection<ShipDrowing> ShipDrowingBaseNames { get; set; }
        public virtual ICollection<ShipDrowing> ShipDrowingBaseSchoolNames { get; set; }
        public virtual ICollection<BookUserManualBRInfo> BookUserManualBRInfos { get; set; }
        public virtual ICollection<YearlyReturn> YearlyReturns { get; set; }
        public virtual ICollection<QuarterlyReturn> QuarterlyReturns { get; set; }
        public virtual ICollection<StatusOfShip> StatusOfShips { get; set; }
        public virtual ICollection<OperationalStatusOfEquipmentSystem> OperationalStatusOfEquipmentSystems { get; set; }
    }
}
