using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BaseSchoolNames.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Commands;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Commands
{  
    public class UpdateBaseSchoolNameCommandHandler : IRequestHandler<UpdateBaseSchoolNameCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public UpdateBaseSchoolNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(UpdateBaseSchoolNameCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBaseSchoolNameDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.CreateBaseSchoolNameDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult); 
             
            var BaseSchoolName = await _unitOfWork.Repository<BaseSchoolName>().Get(request.CreateBaseSchoolNameDto.BaseSchoolNameId); 

            if (BaseSchoolName is null)  
                throw new NotFoundException(nameof(BaseSchoolName), request.CreateBaseSchoolNameDto.BaseSchoolNameId);

            /////// File Upload //////////
            
            string uniqueFileName = null;

            if (request.CreateBaseSchoolNameDto.Image != null)
            {

                var fileName = Path.GetFileName(request.CreateBaseSchoolNameDto.Image.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\base-school-name", uniqueFileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateBaseSchoolNameDto.Image.CopyToAsync(fileSteam);
                }

            }
            _mapper.Map(request.CreateBaseSchoolNameDto, BaseSchoolName);

            BaseSchoolName.SchoolLogo = request.CreateBaseSchoolNameDto.Image != null ? "files/base-school-name/" + uniqueFileName : BaseSchoolName.SchoolLogo.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<BaseSchoolName>().Update(BaseSchoolName); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}