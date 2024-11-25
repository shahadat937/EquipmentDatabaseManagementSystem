using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolManagement.Domain;

namespace SchoolManagement.Api.Models
{
    public partial class EDMSdbContext : DbContext
    {
        public EDMSdbContext()
        {
        }

        public EDMSdbContext(DbContextOptions<EDMSdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcquisitionMethod> AcquisitionMethods { get; set; }
        public virtual DbSet<ActionTaken> ActionTakens { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<BaseName> BaseNames { get; set; }
        public virtual DbSet<BaseSchoolName> BaseSchoolNames { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CodeValue> CodeValues { get; set; }
        public virtual DbSet<CodeValueType> CodeValueTypes { get; set; }
        public virtual DbSet<Controlled> Controlleds { get; set; }
        public virtual DbSet<DailyWorkState> DailyWorkStates { get; set; }
        public virtual DbSet<DealingOfficer> DealingOfficers { get; set; }
        public virtual DbSet<DgdpNssd> DgdpNssds { get; set; }
        public virtual DbSet<Envelope> Envelopes { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }
        public virtual DbSet<EqupmentName> EqupmentNames { get; set; }
        public virtual DbSet<FcLc> FcLcs { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<GroupName> GroupNames { get; set; }
        public virtual DbSet<LetterType> LetterTypes { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<MonthlyReturn> MonthlyReturns { get; set; }
        public virtual DbSet<OperationalState> OperationalStates { get; set; }
        public virtual DbSet<OperationalStatus> OperationalStatuses { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Procurement> Procurements { get; set; }
        public virtual DbSet<ProcurementMethod> ProcurementMethods { get; set; }
        public virtual DbSet<ProcurementType> ProcurementTypes { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public virtual DbSet<QuarterlyReturnNoType> QuarterlyReturnNoTypes { get; set; }
        public virtual DbSet<ReportType> ReportTypes { get; set; }
        public virtual DbSet<ReportingMonth> ReportingMonths { get; set; }
        public virtual DbSet<ReturnType> ReturnTypes { get; set; }
        public virtual DbSet<RoleFeature> RoleFeatures { get; set; }
        public virtual DbSet<RunningTime> RunningTimes { get; set; }
        public virtual DbSet<ShipDrowing> ShipDrowings { get; set; }
        public virtual DbSet<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
        public virtual DbSet<ShipInformation> ShipInformations { get; set; }
        public virtual DbSet<ShipType> ShipTypes { get; set; }
        public virtual DbSet<Sqn> Sqns { get; set; }
        public virtual DbSet<StateOfEquipment> StateOfEquipments { get; set; }
        public virtual DbSet<Tec> Tecs { get; set; }
        public virtual DbSet<TenderOpeningDateType> TenderOpeningDateTypes { get; set; }
        public virtual DbSet<Domain.StatusOfShip> StatusOfShips { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=114.134.95.235,1434;Database=EDMSdb;user id=sa;password=B@ngl@d3sh;Trusted_Connection=false;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcquisitionMethod>(entity =>
            {
                entity.ToTable("AcquisitionMethod");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<ActionTaken>(entity =>
            {
                entity.ToTable("ActionTaken");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.BranchId).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.InActiveDate).HasColumnType("datetime");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Pno).HasColumnName("PNo");

                entity.Property(e => e.RoleName).HasMaxLength(100);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BaseName>(entity =>
            {
                entity.ToTable("BaseName");

                entity.Property(e => e.BaseLogo).HasMaxLength(450);

                entity.Property(e => e.BaseNames)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<BaseSchoolName>(entity =>
            {
                entity.ToTable("BaseSchoolName");

                entity.Property(e => e.Address)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Cellphone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SchoolLogo).HasMaxLength(450);

                entity.Property(e => e.SchoolName).HasMaxLength(450);

                entity.Property(e => e.ServerName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName).HasMaxLength(450);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);

                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_Brand_BaseSchoolName");
            });

            modelBuilder.Entity<CodeValue>(entity =>
            {
                entity.ToTable("CodeValue");

                entity.Property(e => e.AdditonalValue)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DisplayCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeValue)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeValueType)
                    .WithMany(p => p.CodeValues)
                    .HasForeignKey(d => d.CodeValueTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodeValue_CodeValueType");
            });

            modelBuilder.Entity<CodeValueType>(entity =>
            {
                entity.ToTable("CodeValueType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Controlled>(entity =>
            {
                entity.ToTable("Controlled");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<DailyWorkState>(entity =>
            {
                entity.ToTable("DailyWorkState");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DeadLine).HasColumnType("datetime");

                entity.Property(e => e.DealingStaff).HasMaxLength(450);

                entity.Property(e => e.FileUpload).HasMaxLength(450);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LetterNo).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.Subject).HasMaxLength(450);

                entity.Property(e => e.WorkFrom).HasMaxLength(450);

                entity.HasOne(d => d.ActionTaken)
                    .WithMany(p => p.DailyWorkStates)
                    .HasForeignKey(d => d.ActionTakenId)
                    .HasConstraintName("FK_DailyWorkState_ActionTaken");

                entity.HasOne(d => d.DealingOfficer)
                    .WithMany(p => p.DailyWorkStates)
                    .HasForeignKey(d => d.DealingOfficerId)
                    .HasConstraintName("FK_DailyWorkState_DealingOfficer");

                entity.HasOne(d => d.LetterType)
                    .WithMany(p => p.DailyWorkStates)
                    .HasForeignKey(d => d.LetterTypeId)
                    .HasConstraintName("FK_DailyWorkState_LetterType");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.DailyWorkStates)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK_DailyWorkState_Priority");
            });

            modelBuilder.Entity<DealingOfficer>(entity =>
            {
                entity.ToTable("DealingOfficer");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<DgdpNssd>(entity =>
            {
                entity.ToTable("DgdpNssd");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<Envelope>(entity =>
            {
                entity.ToTable("Envelope");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<EquipmentCategory>(entity =>
            {
                entity.ToTable("EquipmentCategory");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);

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
                entity.ToTable("EquipmentType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<EqupmentName>(entity =>
            {
                entity.ToTable("EqupmentName");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);

                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.EqupmentNames)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_EqupmentName_BaseSchoolName");

                entity.HasOne(d => d.EquipmentCategory)
                    .WithMany(p => p.EqupmentNames)
                    .HasForeignKey(d => d.EquipmentCategoryId)
                    .HasConstraintName("FK_EqupmentName_EquipmentCategory");
            });

            modelBuilder.Entity<FcLc>(entity =>
            {
                entity.ToTable("FcLc");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.ToTable("Feature");

                entity.Property(e => e.Class).HasMaxLength(250);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FeatureName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.GroupTitle).HasMaxLength(250);

                entity.Property(e => e.Icon).HasMaxLength(250);

                entity.Property(e => e.IconName).HasMaxLength(250);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Features)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feature_Module");
            });

            modelBuilder.Entity<GroupName>(entity =>
            {
                entity.ToTable("GroupName");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<LetterType>(entity =>
            {
                entity.ToTable("LetterType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);

                entity.Property(e => e.Status).HasMaxLength(450);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module");

                entity.Property(e => e.Class).HasMaxLength(250);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.GroupTitle).HasMaxLength(250);

                entity.Property(e => e.Icon).HasMaxLength(250);

                entity.Property(e => e.IconName).HasMaxLength(250);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModuleName).HasMaxLength(450);

                entity.Property(e => e.Title).HasMaxLength(450);
            });

            modelBuilder.Entity<MonthlyReturn>(entity =>
            {
                entity.ToTable("MonthlyReturn");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DamageDescription).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PresentCondition).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ReportingDate).HasColumnType("datetime");

                entity.Property(e => e.TimeOfDefect).HasColumnType("datetime");

                entity.Property(e => e.UploadDocument).HasMaxLength(450);

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


            modelBuilder.Entity<Domain.StatusOfShip>(entity =>
            {

                     entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.StatusOfShips)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_StatusOfShip_BaseSchoolName");

            });

            modelBuilder.Entity<OperationalState>(entity =>
            {
                entity.ToTable("OperationalState");

                entity.Property(e => e.CausesOfDefect).HasMaxLength(450);

                entity.Property(e => e.DateOfDefect).HasColumnType("datetime");

                entity.Property(e => e.DescriptionOfDefect).HasMaxLength(450);

                entity.Property(e => e.DurationOfDefect).HasMaxLength(450);

                entity.Property(e => e.ImpactOnOtherSyatem).HasMaxLength(450);

                entity.Property(e => e.PartNo).HasMaxLength(450);

                entity.Property(e => e.ProcurementStatusOfSpare).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.RequirmentOfTheShip).HasMaxLength(450);

                entity.Property(e => e.StepTakenByDockyard).HasMaxLength(450);

                entity.Property(e => e.StepTakenByNhq).HasMaxLength(450);

                entity.Property(e => e.StepTakenByOem).HasMaxLength(450);

                entity.Property(e => e.StepsTakenByShipsStaff).HasMaxLength(450);

                entity.HasOne(d => d.EquipmentName)
                    .WithMany(p => p.OperationalStates)
                    .HasForeignKey(d => d.EquipmentNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OperationalState_EqupmentName");
            });

            modelBuilder.Entity<OperationalStatus>(entity =>
            {
                entity.ToTable("OperationalStatus");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.ToTable("PaymentStatus");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<Procurement>(entity =>
            {
                entity.ToTable("Procurement");

                entity.Property(e => e.Aip)
                    .HasMaxLength(450)
                    .HasColumnName("AIP");

                entity.Property(e => e.ClarificationToOemReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.ClarificationToOemSentDate).HasColumnType("datetime");

                entity.Property(e => e.ClarificationToUserReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.ClarificationToUserSentDate).HasColumnType("datetime");

                entity.Property(e => e.ContractMinReceived).HasColumnType("datetime");

                entity.Property(e => e.ContractMinSentDate).HasColumnType("datetime");

                entity.Property(e => e.ContractSignedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Eprice).HasColumnName("EPrice");

                entity.Property(e => e.FinalContractMinReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.FinalContractMinSentDate).HasColumnType("datetime");

                entity.Property(e => e.FoReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.FoTecReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.FoTecSentDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MinForFoReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.MinForFoSentDate).HasColumnType("datetime");

                entity.Property(e => e.OfferReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.SentForContractDate).HasColumnType("datetime");

                entity.Property(e => e.SentToDgdpNssdDate).HasColumnType("datetime");

                entity.Property(e => e.SentToDtsDate).HasColumnType("datetime");

                entity.Property(e => e.TechTecReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.TechTecSentDate).HasColumnType("datetime");

                entity.Property(e => e.TenderOpeningDate).HasColumnType("datetime");

                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_Procurement_BaseSchoolName");

                entity.HasOne(d => d.Controlled)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.ControlledId)
                    .HasConstraintName("FK_Procurement_Controlled");

                entity.HasOne(d => d.DgdpNssd)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.DgdpNssdId)
                    .HasConstraintName("FK_Procurement_DgdpNssd");

                entity.HasOne(d => d.Envelope)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.EnvelopeId)
                    .HasConstraintName("FK_Procurement_Envelope");

                entity.HasOne(d => d.EqupmentName)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.EqupmentNameId)
                    .HasConstraintName("FK_Procurement_EqupmentName");

                entity.HasOne(d => d.FcLc)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.FcLcId)
                    .HasConstraintName("FK_Procurement_FcLc");

                entity.HasOne(d => d.GroupName)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.GroupNameId)
                    .HasConstraintName("FK_Procurement_GroupName");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_Procurement_PaymentStatus");

                entity.HasOne(d => d.ProcurementMethod)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.ProcurementMethodId)
                    .HasConstraintName("FK_Procurement_ProcurementMethod");

                entity.HasOne(d => d.ProcurementType)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.ProcurementTypeId)
                    .HasConstraintName("FK_Procurement_ProcurementType");

                entity.HasOne(d => d.Tec)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.TecId)
                    .HasConstraintName("FK_Procurement_Tec");

                entity.HasOne(d => d.TenderOpeningDateType)
                    .WithMany(p => p.Procurements)
                    .HasForeignKey(d => d.TenderOpeningDateTypeId)
                    .HasConstraintName("FK_Procurement_TenderOpeningDateType");
            });

            modelBuilder.Entity<ProcurementMethod>(entity =>
            {
                entity.ToTable("ProcurementMethod");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<ProcurementType>(entity =>
            {
                entity.ToTable("ProcurementType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {
                entity.ToTable("ProjectStatus");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<QuarterlyReturnNoType>(entity =>
            {
                entity.ToTable("QuarterlyReturnNoType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<ReportType>(entity =>
            {
                entity.ToTable("ReportType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<ReportingMonth>(entity =>
            {
                entity.ToTable("ReportingMonth");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<ReturnType>(entity =>
            {
                entity.ToTable("ReturnType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<RoleFeature>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FeatureKey })
                    .HasName("PK_Company.RoleFeature");

                entity.ToTable("RoleFeature");
            });

            modelBuilder.Entity<RunningTime>(entity =>
            {
                entity.ToTable("RunningTime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<ShipDrowing>(entity =>
            {
                entity.ToTable("ShipDrowing");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FileUpload).HasMaxLength(550);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);

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

            modelBuilder.Entity<ShipEquipmentInfo>(entity =>
            {
                entity.ToTable("ShipEquipmentInfo");

                entity.Property(e => e.Accuracy).HasMaxLength(450);

                entity.Property(e => e.AccuracyOfTemperature).HasMaxLength(450);

                entity.Property(e => e.Ah)
                    .HasMaxLength(450)
                    .HasColumnName("AH");

                entity.Property(e => e.AirPressureCapacity).HasMaxLength(450);

                entity.Property(e => e.AirVelocityUnit).HasMaxLength(450);

                entity.Property(e => e.AmmonationType).HasMaxLength(450);

                entity.Property(e => e.Amps).HasMaxLength(450);

                entity.Property(e => e.AudioableRange).HasMaxLength(450);

                entity.Property(e => e.AverageWindSpeedMeasuring).HasMaxLength(450);

                entity.Property(e => e.Avrbrand)
                    .HasMaxLength(450)
                    .HasColumnName("AVRBrand");

                entity.Property(e => e.Avrmodel)
                    .HasMaxLength(450)
                    .HasColumnName("AVRModel");

                entity.Property(e => e.BearingIndication).HasMaxLength(450);

                entity.Property(e => e.Btu)
                    .HasMaxLength(450)
                    .HasColumnName("BTU");

                entity.Property(e => e.Capacity).HasMaxLength(450);

                entity.Property(e => e.Cell).HasMaxLength(450);

                entity.Property(e => e.Channel).HasMaxLength(450);

                entity.Property(e => e.Colour).HasMaxLength(450);

                entity.Property(e => e.Composition).HasMaxLength(450);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DataCorrection).HasMaxLength(450);

                entity.Property(e => e.DataInterface).HasMaxLength(450);

                entity.Property(e => e.DataOutput).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DefaultChannel).HasMaxLength(450);

                entity.Property(e => e.DefectDescription).HasMaxLength(450);

                entity.Property(e => e.Dgpsinput)
                    .HasMaxLength(450)
                    .HasColumnName("DGPSInput");

                entity.Property(e => e.Diameter).HasMaxLength(450);

                entity.Property(e => e.DisplayMinimumSourndingDepth).HasMaxLength(450);

                entity.Property(e => e.DisplaySize).HasMaxLength(450);

                entity.Property(e => e.DisplayType).HasMaxLength(450);

                entity.Property(e => e.DistanceRunRange).HasMaxLength(450);

                entity.Property(e => e.Ebl)
                    .HasMaxLength(450)
                    .HasColumnName("EBL");

                entity.Property(e => e.EffectiveFiringRange).HasMaxLength(450);

                entity.Property(e => e.EffectiveRange).HasMaxLength(450);

                entity.Property(e => e.Efficiency).HasMaxLength(450);

                entity.Property(e => e.ExpectedLeftOverLifeTime).HasMaxLength(450);

                entity.Property(e => e.Filterandwidth).HasMaxLength(450);

                entity.Property(e => e.Frequency).HasMaxLength(450);

                entity.Property(e => e.FrequencyPowerSupplyVoltage).HasMaxLength(450);

                entity.Property(e => e.FrequencyRange).HasMaxLength(450);

                entity.Property(e => e.GyroCompassFollowUpRate).HasMaxLength(450);

                entity.Property(e => e.HeadingDisplay).HasMaxLength(450);

                entity.Property(e => e.HorizontalDetectionRange).HasMaxLength(450);

                entity.Property(e => e.Inch).HasMaxLength(450);

                entity.Property(e => e.InputTakenForm).HasMaxLength(450);

                entity.Property(e => e.InterfaceProtocol).HasMaxLength(450);

                entity.Property(e => e.InterfaceWith).HasMaxLength(450);

                entity.Property(e => e.Interferencerejection).HasMaxLength(450);

                entity.Property(e => e.Ipvoltage)
                    .HasMaxLength(450)
                    .HasColumnName("IPVoltage");

                entity.Property(e => e.LampLuminousFlux).HasMaxLength(450);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(450);

                entity.Property(e => e.ManufacturerNameAndAddress).HasMaxLength(450);

                entity.Property(e => e.Mass).HasMaxLength(450);

                entity.Property(e => e.MaxRollingAngle).HasMaxLength(450);

                entity.Property(e => e.MaximumFiringElevation).HasMaxLength(450);

                entity.Property(e => e.MaximumRange).HasMaxLength(450);

                entity.Property(e => e.MaximumUseableAngle).HasMaxLength(450);

                entity.Property(e => e.MinimumRange).HasMaxLength(450);

                entity.Property(e => e.Model).HasMaxLength(450);

                entity.Property(e => e.MuzzleVelocity).HasMaxLength(450);

                entity.Property(e => e.NameOfLifeLimeOfImportantSpares).HasMaxLength(450);

                entity.Property(e => e.NoOfCoil).HasMaxLength(450);

                entity.Property(e => e.NoOfSensor).HasMaxLength(450);

                entity.Property(e => e.NoOfTube).HasMaxLength(450);

                entity.Property(e => e.NvrorDvrcapacity)
                    .HasMaxLength(450)
                    .HasColumnName("NVROrDVRCapacity");

                entity.Property(e => e.OperatingFrequency).HasMaxLength(450);

                entity.Property(e => e.OperationRange).HasMaxLength(450);

                entity.Property(e => e.Opvoltage)
                    .HasMaxLength(450)
                    .HasColumnName("OPVoltage");

                entity.Property(e => e.Os)
                    .HasMaxLength(450)
                    .HasColumnName("OS");

                entity.Property(e => e.Output).HasMaxLength(450);

                entity.Property(e => e.ParallelStatus).HasMaxLength(450);

                entity.Property(e => e.Pf)
                    .HasMaxLength(450)
                    .HasColumnName("PF");

                entity.Property(e => e.Phase).HasMaxLength(450);

                entity.Property(e => e.PositionAccuracy).HasMaxLength(450);

                entity.Property(e => e.PositionUpdate).HasMaxLength(450);

                entity.Property(e => e.Power).HasMaxLength(450);

                entity.Property(e => e.PowerOutput).HasMaxLength(450);

                entity.Property(e => e.PowerSupply).HasMaxLength(450);

                entity.Property(e => e.PresentRunningHours).HasMaxLength(450);

                entity.Property(e => e.PresentationMode).HasMaxLength(450);

                entity.Property(e => e.Purpose).HasMaxLength(450);

                entity.Property(e => e.Range).HasMaxLength(450);

                entity.Property(e => e.RangeOfFrequency).HasMaxLength(450);

                entity.Property(e => e.RateOfFire).HasMaxLength(450);

                entity.Property(e => e.RateOfFuelTransfer).HasMaxLength(450);

                entity.Property(e => e.RateOfInitalCurrent).HasMaxLength(450);

                entity.Property(e => e.RateOfWaterSupply).HasMaxLength(450);

                entity.Property(e => e.ReceiverType).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ReqSeaStateForOperate).HasMaxLength(450);

                entity.Property(e => e.ReqShipsSpeedForOperate).HasMaxLength(450);

                entity.Property(e => e.RfoutputImpedance)
                    .HasMaxLength(450)
                    .HasColumnName("RFOutputImpedance");

                entity.Property(e => e.RoutPlanningSystem).HasMaxLength(450);

                entity.Property(e => e.Rpm)
                    .HasMaxLength(450)
                    .HasColumnName("RPM");

                entity.Property(e => e.Sensitivity).HasMaxLength(450);

                entity.Property(e => e.ServiceLifeOfMainEquipment).HasMaxLength(450);

                entity.Property(e => e.SettlingTime).HasMaxLength(450);

                entity.Property(e => e.SonarRange).HasMaxLength(450);

                entity.Property(e => e.SpeedAccuracy).HasMaxLength(450);

                entity.Property(e => e.SpeedLimit).HasMaxLength(450);

                entity.Property(e => e.SpeedRange).HasMaxLength(450);

                entity.Property(e => e.Standard).HasMaxLength(450);

                entity.Property(e => e.SteeringType).HasMaxLength(450);

                entity.Property(e => e.TakenDataFrom).HasMaxLength(450);

                entity.Property(e => e.TechSpecification).HasMaxLength(450);

                entity.Property(e => e.TiltScanRange).HasMaxLength(450);

                entity.Property(e => e.TimeToFirstFix).HasMaxLength(450);

                entity.Property(e => e.TitlScanRange).HasMaxLength(450);

                entity.Property(e => e.TobeStoredInNsdctgLatestBuy)
                    .HasMaxLength(450)
                    .HasColumnName("TobeStoredInNSDCtgLatestBuy");

                entity.Property(e => e.TotalLength).HasMaxLength(450);

                entity.Property(e => e.TransmittedPulseLength).HasMaxLength(450);

                entity.Property(e => e.TxPulsereqetitionRate).HasMaxLength(450);

                entity.Property(e => e.Type).HasMaxLength(450);

                entity.Property(e => e.TypeOfModulation).HasMaxLength(450);

                entity.Property(e => e.TypeOfPlate).HasMaxLength(450);

                entity.Property(e => e.TypeOfSensor).HasMaxLength(450);

                entity.Property(e => e.Updating).HasMaxLength(450);

                entity.Property(e => e.UpgradationOrOverhaulinRequired).HasMaxLength(450);

                entity.Property(e => e.VisibleDistance).HasMaxLength(450);

                entity.Property(e => e.Voltage).HasMaxLength(450);

                entity.Property(e => e.Vrm)
                    .HasMaxLength(450)
                    .HasColumnName("VRM");

                entity.Property(e => e.WaterCapacity).HasMaxLength(450);

                entity.Property(e => e.WeightOfChaff).HasMaxLength(450);

                entity.Property(e => e.WeightOfFuse).HasMaxLength(450);

                entity.Property(e => e.WeightOfMissile).HasMaxLength(450);

                entity.Property(e => e.WeightOfWarhead).HasMaxLength(450);

                entity.Property(e => e.WindSpeedMeasuringRange).HasMaxLength(450);

                entity.Property(e => e.WorkingAirPressure).HasMaxLength(450);

                entity.Property(e => e.YearOfInstallation).HasMaxLength(450);

                entity.HasOne(d => d.AcquisitionMethod)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.AcquisitionMethodId)
                    .HasConstraintName("FK_ShipEquipmentInfo_AcquisitionMethod");

                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.BaseSchoolNameId)
                    .HasConstraintName("FK_ShipEquipmentInfo_BaseSchoolName");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_ShipEquipmentInfo_Brand");
               

                entity.HasOne(d => d.EquipmentCategory)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.EquipmentCategoryId)
                    .HasConstraintName("FK_ShipEquipmentInfo_EquipmentCategory");

                entity.HasOne(d => d.EquipmentType)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.EquipmentTypeId)
                    .HasConstraintName("FK_ShipEquipmentInfo_EquipmentType");

                entity.HasOne(d => d.EqupmentName)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.EqupmentNameId)
                    .HasConstraintName("FK_ShipEquipmentInfo_EqupmentName");

                entity.HasOne(d => d.StateOfEquipment)
                    .WithMany(p => p.ShipEquipmentInfos)
                    .HasForeignKey(d => d.StateOfEquipmentId)
                    .HasConstraintName("FK_ShipEquipmentInfo_StateOfEquipment");
            });

            modelBuilder.Entity<ShipInformation>(entity =>
            {
                entity.ToTable("ShipInformation");

                entity.Property(e => e.Address).HasMaxLength(450);

                entity.Property(e => e.Authority).HasMaxLength(450);

                entity.Property(e => e.BreadthBmld)
                    .HasMaxLength(450)
                    .HasColumnName("BreadthBMLD");

                entity.Property(e => e.CallSign).HasMaxLength(450);

                entity.Property(e => e.Class).HasMaxLength(450);

                entity.Property(e => e.ContactNo).HasMaxLength(450);

                entity.Property(e => e.ControlCode).HasMaxLength(450);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CruisingSpeed).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateOfCommission).HasColumnType("datetime");

                entity.Property(e => e.Depth).HasMaxLength(450);

                entity.Property(e => e.DisplacementFullLoad).HasMaxLength(450);

                entity.Property(e => e.DisplacementLightWeight).HasMaxLength(450);

                entity.Property(e => e.DraftAft)
                    .HasMaxLength(450)
                    .HasColumnName("DraftAFT");

                entity.Property(e => e.DraftFwd)
                    .HasMaxLength(450)
                    .HasColumnName("DraftFWD");

                entity.Property(e => e.EconomicSpeed).HasMaxLength(450);

                entity.Property(e => e.FileUpload).HasMaxLength(450);

                entity.Property(e => e.FreshWaterCapacity).HasMaxLength(450);

                entity.Property(e => e.FualCapacity).HasMaxLength(450);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LengthHom)
                    .HasMaxLength(450)
                    .HasColumnName("LengthHOM");

                entity.Property(e => e.LengthLoa)
                    .HasMaxLength(450)
                    .HasColumnName("LengthLOA");

                entity.Property(e => e.LuboilCapacity).HasMaxLength(450);

                entity.Property(e => e.Manufacturer).HasMaxLength(450);

                entity.Property(e => e.MaximumContinuousSpeed).HasMaxLength(450);

                entity.Property(e => e.MaximumSpeed).HasMaxLength(450);

                entity.Property(e => e.NickName).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.HasOne(d => d.AuthorityNavigation)
                    .WithMany(p => p.ShipInformationAuthorityNavigations)
                    .HasForeignKey(d => d.AuthorityId)
                    .HasConstraintName("FK_ShipInformation_BaseSchoolNameAuthority");

                entity.HasOne(d => d.BaseName)
                    .WithMany(p => p.ShipInformationBaseNames)
                    .HasForeignKey(d => d.BaseNameId)
                    .HasConstraintName("FK_ShipInformation_BaseSchoolNameBaseName");

                entity.HasOne(d => d.BaseSchoolName)
                    .WithMany(p => p.ShipInformationBaseSchoolNames)
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

            modelBuilder.Entity<ShipType>(entity =>
            {
                entity.ToTable("ShipType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<Sqn>(entity =>
            {
                entity.ToTable("Sqn");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<StateOfEquipment>(entity =>
            {
                entity.ToTable("StateOfEquipment");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<Tec>(entity =>
            {
                entity.ToTable("Tec");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            modelBuilder.Entity<TenderOpeningDateType>(entity =>
            {
                entity.ToTable("TenderOpeningDateType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.ShortName).HasMaxLength(450);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
