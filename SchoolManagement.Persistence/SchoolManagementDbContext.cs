using SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Doamin;

//using SchoolManagement.Api.Models;

namespace SchoolManagement.Persistence
{
    public class SchoolManagementDbContext : AuditableDbContext
    {
        public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcquisitionMethod>(entity =>
            {

            });

            modelBuilder.Entity<ActionTaken>(entity =>
            {

            });

            modelBuilder.Entity<BookType>(entity =>
            {

            });

            modelBuilder.Entity<BookUserManualBRInfo>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                   .WithMany(p => p.BookUserManualBRInfos)
                   .HasForeignKey(d => d.BaseSchoolNameId)
                   .HasConstraintName("FK_BookUserManualBRInfo_BaseSchoolName");

                entity.HasOne(d => d.BookType)
                  .WithMany(p => p.BookUserManualBRInfos)
                  .HasForeignKey(d => d.BookTypeId)
                  .HasConstraintName("FK_BookUserManualBRInfo_BookType");

            });

            modelBuilder.Entity<Priority>(entity =>
            {

            });

            modelBuilder.Entity<HalfYearlyReturn>(entity =>
            {
                entity.HasOne(d => d.EquipmentCategory)
                      .WithMany(p => p.HalfYearlyReturns)
                      .HasForeignKey(d => d.EquipmentCategoryId)
                      .HasConstraintName("FK_HalfYearlyReturn_EquipmentCategory");

                entity.HasOne(d => d.EqupmentName)
                     .WithMany(p => p.HalfYearlyReturns)
                     .HasForeignKey(d => d.EqupmentNameId)
                     .HasConstraintName("FK_HalfYearlyReturn_EqupmentName");

                entity.HasOne(d => d.Brand)
                     .WithMany(p => p.HalfYearlyReturns)
                     .HasForeignKey(d => d.BrandId)
                     .HasConstraintName("FK_HalfYearlyReturn_Brand");

                entity.HasOne(d => d.BaseSchoolName)
                      .WithMany(p => p.HalfYearlyReturns)
                      .HasForeignKey(d => d.BaseSchoolNameId)
                      .HasConstraintName("FK_HalfYearlyReturn_BaseSchoolName");
                entity.HasOne(d => d.ShipEquipmentInfo)
                     .WithMany(p => p.HalfYearlyReturns)
                     .HasForeignKey(d => d.ShipEquipmentInfoId)
                     .HasConstraintName("FK_HalfYearlyReturn_ShipEquipmentInfo");
                entity.HasOne(d => d.ReportingMonth)
                    .WithMany(p => p.HalfYearlyReturns)
                    .HasForeignKey(d => d.ReportingMonthId)
                    .HasConstraintName("FK_HalfYearlyReturn_ReportingMonth");



            });

            modelBuilder.Entity<HalfYearlyRunningTime>(entity =>
            {

            });


            modelBuilder.Entity<DailyWorkState>(entity =>
            {
                entity.HasOne(d => d.LetterType)
                  .WithMany(p => p.DailyWorkStates)
                  .HasForeignKey(d => d.LetterTypeId)
                  .HasConstraintName("FK_DailyWorkState_LetterType");

                entity.HasOne(d => d.DealingOfficer)
                 .WithMany(p => p.DailyWorkStates)
                 .HasForeignKey(d => d.DealingOfficerId)
                 .HasConstraintName("FK_DailyWorkState_DealingOfficer");

                entity.HasOne(d => d.ActionTaken)
                 .WithMany(p => p.DailyWorkStates)
                 .HasForeignKey(d => d.ActionTakenId)
                 .HasConstraintName("FK_DailyWorkState_ActionTaken");

                entity.HasOne(d => d.Priority)
                 .WithMany(p => p.DailyWorkStates)
                 .HasForeignKey(d => d.PriorityId)
                 .HasConstraintName("FK_DailyWorkState_Priority");


            });

            modelBuilder.Entity<BaseSchoolName>(entity =>
            {

                //entity.HasOne(d => d.BaseName)
                //    .WithMany(p => p.BaseSchoolNames)
                //    .HasForeignKey(d => d.BaseNameId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_BaseSchoolName_BaseName");

            });

            modelBuilder.Entity<CodeValue>(entity =>
            {
                entity.HasOne(d => d.CodeValueType)
                    .WithMany(p => p.CodeValues)
                    .HasForeignKey(d => d.CodeValueTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodeValue_CodeValueType");
            });

            modelBuilder.Entity<CodeValueType>(entity =>
            {

            });

            modelBuilder.Entity<Envelope>(entity =>
            {

            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Features)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feature_Module");
            });

            modelBuilder.Entity<Module>(entity =>
            {

            });

            modelBuilder.Entity<RoleFeature>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FeatureKey });

                entity.ToTable("RoleFeature");

            });

            modelBuilder.Entity<Organization>(entity =>
            {

            });

            modelBuilder.Entity<ShipType>(entity =>
            {

            });

            modelBuilder.Entity<ShipDrowing>(entity =>
            {
                entity.HasOne(d => d.Authority)
                    .WithMany(p => p.ShipDrowingAuthorities)
                    .HasForeignKey(d => d.AuthorityId)
                    .HasConstraintName("FK_ShipDrowing_BaseSchoolNameAuthority");

                entity.HasOne(d => d.BaseName)
                    .WithMany(p => p.ShipDrowingBaseNames)
                    .HasForeignKey(d => d.BaseNameId)
                    .HasConstraintName("FK_ShipDrowing_BaseSchoolNameBaseName");

                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.ShipDrowingBaseSchoolNames)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_ShipDrowing_ShipDrowingBaseSchool");
            });
            modelBuilder.Entity<OperationalStatus>(entity =>
            {

            });

            modelBuilder.Entity<Sqn>(entity =>
            {

            });


            modelBuilder.Entity<GroupName>(entity =>
            {

            });

            modelBuilder.Entity<QuarterlyReturnNoType>(entity =>
            {

            });

            modelBuilder.Entity<RunningTime>(entity =>
            {

            });

            modelBuilder.Entity<ReportType>(entity =>
            {

            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {

            });

            modelBuilder.Entity<ProcurementType>(entity =>
            {

            });

            modelBuilder.Entity<Procurement>(entity =>
            {

                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_Procurement_BaseSchoolName");

                entity.HasOne(d => d.ProcurementMethod)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.ProcurementMethodId)
                    .HasConstraintName("FK_Procurement_ProcurementMethod");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_Procurement_PaymentStatus");

                entity.HasOne(d => d.ProcurementType)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.ProcurementTypeId)
                    .HasConstraintName("FK_Procurement_ProcurementType");

                entity.HasOne(d => d.GroupName)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.GroupNameId)
                    .HasConstraintName("FK_Procurement_GroupName");


                entity.HasOne(d => d.EqupmentName)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.EqupmentNameId)
                    .HasConstraintName("FK_Procurement_EqupmentName");

                entity.HasOne(d => d.FcLc)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.FcLcId)
                    .HasConstraintName("FK_Procurement_FcLc");

                entity.HasOne(d => d.DgdpNssd)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.DgdpNssdId)
                    .HasConstraintName("FK_Procurement_DgdpNssd");

                entity.HasOne(d => d.Tec)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.TecId)
                    .HasConstraintName("FK_Procurement_Tec");

                entity.HasOne(d => d.TenderOpeningDateType)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.TenderOpeningDateTypeId)
                    .HasConstraintName("FK_Procurement_TenderOpeningDateType");

                entity.HasOne(d => d.Controlled)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.ControlledId)
                    .HasConstraintName("FK_Procurement_Controlled");

                entity.HasOne(d => d.Envelope)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.EnvelopeId)
                    .HasConstraintName("FK_Procurement_Envelope");

            });

            modelBuilder.Entity<ProcurementMethod>(entity =>
            {

            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {

            });

            modelBuilder.Entity<DealingOfficer>(entity =>
            {

            });

            modelBuilder.Entity<DgdpNssd>(entity =>
            {

            });

            modelBuilder.Entity<FcLc>(entity =>
            {

            });

            modelBuilder.Entity<Controlled>(entity =>
            {

            });

            modelBuilder.Entity<ShipEquipmentInfo>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_ShipEquipmentInfo_BaseSchoolName");

                entity.HasOne(d => d.AcquisitionMethod)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.AcquisitionMethodId)
                    .HasConstraintName("FK_ShipEquipmentInfo_AcquisitionMethod");

                //entity.HasOne(d => d.Brand)
                //    .WithMany(p => p.ShipEquipmentInfos)
                //    .HasForeignKey(d => d.BrandId)
                //    .HasConstraintName("FK_ShipEquipmentInfo_Brand");

                entity.HasOne(d => d.EquipmentCategory)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.EquipmentCategoryId)
                    .HasConstraintName("FK_ShipEquipmentInfo_EquipmentCategory");

                entity.HasOne(d => d.EqupmentName)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.EqupmentNameId)
                    .HasConstraintName("FK_ShipEquipmentInfo_EqupmentName");

                entity.HasOne(d => d.EquipmentType)
                   .WithMany(p => p.ShipEquipmentInfos)
                   .HasForeignKey(d => d.EquipmentTypeId)
                   .HasConstraintName("FK_ShipEquipmentInfo_EquipmentType");

                entity.HasOne(d => d.StateOfEquipment)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.StateOfEquipmentId)
                    .HasConstraintName("FK_ShipEquipmentInfo_StateOfEquipment");


            });


            modelBuilder.Entity<ShipInformation>(entity =>
            {
                entity.HasOne(d => d.BaseName)
                  .WithMany(p => p.ShipInformationBaseNames)
                  .HasForeignKey(d => d.BaseNameId)
                  .HasConstraintName("FK_ShipInformation_BaseSchoolNameBaseName");

                entity.HasOne(d => d.AuthorityNavigation)
                 .WithMany(p => p.ShipInformationAuthorityNavigations)
                 .HasForeignKey(d => d.AuthorityId)
                 .HasConstraintName("FK_ShipInformation_BaseSchoolNameAuthority");

                entity.HasOne(d => d.BaseSchoolName)
                  .WithMany(p => p.ShipInformations)
                  .HasForeignKey(d => d.BaseSchoolNameId)
                  .HasConstraintName("FK_ShipInformation_BaseSchoolName");

                entity.HasOne(d => d.OperationalStatus)
                    .WithMany(p => p.ShipInformations)
                    .HasForeignKey(d => d.OperationalStatusId)
                    .HasConstraintName("FK_ShipInformation_OperationalStatus");

                entity.HasOne(d => d.ShipType)
                    .WithMany(p => p.ShipInformations)
                    .HasForeignKey(d => d.ShipTypeId)
                    .HasConstraintName("FK_ShipInformation_ShipType");

                entity.HasOne(d => d.Sqn)
                    .WithMany(p => p.ShipInformations)
                    .HasForeignKey(d => d.SqnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipInformation_Sqn");
            });
            modelBuilder.Entity<EqupmentName>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.EqupmentNames)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_EqupmentName_BaseSchoolName");

                entity.HasOne(d => d.EquipmentCategory)
                    .WithMany(p => p.EqupmentNames)
                    .HasForeignKey(d => d.EquipmentCategoryId)
                    .HasConstraintName("FK_EqupmentName_EquipmentCategory");
            });
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_Brand_BaseSchoolName");
            });

            modelBuilder.Entity<EquipmentCategory>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.EquipmentCategories)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_EquipmentCategory_BaseSchoolName");

                entity.HasOne(d => d.GroupName)
                    .WithMany(p => p.EquipmentCategories)
                    .HasForeignKey(d => d.GroupNameId)
                    .HasConstraintName("FK_EquipmentCategory_GroupName");
            });

            modelBuilder.Entity<EquipmentCategory>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.EquipmentCategories)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_EquipmentCategory_BaseSchoolName");

                entity.HasOne(d => d.GroupName)
                    .WithMany(p => p.EquipmentCategories)
                    .HasForeignKey(d => d.GroupNameId)
                    .HasConstraintName("FK_EquipmentCategory_GroupName");
            });

            modelBuilder.Entity<EquipmentType>(entity =>
            {

            });

            modelBuilder.Entity<TenderOpeningDateType>(entity =>
            {

            });

            modelBuilder.Entity<Tec>(entity =>
            {

            });

            modelBuilder.Entity<LetterType>(entity =>
            {

            });
            modelBuilder.Entity<MonthlyReturn>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.MonthlyReturns)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_MonthlyReturn_BaseSchoolName");

                entity.HasOne(d => d.EquipmentCategory)
                    .WithMany(p => p.MonthlyReturns)
                    .HasForeignKey(d => d.EquipmentCategoryId)
                    .HasConstraintName("FK_MonthlyReturn_EquipmentCategory");

                entity.HasOne(d => d.EqupmentName)
                    .WithMany(p => p.MonthlyReturns)
                    .HasForeignKey(d => d.EqupmentNameId)
                    .HasConstraintName("FK_MonthlyReturn_EqupmentName");

                entity.HasOne(d => d.OperationalStatus)
                    .WithMany(p => p.MonthlyReturns)
                    .HasForeignKey(d => d.OperationalStatusId)
                    .HasConstraintName("FK_MonthlyReturn_OperationalStatus");

                entity.HasOne(d => d.ReportingMonth)
                    .WithMany(p => p.MonthlyReturns)
                    .HasForeignKey(d => d.ReportingMonthId)
                    .HasConstraintName("FK_MonthlyReturn_ReportingMonth");

                entity.HasOne(d => d.ReturnType)
                    .WithMany(p => p.MonthlyReturns)
                    .HasForeignKey(d => d.ReturnTypeId)
                    .HasConstraintName("FK_MonthlyReturn_ReturnType");
            });
            modelBuilder.Entity<YearlyReturn>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.YearlyReturns)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_YearlyReturn_BaseSchoolName");
            });
            modelBuilder.Entity<ReportingMonth>(entity =>
            {

            });

            modelBuilder.Entity<ReturnType>(entity =>
            {

            });

            modelBuilder.Entity<OperationalState>(entity =>
            {
                entity.HasOne(d => d.EquipmentName)
                    .WithMany(p => p.OperationalStates)
                    .HasForeignKey(d => d.EquipmentNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OperationalState_EqupmentName");
            });

            modelBuilder.Entity<OperationalStatusOfEquipmentSystem>(entity =>
            {
                entity.HasOne(d => d.BaseSchoolName)
                .WithMany(p => p.OperationalStatusOfEquipmentSystems)
                .HasForeignKey(d => d.BaseSchoolNameId)
                .HasConstraintName("FK_OperationalStatusOfEquipmentSystem_BaseSchoolName");

                entity.HasOne(d => d.OperationalStatus)
                .WithMany(p => p.OperationalStatusOfEquipmentSystems)
                .HasForeignKey(d => d.OperationalStatusId)
                .HasConstraintName("FK_OperationalStatusOfEquipmentSystem_OperationalStatus");

                entity.HasOne(d => d.EqupmentName)
                .WithMany(p => p.OperationalStatusOfEquipmentSystems)
                .HasForeignKey(d => d.NameOfEquipmentId)
                .HasConstraintName("FK_OperationalStatusOfEquipmentSystem_EquipmentName");
            });

        }

        public virtual DbSet<AcquisitionMethod> AcquisitionMethod { get; set; } = null!;
        public virtual DbSet<ActionTaken> ActionTaken { get; set; } = null!;
        public virtual DbSet<BaseSchoolName> BaseSchoolName { get; set; } = null!;
        public virtual DbSet<CodeValue> CodeValue { get; set; } = null!;
        public virtual DbSet<CodeValueType> CodeValueType { get; set; } = null!;
        public virtual DbSet<DailyWorkState> DailyWorkState { get; set; } = null!;
        public virtual DbSet<Envelope> Envelope { get; set; } = null!;
        public virtual DbSet<Feature> Feature { get; set; } = null!;
        public virtual DbSet<Module> Module { get; set; } = null!;
        public virtual DbSet<RoleFeature> RoleFeature { get; set; } = null!;
        public virtual DbSet<ShipType> ShipType { get; set; }
        public virtual DbSet<Sqn> Sqn { get; set; }
        public virtual DbSet<QuarterlyReturnNoType> QuarterlyReturnNoType { get; set; }
        public virtual DbSet<RunningTime> RunningTime { get; set; }
        public virtual DbSet<ReportType> ReportType { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<ProcurementType> ProcurementType { get; set; }
        public virtual DbSet<Procurement> Procurement { get; set; }
        public virtual DbSet<ProcurementMethod> ProcurementMethod { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<DealingOfficer> DealingOfficer { get; set; }
        public virtual DbSet<DgdpNssd> DgdpNssd { get; set; }
        public virtual DbSet<FcLc> FcLc { get; set; }
        public virtual DbSet<Controlled> Controlled { get; set; }
        public virtual DbSet<OperationalStatus> OperationalStatus { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<ShipEquipmentInfo> ShipEquipmentInfo { get; set; }
        public virtual DbSet<ShipInformation> ShipInformation { get; set; }
        public virtual DbSet<StateOfEquipment> StateOfEquipment { get; set; }
        public virtual DbSet<EqupmentName> EqupmentName { get; set; }
        public virtual DbSet<EquipmentType> EquipmentType { get; set; }
        public virtual DbSet<GroupName> GroupName { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategory { get; set; }
        public virtual DbSet<TenderOpeningDateType> TenderOpeningDateType { get; set; }
        public virtual DbSet<Tec> Tec { get; set; }
        public virtual DbSet<LetterType> LetterType { get; set; }
        public virtual DbSet<HalfYearlyReturn> HalfYearlyReturn { get; set; }
        public virtual DbSet<HalfYearlyRunningTime> HalfYearlyRunningTime { get; set; }
        public virtual DbSet<ShipDrowing> ShipDrowing { get; set; }
        public virtual DbSet<ReportingMonth> ReportingMonth { get; set; }
        public virtual DbSet<ReturnType> ReturnType { get; set; }
        public virtual DbSet<MonthlyReturn> MonthlyReturn { get; set; }
        public virtual DbSet<OperationalState> OperationalState { get; set; }
        public virtual DbSet<YearlyReturn> YearlyReturn { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<OperationalStatusOfEquipmentSystem> OperationalStatusOfEquipmentSystem { get; set; }
    }
}
