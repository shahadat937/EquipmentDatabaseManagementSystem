using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DealingOfficer.Validators;
using SchoolManagement.Application.Features.DealingOfficers.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DealingOfficers.Handlers.Commands
{
    public class CreateDealingOfficerCommandHandler : IRequestHandler<CreateDealingOfficerCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDealingOfficerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDealingOfficerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDealingOfficerDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DealingOfficerDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var DealingOfficer = _mapper.Map<DealingOfficer>(request.DealingOfficerDto);

                DealingOfficer = await _unitOfWork.Repository<DealingOfficer>().Add(DealingOfficer);

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
                response.Id = DealingOfficer.DealingOfficerId;
            }

            return response;
        }
    }
}
