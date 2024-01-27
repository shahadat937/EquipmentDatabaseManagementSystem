using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Commands;
using SchoolManagement.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Commands  
{ 
    public class DeleteBaseSchoolNameCommandHandler : IRequestHandler<DeleteBaseSchoolNameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public DeleteBaseSchoolNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(DeleteBaseSchoolNameCommand request, CancellationToken cancellationToken)
        {  
            var BaseSchoolName = await _unitOfWork.Repository<BaseSchoolName>().Get(request.BaseSchoolNameId);

            if (BaseSchoolName == null)  
                throw new NotFoundException(nameof(BaseSchoolName), request.BaseSchoolNameId);
             

            await _unitOfWork.Repository<BaseSchoolName>().Delete(BaseSchoolName); 



            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}