using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.CodeValueTypes.Requests.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.CodeValueTypes.Handlers.Commands  
{ 
    public class DeleteCodeValueTypeCommandHandler : IRequestHandler<DeleteCodeValueTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public DeleteCodeValueTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(DeleteCodeValueTypeCommand request, CancellationToken cancellationToken)
        {  
            var CodeValueType = await _unitOfWork.Repository<CodeValueType>().Get(request.CodeValueTypeId);

            if (CodeValueType == null)  
                throw new NotFoundException(nameof(CodeValueType), request.CodeValueTypeId);
             
            await _unitOfWork.Repository<CodeValueType>().Delete(CodeValueType); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}