﻿using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.DailyWorkState;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyWorkStates.Handlers.Queries
{
    public class GetDailyWorkStateListForActionTakenYesRequestHandler : IRequestHandler<GetDailyWorkStateListForActionTakenYesRequest, List<DailyWorkStateDto>>
    {
        private readonly ISchoolManagementRepository<DailyWorkState> _DailyWorkStateRepository;

        private readonly IMapper _mapper;
        public GetDailyWorkStateListForActionTakenYesRequestHandler(ISchoolManagementRepository<DailyWorkState> DailyWorkStateRepository, IMapper mapper)
        {
            _DailyWorkStateRepository = DailyWorkStateRepository;
            _mapper = mapper;
        }

        public async Task<List<DailyWorkStateDto>> Handle(GetDailyWorkStateListForActionTakenYesRequest request, CancellationToken cancellationToken)
        {
            IQueryable<DailyWorkState> DailyWorkStates = _DailyWorkStateRepository.FilterWithInclude(x => x.ActionTakenId ==1 , "LetterType", "DealingOfficer", "ActionTaken", "Priority");

            var DailyWorkStateDtos = _mapper.Map<List<DailyWorkStateDto>>(DailyWorkStates);

            return DailyWorkStateDtos;
        }

    }
    //public class GetDailyWorkStateListForActionTakenNoRequestHandler : IRequestHandler<GetDailyWorkStateListForActionTakenNoRequest, PagedResult<DailyWorkStateDto>>
    //{

    //    private readonly ISchoolManagementRepository<DailyWorkState> _DailyWorkStateRepository;

    //    private readonly IMapper _mapper;

    //    public GetDailyWorkStateListForActionTakenNoRequestHandler(ISchoolManagementRepository<DailyWorkState> DailyWorkStateRepository, IMapper mapper)
    //    {
    //        _DailyWorkStateRepository = DailyWorkStateRepository;
    //        _mapper = mapper;
    //    }

    //    public async Task<PagedResult<DailyWorkStateDto>> Handle(GetDailyWorkStateListForActionTakenNoRequest request, CancellationToken cancellationToken)
    //    {
    //        var validator = new QueryParamsValidator();
    //        var validationResult = await validator.ValidateAsync(request.QueryParams);

    //        if (validationResult.IsValid == false)
    //            throw new ValidationException(validationResult);

    //        IQueryable<DailyWorkState> DailyWorkStates = _DailyWorkStateRepository.FilterWithInclude(x => (x.Subject.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "LetterType", "DealingOfficer", "ActionTaken", "Priority");
    //        var totalCount = DailyWorkStates.Count();
    //        DailyWorkStates = DailyWorkStates.OrderByDescending(x => x.DailyWorkStateId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize).Where(x => x.ActionTakenId != 1);

    //        var DailyWorkStateDtos = _mapper.Map<List<DailyWorkStateDto>>(DailyWorkStates);
    //        var result = new PagedResult<DailyWorkStateDto>(DailyWorkStateDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

    //        return result;


    //    }
    //}
}
