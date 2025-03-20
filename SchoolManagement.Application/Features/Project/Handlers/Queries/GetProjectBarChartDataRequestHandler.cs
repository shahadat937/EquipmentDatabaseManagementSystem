using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Project;
using SchoolManagement.Application.Features.Project.Requests.Queries;
using SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Helpers;
using System.Linq.Expressions;

namespace SchoolManagement.Application.Features.Project.Handlers.Queries
{
    public class GetProjectBarChartDataRequestHandler : IRequestHandler<GetProjectBarChartDataRequest, ProjectBarChartDto>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Project> _ProjectRepository;
        private readonly ISchoolManagementRepository<ProjectBaseSchoolName> _ProjectBaseSchoolNameRepository;
        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;
        private readonly IMapper _mapper;

        public GetProjectBarChartDataRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Project> ProjectRepository, IMapper mapper, ISchoolManagementRepository<ProjectBaseSchoolName> ProjectBaseSchoolNameRepository, ISchoolManagementRepository<BaseSchoolName> baseSchoolNameRepository)
        {
            _ProjectRepository = ProjectRepository;
            _mapper = mapper;
            _ProjectBaseSchoolNameRepository = ProjectBaseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
        }

        public  Task<ProjectBarChartDto> Handle(GetProjectBarChartDataRequest request, CancellationToken cancellationToken)
        {

            
            var ProjectQuery = _ProjectRepository.FilterWithInclude(
                x => true
            );

            var foreiginTrainingCount = ProjectQuery.Where(x => (x.ForeignTrainingScheduleAndContractReceived != null || x.ForeignTrainingApprovalForOfficerNomination != null || x.ForeignTrainingGoApproval != null && x.ForeignTraningDate != null) && (x.FatSchedule == null && x.FatAcceptance == null && x.FatApprovalForOfficerNomination == null && x.FatGoApproval == null && x.FatDate == null) && (x.ShipmentAndDelivery == null && x.SAT == null && x.HAT == null && x.SAT == null && x.LocalTraining == null && x.FinalAcceptance == null)).Count();

            var fatCount = ProjectQuery.Where(x => (x.FatSchedule != null || x.FatAcceptance != null || x.FatApprovalForOfficerNomination != null || x.FatGoApproval != null || x.FatDate != null) && (x.ShipmentAndDelivery == null && x.SAT == null && x.HAT == null && x.SAT == null && x.LocalTraining == null && x.FinalAcceptance == null)).Count();

            var installationCount = ProjectQuery.Where(x => (x.ShipmentAndDelivery != null || x.SAT != null || x.HAT != null || x.SAT != null || x.LocalTraining != null || x.FinalAcceptance != null)).Count();


            var foreiginTrainingItems = ProjectQuery.Where(x => (x.ForeignTrainingScheduleAndContractReceived != null || x.ForeignTrainingApprovalForOfficerNomination != null || x.ForeignTrainingGoApproval != null && x.ForeignTraningDate != null) && (x.FatSchedule == null && x.FatAcceptance == null && x.FatApprovalForOfficerNomination == null && x.FatGoApproval == null && x.FatDate == null) && (x.ShipmentAndDelivery == null && x.SAT == null && x.HAT == null && x.SAT == null && x.LocalTraining == null && x.FinalAcceptance == null)).OrderByDescending(x=>x.LastModifiedDate).Select(x => x.EqupmentName.Name).ToList();


            var fatItems = ProjectQuery.Where(x => (x.FatSchedule != null || x.FatAcceptance != null || x.FatApprovalForOfficerNomination != null || x.FatGoApproval != null || x.FatDate != null) && (x.ShipmentAndDelivery == null && x.SAT == null && x.HAT == null && x.SAT == null && x.LocalTraining == null && x.FinalAcceptance == null)).OrderByDescending(x => x.LastModifiedDate).Select(x=> x.EqupmentName.Name).ToList();

            var installationItmes = ProjectQuery.Where(x => (x.ShipmentAndDelivery != null || x.SAT != null || x.HAT != null || x.SAT != null || x.LocalTraining != null || x.FinalAcceptance != null)).OrderByDescending(x => x.LastModifiedDate).Select(x => x.EqupmentName.Name).ToList();

            var result = new ProjectBarChartDto
            {
                ForeignTrainingCount = foreiginTrainingCount,
                FatCount = fatCount,
                InstallationCount  = installationCount,
                ForeignTrainingItems = foreiginTrainingItems,
                FatItems = fatItems,
                InstallationItems = installationItmes

            };

            return  Task.FromResult(result);


        }
    }
}