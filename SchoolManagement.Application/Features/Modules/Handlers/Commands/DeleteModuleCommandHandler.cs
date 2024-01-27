using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Modules.Requests.Commands;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Modules.Handlers.Commands
{
    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public DeleteModuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {  
            var Module = await _unitOfWork.Repository<Module>().Get(request.Id);

            if (Module == null)  
                throw new NotFoundException(nameof(Module), request.Id);
             
            await _unitOfWork.Repository<Module>().Delete(Module); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}