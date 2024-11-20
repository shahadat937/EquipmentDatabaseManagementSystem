using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.HalfYearlyReturn.Validators;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Handlers.Commands
{
    public class CreateHalfYearlyReturnCommandHandler : IRequestHandler<CreateHalfYearlyReturnCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHalfYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateHalfYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var halfYearly = _mapper.Map<List<HalfYearlyReturn>>(request.HalfYearlyReturnDto);

            foreach (var yearly in halfYearly)
            {
                await _unitOfWork.Repository<HalfYearlyReturn>().Add(yearly);
                await _unitOfWork.Save();
            }


            //var halfYearlyReturn = request.HalfYearlyReturnDto;
            //var halfYearlyReturnList = halfYearlyReturn.ShipEquipmentInfoList.Select(x => new HalfYearlyReturn()
            //{
            //    HalfYearlyReturnId = halfYearlyReturn.HalfYearlyReturnId,
            //    BaseSchoolNameId = x.BaseSchoolNameId,
            //    EquipmentCategoryId = halfYearlyReturn.EquipmentCategoryId,
            //    EqupmentNameId = halfYearlyReturn.EqupmentNameId,
            //    BrandId = halfYearlyReturn.BrandId,
            //    HalfYearlyRunningTime = x.HalfYearlyRunningTime,
            //    TotalRunningTime = x.TotalRunningTime,
            //    HalfYearlyProblem = x.HalfYearlyProblem,
            //    PowerSupplyUnit = x.PowerSupplyUnit,
            //    await _unitOfWork.Repository<HalfYearlyReturn>().AddRangeAsync(halfYearlyReturnList);

            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }


            response.Success = true;
            response.Message = "Creation Successful";
        

            return response;
        }
}
}
