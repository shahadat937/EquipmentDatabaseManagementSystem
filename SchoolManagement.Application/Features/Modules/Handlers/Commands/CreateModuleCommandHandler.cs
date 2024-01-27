using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Modules.Validators;
using SchoolManagement.Application.Features.Modules.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Modules.Handlers.Commands
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public CreateModuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateModuleDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.ModuleDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Module = _mapper.Map<Module>(request.ModuleDto); 

                Module = await _unitOfWork.Repository<Module>().Add(Module);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful"; 
                response.Id = Module.ModuleId;
            }

            return response;
        }
    }
}
