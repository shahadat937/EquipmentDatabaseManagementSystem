using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.StatusOfShip;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.StatusOfShip.Request.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfShip.Handler.Queries
{
    public class GetStatusOfShipListRequestHandler : IRequestHandler<GetStatusOfShipListRequest, PagedResult<StatusOfShipDto>>
    {
        private readonly ISchoolManagementRepository<Domain.StatusOfShip> _statusOfShipRepository;
        private readonly IMapper _mapper;

        public GetStatusOfShipListRequestHandler(ISchoolManagementRepository<Domain.StatusOfShip> statusOfShipRepository, IMapper mapper)
        {
            _statusOfShipRepository = statusOfShipRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<StatusOfShipDto>> Handle(GetStatusOfShipListRequest request, CancellationToken cancellationToken)
        {

            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);


            IQueryable<Domain.StatusOfShip> statusOfShips = _statusOfShipRepository.FilterWithInclude(
                x => string.IsNullOrEmpty(request.QueryParams.SearchText) ||
                     x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText), "BaseSchoolName"
            );

            
            var totalCount = await statusOfShips.CountAsync(cancellationToken);

          
            statusOfShips = statusOfShips
                .OrderByDescending(x => x.StatusOfShipId)
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

       
            var statusOfShipDtos = await _mapper.ProjectTo<StatusOfShipDto>(statusOfShips).ToListAsync();

        
            var result = new PagedResult<StatusOfShipDto>(
                statusOfShipDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize
            );

            return result;
        }
    }
}
