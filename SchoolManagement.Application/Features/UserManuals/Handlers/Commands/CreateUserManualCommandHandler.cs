using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.UserManual.Validators;
using SchoolManagement.Application.Features.UserManuals.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.UserManuals.Handlers.Commands
{
    public class CreateUserManualCommandHandler : IRequestHandler<CreateUserManualCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserManualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateUserManualCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateUserManualDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UserManualDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                /////// File Upload //////////
                ///
                string uniqueFileName = null;

                if (request.UserManualDto.DocFile != null)
                {

                    var fileName = Path.GetFileName(request.UserManualDto.DocFile.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\user-manuals", uniqueFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.UserManualDto.DocFile.CopyToAsync(fileSteam);
                    }


                }
                var UserManual = _mapper.Map<UserManual>(request.UserManualDto);
                UserManual.Doc = request.UserManualDto.Doc ?? "files/user-manuals/" + uniqueFileName;
                UserManual = await _unitOfWork.Repository<UserManual>().Add(UserManual);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = UserManual.UserManualId;
            }

            return response;
        }
    }
}
