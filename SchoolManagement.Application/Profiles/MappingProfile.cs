using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Application.DTOs.CodeValueType;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.DTOs.Modules;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.Helpers;
using SchoolManagement.Application.DTOs.Sqns;
using SchoolManagement.Application.DTOs.OperationalStatuss;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.DTOs.StateOfEquipments;
using SchoolManagement.Application.DTOs.EqupmentNames;
using SchoolManagement.Application.DTOs.GroupNames;
using SchoolManagement.Application.DTOs.Brands;
using SchoolManagement.Application.DTOs.EquipmentCategorys;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes;
using SchoolManagement.Application.DTOs.LetterTypes;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes;
using SchoolManagement.Application.DTOs.RunningTimes;
using SchoolManagement.Application.DTOs.ReportType;
using SchoolManagement.Application.DTOs.ProjectStatus;
using SchoolManagement.Application.DTOs.ProcurementType;
using SchoolManagement.Application.DTOs.DealingOfficer;
using SchoolManagement.Application.DTOs.DgdpNssd;
using SchoolManagement.Application.DTOs.FcLc;
using SchoolManagement.Application.DTOs.Controlled;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.DTOs.AcquisitionMethod;
using SchoolManagement.Application.DTOs.ShipDrowings;
using SchoolManagement.Application.DTOs.ShipDrowinges;
using SchoolManagement.Application.DTOs.EquipmentType;
using SchoolManagement.Application.DTOs.Tec;
using SchoolManagement.Application.DTOs.ProcurementMethod;
using SchoolManagement.Application.DTOs.PaymentStatus;
using SchoolManagement.Application.DTOs.Envelope;
using SchoolManagement.Doamin;
using SchoolManagement.Application.DTOs.ReportingMonths;
using SchoolManagement.Application.DTOs.ReturnTypes;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.DTOs.OperationalStates;
using SchoolManagement.Application.DTOs.ActionTaken;
using SchoolManagement.Application.DTOs.Priority;
using SchoolManagement.Application.DTOs.DailyWorkState;
using SchoolManagement.Application.DTOs.BookType;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.DTOs.AspNetRoles;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem;
using SchoolManagement.Application.DTOs.StatusOfShip;
using SchoolManagement.Application.DTOs.UserManual;
using SchoolManagement.Application.DTOs.ReporingYear;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using SchoolManagement.Application.DTOs.ReportingYear;

namespace SchoolManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Lattar A
            #region AcquisitionMethod Mapping    
            CreateMap<AcquisitionMethod, AcquisitionMethodDto>().ReverseMap();
            CreateMap<AcquisitionMethod, CreateAcquisitionMethodDto>().ReverseMap();
            #endregion

            #region ActionTaken Mapping    
            CreateMap<ActionTaken, ActionTakenDto>().ReverseMap();
            CreateMap<ActionTaken, CreateActionTakenDto>().ReverseMap();
            #endregion

            #region BookType Mapping    
            CreateMap<BookType, BookTypeDto>().ReverseMap();
            CreateMap<BookType, CreateBookTypeDto>().ReverseMap();
            #endregion

            #region BookUserManualBRInfo Mapping    
            CreateMap<BookUserManualBRInfoDto, BookUserManualBRInfo>().ReverseMap()
                 .ForMember(d => d.BookType, o => o.MapFrom(s => s.BookType.Name))
                 .ForMember(d => d.ShipName, o => o.MapFrom(s => s.BaseSchoolName.SchoolName))
                 .ForMember(d => d.UploadDocument, o => o.MapFrom<BookUserManualBRInfoFileUrlResolver>());
            CreateMap<BookUserManualBRInfo, CreateBookUserManualBRInfoDto>().ReverseMap();
            #endregion

            #region Priority Mapping    
            CreateMap<Priority, PriorityDto>().ReverseMap();
            CreateMap<Priority, CreatePriorityDto>().ReverseMap();
            #endregion

            #region DailyWorkState Mapping    
            CreateMap<DailyWorkStateDto, DailyWorkState>().ReverseMap()
                .ForMember(d => d.LetterType, o => o.MapFrom(s => s.LetterType.Name))
                .ForMember(d => d.DealingOfficer, o => o.MapFrom(s => s.DealingOfficer.Name))
                .ForMember(d => d.ActionTaken, o => o.MapFrom(s => s.ActionTaken.Name))
                .ForMember(d => d.Priority, o => o.MapFrom(s => s.Priority.Name))
                .ForMember(d => d.FileUpload, o => o.MapFrom<DailyWorkStateFileUrlResolver>());
            CreateMap<DailyWorkState, CreateDailyWorkStateDto>().ReverseMap();
            #endregion

            #region HalfYearlyReturn Mapping    
            CreateMap<HalfYearlyReturnDto, HalfYearlyReturn>().ReverseMap()
                 .ForMember(d => d.EquipmentCategory, o => o.MapFrom(s => s.EquipmentCategory.Name))
                 .ForMember(d => d.EqupmentName, o => o.MapFrom(s => s.EqupmentName.Name))
                 .ForMember(d => d.BaseSchoolName, o => o.MapFrom(s => s.BaseSchoolName.SchoolName))
                 .ForMember(d => d.InputPowerSupply, o => o.MapFrom(s => s.ShipEquipmentInfo.PowerSupply));


            CreateMap<HalfYearlyReturn, CreateHalfYearlyReturnDto>().ReverseMap();
            #endregion

            #region YearlyReturn Mapping   
            CreateMap<YearlyReturnDto, YearlyReturn>().ReverseMap()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.BaseSchoolName != null ? src.BaseSchoolName.SchoolName : null))
                .ForMember(d => d.OperationalStatus, o => o.MapFrom(s => s.OperationalStatus.Name))
                .ForMember(d => d.ReportingMonth, o => o.MapFrom(s => s.ReportingMonth.Name))
                .ForMember(d=> d.ReportingYear, o=> o.MapFrom(s=> s.ReportingYear.Year))
                .ForMember(d => d.FileUpload, o => o.MapFrom<YearlyReturnFileUrlResolver>());

            CreateMap<YearlyReturn, CreateYearlyReturnDto>().ReverseMap();
            #endregion

            #region QuarterlyReturn Mapping   
            CreateMap<QuarterlyReturnDto, QuarterlyReturn>().ReverseMap()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.BaseSchoolName != null ? src.BaseSchoolName.SchoolName : null))
                .ForMember(d => d.OperationalStatus, o => o.MapFrom(s => s.OperationalStatus.Name))
                .ForMember(d => d.ReportingMonth, o => o.MapFrom(s => s.ReportingMonth.Name))
                .ForMember(d => d.ReportingYear, o => o.MapFrom(s => s.ReportingYear.Year))
                .ForMember(d => d.FileUpload, o => o.MapFrom<QuarterlyReturnFileUrlResolver>());

            CreateMap<CreateQuarterlyReturnDto, QuarterlyReturn>().ReverseMap();
            #endregion



            #region HalfYearlyRunningTime Mapping    
            CreateMap<HalfYearlyRunningTime, HalfYearlyRunningTimeDto>().ReverseMap();
            CreateMap<HalfYearlyRunningTime, CreateHalfYearlyRunningTimeDto>().ReverseMap();
            #endregion

            #region Sqn Mapping    
            CreateMap<Sqn, SqnDto>().ReverseMap();
            CreateMap<Sqn, CreateSqnDto>().ReverseMap();
            #endregion

            #region MonthlyReturn Mapping    
            CreateMap<MonthlyReturnDto, MonthlyReturn>().ReverseMap()
            .ForMember(d => d.EquipmentCategory, o => o.MapFrom(s => s.EquipmentCategory.Name))
            .ForMember(d => d.EqupmentName, o => o.MapFrom(s => s.EqupmentName.Name))
            .ForMember(d => d.EqupmentName, o => o.MapFrom(s => s.EqupmentName.Name))
            .ForMember(d => d.OperationalStatus, o => o.MapFrom(s => s.OperationalStatus.Name))
            .ForMember(d => d.ReportingMonth, o => o.MapFrom(s => s.ReportingMonth.Name))
            .ForMember(d => d.ReturnType, o => o.MapFrom(s => s.ReturnType.Name))
            .ForMember(d => d.UploadDocument, o => o.MapFrom<MonthlyReturnUrlResolver>())
            .ForMember(d => d.SchoolName, o => o.MapFrom(s => s.BaseSchoolName.SchoolName));
            CreateMap<MonthlyReturn, CreateMonthlyReturnDto>().ReverseMap();
            #endregion

            //       CreateMap<Domain.StatusOfShip, StatusOfShipDto>().ReverseMap()
            //.ForMember(d => d.SchoolName, o => o.MapFrom(s => s.BaseSchoolName != null ? s.BaseSchoolName.SchoolName : null))
            //.ForMember(d => d.OperationalStatus, o => o.MapFrom(s => s.OperationalStatus != null ? s.OperationalStatus.Name : null)).ReverseMap();

            CreateMap<StatusOfShipDto, StatusOfShip>().ReverseMap()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.BaseSchoolName != null ? src.BaseSchoolName.SchoolName : null))
             .ForMember(d => d.OperationalStatus, o => o.MapFrom(s => s.OperationalStatus.Name));

            CreateMap<StatusOfShip, CreateStatusOfShipDto>().ReverseMap();





            #region OperationalState Mapping    
            CreateMap<OperationalStateDto, OperationalState>().ReverseMap();
            CreateMap<OperationalState, CreateOperationalStateDto>().ReverseMap();
            #endregion

            #region ReturnType Mapping    
            CreateMap<ReturnType, ReturnTypeDto>().ReverseMap();
            CreateMap<ReturnType, CreateReturnTypeDto>().ReverseMap();
            #endregion

            #region ReportingMonth Mapping    
            CreateMap<ReportingMonth, ReportingMonthDto>().ReverseMap();
            CreateMap<ReportingMonth, CreateReportingMonthDto>().ReverseMap();
            #endregion

            #region QuarterlyReturnNoType Mapping    
            CreateMap<QuarterlyReturnNoType, QuarterlyReturnNoTypeDto>().ReverseMap();
            CreateMap<QuarterlyReturnNoType, CreateQuarterlyReturnNoTypeDto>().ReverseMap();
            #endregion

            #region RunningTime Mapping    
            CreateMap<RunningTime, RunningTimeDto>().ReverseMap();
            CreateMap<RunningTime, CreateRunningTimeDto>().ReverseMap();
            #endregion

            #region ReportType Mapping    
            CreateMap<ReportType, ReportTypeDto>().ReverseMap();
            CreateMap<ReportType, CreateReportTypeDto>().ReverseMap();
            #endregion

            #region DealingOfficer Mapping    
            CreateMap<DealingOfficer, DealingOfficerDto>().ReverseMap();
            CreateMap<DealingOfficer, CreateDealingOfficerDto>().ReverseMap();
            #endregion

            #region DgdpNssd Mapping    
            CreateMap<DgdpNssd, DgdpNssdDto>().ReverseMap();
            CreateMap<DgdpNssd, CreateDgdpNssdDto>().ReverseMap();
            #endregion

            #region FcLc Mapping    
            CreateMap<FcLc, FcLcDto>().ReverseMap();
            CreateMap<FcLc, CreateFcLcDto>().ReverseMap();
            #endregion

            #region Envelope Mapping    
            CreateMap<Envelope, EnvelopeDto>().ReverseMap();
            CreateMap<Envelope, CreateEnvelopeDto>().ReverseMap();
            #endregion

            #region PaymentStatus Mapping    
            CreateMap<PaymentStatus, PaymentStatusDto>().ReverseMap();
            CreateMap<PaymentStatus, CreatePaymentStatusDto>().ReverseMap();
            #endregion

            #region ProcurementMethod Mapping    
            CreateMap<ProcurementMethod, ProcurementMethodDto>().ReverseMap();
            CreateMap<ProcurementMethod, CreateProcurementMethodDto>().ReverseMap();
            #endregion

            #region Procurement Mapping    
            CreateMap<ProcurementDto, Procurement>().ReverseMap()
                .ForMember(d => d.SchoolName, o => o.MapFrom(s => s.BaseSchoolName.SchoolName))
                .ForMember(d => d.ProcurementMethodName, o => o.MapFrom(s => s.ProcurementMethod.Name))
                .ForMember(d => d.EnvelopeName, o => o.MapFrom(s => s.Envelope.Name))
                .ForMember(d => d.ProcurementTypeName, o => o.MapFrom(s => s.ProcurementType.Name))
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.GroupName.Name))
                .ForMember(d => d.EqupmentName, o => o.MapFrom(s => s.EqupmentName.Name))
                .ForMember(d => d.ControlledName, o => o.MapFrom(s => s.Controlled.Name))
                .ForMember(d => d.FcLcName, o => o.MapFrom(s => s.FcLc.Name))
                .ForMember(d => d.DgdpNssdName, o => o.MapFrom(s => s.DgdpNssd.Name))
                .ForMember(d => d.TenderOpeningDateTypeName, o => o.MapFrom(s => s.TenderOpeningDateType.Name))
                .ForMember(d => d.TecName, o => o.MapFrom(s => s.Tec.Name))
                .ForMember(d => d.PaymentStatusName, o => o.MapFrom(s => s.PaymentStatus.Name));
            CreateMap<Procurement, CreateProcurementDto>().ReverseMap();
            #endregion

            #region Controlled Mapping    
            CreateMap<Controlled, ControlledDto>().ReverseMap();
            CreateMap<Controlled, CreateControlledDto>().ReverseMap();
            #endregion

            #region ProjectStatus Mapping    
            CreateMap<ProjectStatus, ProjectStatusDto>().ReverseMap();
            CreateMap<ProjectStatus, CreateProjectStatusDto>().ReverseMap();
            #endregion

            #region ProcurementType Mapping    
            CreateMap<ProcurementType, ProcurementTypeDto>().ReverseMap();
            CreateMap<ProcurementType, CreateProcurementTypeDto>().ReverseMap();
            #endregion

            #region OperationalStatus Mapping    
            CreateMap<OperationalStatus, OperationalStatusDto>().ReverseMap();
            CreateMap<OperationalStatus, CreateOperationalStatusDto>().ReverseMap();
            #endregion

            #region OperationalStatus Mapping    
            CreateMap<ShipType, ShipTypeDto>().ReverseMap();
            CreateMap<ShipType, CreateShipTypeDto>().ReverseMap();
            #endregion

            #region ShipInformation Mapping    
            CreateMap<ShipInformationDto, ShipInformation>().ReverseMap()
            .ForMember(d => d.BaseSchoolName, o => o.MapFrom(s => s.BaseSchoolName.SchoolName))
            .ForMember(d => d.AuthorityName, o => o.MapFrom(s => s.AuthorityNavigation.SchoolName))
            .ForMember(d => d.BaseName, o => o.MapFrom(s => s.BaseName.SchoolName))
            .ForMember(d => d.SqnName, o => o.MapFrom(s => s.Sqn.Name))
            .ForMember(d => d.OperationalStatus, o => o.MapFrom(s => s.OperationalStatus.Name))
            .ForMember(d => d.ShipType, o => o.MapFrom(s => s.ShipType.Name))
           .ForMember(d => d.FileUpload, o => o.MapFrom<FileUrlResolver>());
            CreateMap<ShipInformation, CreateShipInformationDto>().ReverseMap();
            //CreateMap<ShipInformation, ShipInformationDto>().ReverseMap();
            //CreateMap<ShipInformation, CreateShipInformationDto>().ReverseMap();
            #endregion

            #region ShipEquipmentInfo Mapping    
            CreateMap<ShipEquipmentInfoDto, ShipEquipmentInfo>().ReverseMap()
                .ForMember(d => d.EquipmentCategory, o => o.MapFrom(s => s.EquipmentCategory.Name))
                .ForMember(d => d.EqupmentName, o => o.MapFrom(s => s.EqupmentName.Name))
                 .ForMember(d => d.StateOfEquipment, o => o.MapFrom(s => s.StateOfEquipment.Name))
                 .ForMember(d => d.SchoolName, o => o.MapFrom(s => s.BaseSchoolName.SchoolName))
                 .ForMember(d => d.AcquisitionMethodName, o => o.MapFrom(s => s.AcquisitionMethod.Name))
                  .ForMember(d => d.FileUpload, o => o.MapFrom<ShipEquipmentFileUrlResolver>());
            CreateMap<ShipEquipmentInfo, CreateShipEquipmentInfoDto>().ReverseMap();
            #endregion

            #region ShipInformation Mapping    
            CreateMap<StateOfEquipment, StateOfEquipmentDto>().ReverseMap();
            CreateMap<StateOfEquipment, CreateStateOfEquipmentDto>().ReverseMap();
            #endregion

            #region EquipmentType Mapping    
            CreateMap<EquipmentType, EquipmentTypeDto>().ReverseMap();
            CreateMap<EquipmentType, CreateEquipmentTypeDto>().ReverseMap();
            #endregion

            #region Tec Mapping    
            CreateMap<Tec, TecDto>().ReverseMap();
            CreateMap<Tec, CreateTecDto>().ReverseMap();
            #endregion

            #region EqupmentName Mapping    
            CreateMap<EqupmentNameDto, EqupmentName>().ReverseMap()
                .ForMember(d => d.EquipmentCategory, o => o.MapFrom(s => s.EquipmentCategory.Name));
            CreateMap<EqupmentName, CreateEqupmentNameDto>().ReverseMap();
            #endregion



            #region GroupName Mapping    
            CreateMap<GroupName, GroupNameDto>().ReverseMap();
            CreateMap<GroupName, CreateGroupNameDto>().ReverseMap();
            #endregion


            #region Brand Mapping    
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            #endregion

            #region ShipDrowing Mapping    
            CreateMap<ShipDrowingDto, ShipDrowing>().ReverseMap()
                .ForMember(x => x.Authority, o => o.MapFrom(s => s.Authority.SchoolName))
                .ForMember(x => x.BaseName, o => o.MapFrom(s => s.BaseName.SchoolName))
                .ForMember(x => x.BaseSchoolName, o => o.MapFrom(s => s.BaseSchoolName.SchoolName))
                .ForMember(d => d.FileUpload, o => o.MapFrom<ShipDrowingFileUrlResolver>());
            CreateMap<ShipDrowing, CreateShipDrowingDto>().ReverseMap();
            #endregion

            #region EquipmentCategory Mapping    
            CreateMap<EquipmentCategoryDto, EquipmentCategory>().ReverseMap()
                 .ForMember(d => d.GroupName, o => o.MapFrom(s => s.GroupName.Name));
            CreateMap<EquipmentCategory, CreateEquipmentCategoryDto>().ReverseMap();
            #endregion

            #region TenderOpeningDateType Mapping    
            CreateMap<TenderOpeningDateType, TenderOpeningDateTypeDto>().ReverseMap();
            CreateMap<TenderOpeningDateType, CreateTenderOpeningDateTypeDto>().ReverseMap();
            #endregion

            #region LetterType Mapping    
            CreateMap<LetterType, LetterTypeDto>().ReverseMap();
            CreateMap<LetterType, CreateLetterTypeDto>().ReverseMap();
            #endregion

            //#region BnaClassSchedule Mapping    
            //CreateMap< BnaClassScheduleDto, BnaClassSchedule>().ReverseMap()
            //    .ForMember(d => d.BnaSemester, o => o.MapFrom(s => s.BnaSemester.SemesterName))
            //    .ForMember(d => d.ClassPeriod, o => o.MapFrom(s => s.ClassPeriod.PeriodName))
            //    .ForMember(d => d.BnaSubjectName, o => o.MapFrom(s => s.BnaSubjectName.SubjectName))
            //    .ForMember(d => d.BnaClassSectionSelection, o => o.MapFrom(s => s.BnaClassSectionSelection.SectionName));
            //CreateMap<BnaClassSchedule, CreateBnaClassScheduleDto>().ReverseMap();
            //#endregion


            #region BaseSchoolNames Mapping   
            CreateMap<BaseSchoolNameDto, BaseSchoolName>().ReverseMap()
              //.ForMember(d => d.BaseName, o => o.MapFrom(s => s.BaseName.BaseNames))
              .ForMember(d => d.SchoolLogo, o => o.MapFrom<LogoUrlResolver>()); ;
            CreateMap<BaseSchoolName, CreateBaseSchoolNameDto>().ReverseMap();
            #endregion           

            #region CodeValues  Mappings 
            CreateMap<CodeValueDto, CodeValue>().ReverseMap()
              .ForMember(d => d.CodeValueType, o => o.MapFrom(s => s.CodeValueType.Type));

            CreateMap<CodeValue, CreateCodeValueDto>().ReverseMap();
            #endregion

            #region CodeValueType  Mappings 
            CreateMap<CodeValueType, CodeValueTypeDto>().ReverseMap();
            CreateMap<CodeValueType, CreateCodeValueTypeDto>().ReverseMap();
            #endregion

            #region Features Mapping    
            CreateMap<FeatureDto, Feature>().ReverseMap()
             .ForMember(d => d.ModuleName, o => o.MapFrom(s => s.Module.Title));

            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            #endregion

            #region Modules Mapping    
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<Module, ModuleFeatureDto>().ReverseMap();

            CreateMap<Module, CreateModuleDto>().ReverseMap();
            #endregion

            #region RoleFeature Mappings 
            CreateMap<RoleFeature, RoleFeatureDto>().ReverseMap();
            CreateMap<RoleFeature, CreateRoleFeatureDto>().ReverseMap();
            #endregion
            #region ASPNetRoles 
            CreateMap<AspNetRoles, AspNetRolesDto>().ReverseMap();
            #endregion
            #region OperationalStatusOfEquipmentSystem
            CreateMap<OperationalStatusOfEquipmentSystem, CreateOperationalStatusOfEquipmentSystemDto>().ReverseMap();
            CreateMap<OperationalStatusOfEquipmentSystemDto, OperationalStatusOfEquipmentSystem>().ReverseMap()
                .ForMember(d => d.BaseSchoolName, o => o.MapFrom(s => s.BaseSchoolName.SchoolName))
                .ForMember(d => d.StateOfEquipment, o => o.MapFrom(s => s.StateOfEquipment.Name))
                .ForMember(d => d.NameOfEquipment, o => o.MapFrom(s => s.EqupmentName.Name));
            #endregion
            #region UserManual Mappings
            CreateMap<UserManualDto, UserManual>().ReverseMap()
                .ForMember(d => d.Doc, o => o.MapFrom<UserManualUrlResolver>());
            CreateMap<UserManual, CreateUserManualDto>().ReverseMap();
            #endregion

            //.CreateMap<StatusOfShipDto, StatusOfShipSpace>().ReverseMap()
            //    .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.BaseSchoolName != null ? src.BaseSchoolName.SchoolName : null));
            //CreateMap<StatusOfShipSpace, CreateStatusOfShipDto>().ReverseMap();

            #region ReportingYear   
            CreateMap<ReportingYear, ReportingYearDto>().ReverseMap();
            CreateMap<ReportingYear, CreateReportingYearDto>().ReverseMap();
            #endregion
        }
    }

}
