using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.StateOfEquipments;
using SchoolManagement.Application.Features.StateOfEquipments.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.StateOfEquipments.Handlers.Queries
{
    public class GetStateOfEquipmentListRequestHandler : IRequestHandler<GetStateOfEquipmentListRequest, PagedResult<StateOfEquipmentDto>>
    {

        private readonly ISchoolManagementRepository<StateOfEquipment> _StateOfEquipmentRepository;

        private readonly IMapper _mapper;

        public GetStateOfEquipmentListRequestHandler(ISchoolManagementRepository<StateOfEquipment> StateOfEquipmentRepository, IMapper mapper)
        {
            _StateOfEquipmentRepository = StateOfEquipmentRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<StateOfEquipmentDto>> Handle(GetStateOfEquipmentListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<StateOfEquipment> StateOfEquipments = _StateOfEquipmentRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = StateOfEquipments.Count();
            StateOfEquipments = StateOfEquipments.OrderByDescending(x => x.StateOfEquipmentId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var StateOfEquipmentDtos = _mapper.Map<List<StateOfEquipmentDto>>(StateOfEquipments);
            var result = new PagedResult<StateOfEquipmentDto>(StateOfEquipmentDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
