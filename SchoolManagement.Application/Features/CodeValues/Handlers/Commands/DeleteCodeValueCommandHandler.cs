using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.CodeValues.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CodeValues.Handlers.Commands
{
    public class DeleteCodeValueCommandHandler : IRequestHandler<DeleteCodeValueCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public DeleteCodeValueCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(DeleteCodeValueCommand request, CancellationToken cancellationToken)
        {  
            var CodeValue = await _unitOfWork.Repository<CodeValue>().Get(request.Id);

            if (CodeValue == null)  
                throw new NotFoundException(nameof(CodeValue), request.Id);
             
            await _unitOfWork.Repository<CodeValue>().Delete(CodeValue); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}