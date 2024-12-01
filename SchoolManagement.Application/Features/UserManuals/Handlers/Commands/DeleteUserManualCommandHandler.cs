using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.UserManuals.Requests.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.UserManuals.Handlers.Commands
{
    public class DeleteUserManualCommandHandler : IRequestHandler<DeleteUserManualCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserManualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteUserManualCommand request, CancellationToken cancellationToken)
        {
            var UserManual = await _unitOfWork.Repository<UserManual>().Get(request.UserManualId);

            if (UserManual == null)
                throw new NotFoundException(nameof(UserManual), request.UserManualId);

            await _unitOfWork.Repository<UserManual>().Delete(UserManual);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
