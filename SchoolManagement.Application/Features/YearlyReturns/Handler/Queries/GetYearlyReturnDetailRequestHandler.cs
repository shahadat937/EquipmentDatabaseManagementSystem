using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries;
using SchoolManagement.Application.Features.YearlyReturns.Request.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Handler.Queries
{
    public class GetYearlyReturnDetailRequestHandler : IRequestHandler<GetYearlyReturnDetailRequest, YearlyReturnDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<YearlyReturn> _yearlyReturnRepository;

        public GetYearlyReturnDetailRequestHandler(
            ISchoolManagementRepository<YearlyReturn> yearlyReturnRepository,
            IMapper mapper)
        {
            _yearlyReturnRepository = yearlyReturnRepository;
            _mapper = mapper;
        }

        public async Task<YearlyReturnDto> Handle(GetYearlyReturnDetailRequest request, CancellationToken cancellationToken)
        {
            var yearlyReturn = await _yearlyReturnRepository.Get(request.YearlyReturnId);

            if (yearlyReturn == null)
            {
                throw new KeyNotFoundException($"YearlyReturn with ID {request.YearlyReturnId} not found.");
            }

            return _mapper.Map<YearlyReturnDto>(yearlyReturn);
        }
    }
}
