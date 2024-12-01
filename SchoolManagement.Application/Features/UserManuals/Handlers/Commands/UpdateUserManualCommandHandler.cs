using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.UserManual.Validators;
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
    public class UpdateUserManualCommandHandler : IRequestHandler<UpdateUserManualCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserManualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserManualCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserManualDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateUserManualDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var UserManual = await _unitOfWork.Repository<UserManual>().Get(request.CreateUserManualDto.UserManualId);

            if (UserManual is null)
                throw new NotFoundException(nameof(UserManual), request.CreateUserManualDto.UserManualId);

            string uniqueFileName = null;

            if (request.CreateUserManualDto.DocFile != null)
            {

                var fileName = Path.GetFileName(request.CreateUserManualDto.DocFile.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\user-manuals", uniqueFileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateUserManualDto.DocFile.CopyToAsync(fileSteam);
                }

            }
            _mapper.Map(request.CreateUserManualDto, UserManual);
            UserManual.Doc = request.CreateUserManualDto.Doc != null ? "files/user-manuals/" + uniqueFileName : UserManual.Doc.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<UserManual>().Update(UserManual);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
