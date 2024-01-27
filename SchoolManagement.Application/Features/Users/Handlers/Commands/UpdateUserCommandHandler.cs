using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.User.Validators;
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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.UserDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult); 
             
            var User = await _unitOfWork.Repository<User>().Get(request.UserDto.UserId); 

            if (User is null)  
                throw new NotFoundException(nameof(User), request.UserDto.UserId); 

            _mapper.Map(request.UserDto, User);  

            await _unitOfWork.Repository<User>().Update(User); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}