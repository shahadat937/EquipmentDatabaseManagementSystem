﻿using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetProcurementListRequestHandler : IRequestHandler<GetProcurementListRequest, PagedResult<ProcurementDto>>
    {

        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;

        private readonly IMapper _mapper;

        public GetProcurementListRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository, IMapper mapper)
        {
            _ProcurementRepository = ProcurementRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProcurementDto>> Handle(GetProcurementListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Procurement> Procurements;

            if (request.SearchBy == "shipname")
            {
                Procurements = _ProcurementRepository.FilterWithInclude(
                    x => (x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText) || string.IsNullOrEmpty(request.QueryParams.SearchText)),
                    "BaseSchoolName", "ProcurementMethod", "Envelope", "ProcurementType", "GroupName", "EqupmentName", "Controlled", "FcLc", "DgdpNssd", "Tec", "TenderOpeningDateType", "PaymentStatus");
            }
            else if (request.SearchBy == "equipmentname")
            {
                Procurements = _ProcurementRepository.FilterWithInclude(
                    x => (x.EqupmentName.Name.Contains(request.QueryParams.SearchText) || string.IsNullOrEmpty(request.QueryParams.SearchText)),
                    "BaseSchoolName", "ProcurementMethod", "Envelope", "ProcurementType", "GroupName", "EqupmentName", "Controlled", "FcLc", "DgdpNssd", "Tec", "TenderOpeningDateType", "PaymentStatus");
            }
            else
            {
                Procurements = _ProcurementRepository.FilterWithInclude(
                    x => (string.IsNullOrEmpty(request.QueryParams.SearchText)),
                    "BaseSchoolName", "ProcurementMethod", "Envelope", "ProcurementType", "GroupName", "EqupmentName", "Controlled", "FcLc", "DgdpNssd", "Tec", "TenderOpeningDateType", "PaymentStatus");
            }

            var totalCount = Procurements.Count();

            Procurements = Procurements
                .OrderByDescending(x => x.ProcurementId)
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

            var ProcurementDtos = _mapper.Map<List<ProcurementDto>>(Procurements);

            var result = new PagedResult<ProcurementDto>(
                ProcurementDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize);

            return result;





        }
    }
}
