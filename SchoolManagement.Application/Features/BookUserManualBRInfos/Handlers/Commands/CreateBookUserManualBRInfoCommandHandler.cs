using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo.Validators;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Handlers.Commands
{
    public class CreateBookUserManualBRInfoCommandHandler : IRequestHandler<CreateBookUserManualBRInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookUserManualBRInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBookUserManualBRInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBookUserManualBRInfoDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BookUserManualBRInfoDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {

                /////// File Upload //////////
                string uniqueFileName = null;

                if (request.BookUserManualBRInfoDto.Document != null)
                {

                    var fileName = Path.GetFileName(request.BookUserManualBRInfoDto.Document.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\Bbook-user-manual", uniqueFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.BookUserManualBRInfoDto.Document.CopyToAsync(fileSteam);
                    }
                }

                var BookUserManualBRInfo = _mapper.Map<BookUserManualBRInfo>(request.BookUserManualBRInfoDto);
                BookUserManualBRInfo.UploadDocument = request.BookUserManualBRInfoDto.UploadDocument ?? "files/Bbook-user-manual/" + uniqueFileName;
                BookUserManualBRInfo = await _unitOfWork.Repository<BookUserManualBRInfo>().Add(BookUserManualBRInfo);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = BookUserManualBRInfo.BookUserManualBRInfoId;
            }

            return response;
        }
    }
}
