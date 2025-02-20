using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries;
using SchoolManagement.Application.Features.QuarterlyReturns.Request.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Handler.Queries
{
    public class GetQuarterlyReturnDetailRequestHandler : IRequestHandler<GetQuarterlyReturnDetailRequest, QuarterlyReturnDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<QuarterlyReturn> _QuarterlyReturnRepository;

        public GetQuarterlyReturnDetailRequestHandler(
            ISchoolManagementRepository<QuarterlyReturn> QuarterlyReturnRepository,
            IMapper mapper)
        {
            _QuarterlyReturnRepository = QuarterlyReturnRepository;
            _mapper = mapper;
        }

        public async Task<QuarterlyReturnDto> Handle(GetQuarterlyReturnDetailRequest request, CancellationToken cancellationToken)
        {
            var QuarterlyReturn = await _QuarterlyReturnRepository.Get(request.QuarterlyReturnId);

            if (QuarterlyReturn == null)
            {
                throw new KeyNotFoundException($"QuarterlyReturn with ID {request.QuarterlyReturnId} not found.");
            }

            return _mapper.Map<QuarterlyReturnDto>(QuarterlyReturn);
        }
    }
}
