using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo.Validators;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Commands;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Handlers.Commands
{
    public class UpdateBookUserManualBRInfoCommandHandler : IRequestHandler<UpdateBookUserManualBRInfoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UpdateBookUserManualBRInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<Unit> Handle(UpdateBookUserManualBRInfoCommand request, CancellationToken cancellationToken)
        {
            var apiUrl = _config["ApiUrl"];
            var validator = new CreateBookUserManualBRInfoDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.BookUserManualBRInfoDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var BookUserManualBRInfo = await _unitOfWork.Repository<BookUserManualBRInfo>().Get(request.BookUserManualBRInfoDto.BookUserManualBRInfoId);

            if (BookUserManualBRInfo is null)
                throw new NotFoundException(nameof(BookUserManualBRInfo), request.BookUserManualBRInfoDto.BookUserManualBRInfoId);

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

            _mapper.Map(request.BookUserManualBRInfoDto, BookUserManualBRInfo);

            if ((request.BookUserManualBRInfoDto.UploadDocument != null && request.BookUserManualBRInfoDto.Document != null) || (request.BookUserManualBRInfoDto.Document == null && request.BookUserManualBRInfoDto.UploadDocument != null) || (request.BookUserManualBRInfoDto.Document != null && request.BookUserManualBRInfoDto.UploadDocument == null))
            {
                BookUserManualBRInfo.UploadDocument = request.BookUserManualBRInfoDto.Document != null ? "files/Bbook-user-manual/" + uniqueFileName : BookUserManualBRInfo.UploadDocument.Replace(apiUrl, String.Empty);


            }
            else if (request.BookUserManualBRInfoDto.UploadDocument != null)
            {
                BookUserManualBRInfo.UploadDocument = BookUserManualBRInfo.UploadDocument.Replace(apiUrl, string.Empty);
            }
            else
            {
                BookUserManualBRInfo.UploadDocument = "";
            }


            await _unitOfWork.Repository<BookUserManualBRInfo>().Update(BookUserManualBRInfo);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
