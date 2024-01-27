using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Modules.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Modules.Requests.Commands;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Modules.Handlers.Commands
{
    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public UpdateModuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateModuleDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.ModuleDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult); 
             
            var Module = await _unitOfWork.Repository<Module>().Get(request.ModuleDto.ModuleId); 

            if (Module is null)  
                throw new NotFoundException(nameof(Module), request.ModuleDto.ModuleId); 

            _mapper.Map(request.ModuleDto, Module);  

            await _unitOfWork.Repository<Module>().Update(Module); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}