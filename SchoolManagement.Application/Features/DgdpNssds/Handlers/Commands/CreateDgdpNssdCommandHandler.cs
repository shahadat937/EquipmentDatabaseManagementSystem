using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DgdpNssd.Validators;
using SchoolManagement.Application.Features.DgdpNssds.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DgdpNssds.Handlers.Commands
{
    public class CreateDgdpNssdCommandHandler : IRequestHandler<CreateDgdpNssdCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDgdpNssdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDgdpNssdCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDgdpNssdDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DgdpNssdDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var DgdpNssd = _mapper.Map<DgdpNssd>(request.DgdpNssdDto);

                DgdpNssd = await _unitOfWork.Repository<DgdpNssd>().Add(DgdpNssd);

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
                response.Id = DgdpNssd.DgdpNssdId;
            }

            return response;
        }
    }
}
