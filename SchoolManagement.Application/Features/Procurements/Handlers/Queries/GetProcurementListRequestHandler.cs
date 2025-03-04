using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetProcurementListRequestHandler : IRequestHandler<GetProcurementListRequest, PagedResult<ProcurementDto>>
    {

        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;
        private readonly ISchoolManagementRepository<ProcurementBaseSchoolName> _ProcurementBaseSchoolNameRepository;
        private readonly ISchoolManagementRepository<ProcurementTenderOpening> _ProcurementTenderOpeningRepository;
        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;


        private readonly IMapper _mapper;

        public GetProcurementListRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository, IMapper mapper, ISchoolManagementRepository<ProcurementBaseSchoolName> ProcurementBaseSchoolNameRepository, ISchoolManagementRepository<ProcurementTenderOpening> ProcurementTenderOpeningRepository, ISchoolManagementRepository<BaseSchoolName> baseSchoolNameRepository)
        {
            _ProcurementRepository = ProcurementRepository;
            _mapper = mapper;
            _ProcurementTenderOpeningRepository = ProcurementTenderOpeningRepository;
            _ProcurementBaseSchoolNameRepository = ProcurementBaseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
        }

        public async Task<PagedResult<ProcurementDto>> Handle(GetProcurementListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            
            IQueryable<Procurement> procurementsQuery;

           
                procurementsQuery = _ProcurementRepository.FilterWithInclude(
                    x => string.IsNullOrEmpty(request.QueryParams.SearchText) || 
                         x.EqupmentName.Name.Contains(request.QueryParams.SearchText), 
                    "BaseSchoolName", "GroupName", "EqupmentName", "Controlled", "FcLc", "DgdpNssd", "FinancialYear"
                );
            

            var totalCount = await procurementsQuery.CountAsync();

            var procurements = await procurementsQuery
                .OrderByDescending(x => x.ProcurementId)  
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToListAsync();

            var baseSchoolNames = await _ProcurementBaseSchoolNameRepository.GetAll();
            var tenderOpenings = await _ProcurementTenderOpeningRepository.GetAll();
            var allBaseSchool = await _BaseSchoolNameRepository.GetAll();

 
            var procurementDtos = _mapper.Map<List<ProcurementDto>>(procurements);

            foreach (var procurementDto in procurementDtos)
            {

                procurementDto.BaseSchoolNameDtos = baseSchoolNames
                    .Where(x => x.ProcurementId == procurementDto.ProcurementId)
                    .Select(x => new ProcurementBaseSchoolNameDto
                    {
                        BaseSchoolNameId = x.BaseSchoolNameId,
                        BaseSchoolName = allBaseSchool.FirstOrDefault(b => b.BaseSchoolNameId == x.BaseSchoolNameId)?.SchoolName
                       
                    }).ToList();


                procurementDto.ProcurementTenderOpeningDto = tenderOpenings
                    .Where(x => x.ProcurementId == procurementDto.ProcurementId)
                    .Select(x => new ProcurementTenderOpeningDto
                    {
                        TenderOpeningDate = x.TenderOpeningDate,
                        TenderOpeningCount = x.TenderOpeningCount

                    }).ToList();
            }

            var result = new PagedResult<ProcurementDto>(
                procurementDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize
            );

            return result;
        }


    }
}