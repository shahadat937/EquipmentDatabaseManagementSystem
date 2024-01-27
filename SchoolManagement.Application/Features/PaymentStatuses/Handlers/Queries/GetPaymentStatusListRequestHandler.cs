using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.PaymentStatus;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.PaymentStatuss.Handlers.Queries
{
    public class GetPaymentStatusListRequestHandler : IRequestHandler<GetPaymentStatusListRequest, PagedResult<PaymentStatusDto>>
    {

        private readonly ISchoolManagementRepository<PaymentStatus> _PaymentStatusRepository;

        private readonly IMapper _mapper;

        public GetPaymentStatusListRequestHandler(ISchoolManagementRepository<PaymentStatus> PaymentStatusRepository, IMapper mapper)
        {
            _PaymentStatusRepository = PaymentStatusRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PaymentStatusDto>> Handle(GetPaymentStatusListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<PaymentStatus> PaymentStatuss = _PaymentStatusRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = PaymentStatuss.Count();
            PaymentStatuss = PaymentStatuss.OrderByDescending(x => x.PaymentStatusId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var PaymentStatusDtos = _mapper.Map<List<PaymentStatusDto>>(PaymentStatuss);
            var result = new PagedResult<PaymentStatusDto>(PaymentStatusDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
