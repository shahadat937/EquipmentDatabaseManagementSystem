using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Users.Requests.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Users.Handlers.Commands  
{ 
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {  
            var User = await _unitOfWork.Repository<User>().Get(request.Id);

            if (User == null)  
                throw new NotFoundException(nameof(User), request.Id);
             
            await _unitOfWork.Repository<User>().Delete(User); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}