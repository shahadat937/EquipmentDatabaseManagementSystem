using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BaseSchoolNames.Validators;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Commands   
{
    public class CreateBaseSchoolNameCommandHandler : IRequestHandler<CreateBaseSchoolNameCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public CreateBaseSchoolNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateBaseSchoolNameCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBaseSchoolNameDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.BaseSchoolNameDto);

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

                if (request.BaseSchoolNameDto.Image != null)
                {

                    var fileName = Path.GetFileName(request.BaseSchoolNameDto.Image.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\base-school-name", uniqueFileName);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.BaseSchoolNameDto.Image.CopyToAsync(fileSteam);
                    }


                }
                var BaseSchoolName = _mapper.Map<BaseSchoolName>(request.BaseSchoolNameDto);

                BaseSchoolName.SchoolLogo = request.BaseSchoolNameDto.SchoolLogo ?? "files/base-school-name/" + uniqueFileName;
                BaseSchoolName = await _unitOfWork.Repository<BaseSchoolName>().Add(BaseSchoolName);
                
                await _unitOfWork.Save();

                BaseSchoolName.FirstLevel = BaseSchoolName.BranchLevel == 1 ? BaseSchoolName.BaseSchoolNameId : BaseSchoolName.FirstLevel;
                BaseSchoolName.SecondLevel = BaseSchoolName.BranchLevel == 2 ? BaseSchoolName.BaseSchoolNameId : BaseSchoolName.SecondLevel;
                BaseSchoolName.ThirdLevel = BaseSchoolName.BranchLevel == 3 ? BaseSchoolName.BaseSchoolNameId : BaseSchoolName.ThirdLevel;
                BaseSchoolName.FourthLevel = BaseSchoolName.BranchLevel == 4 ? BaseSchoolName.BaseSchoolNameId : BaseSchoolName.FourthLevel;
               
                await _unitOfWork.Repository<BaseSchoolName>().Update(BaseSchoolName);

                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful"; 
                response.Id = BaseSchoolName.BaseSchoolNameId;
            }

            return response;
        }
    }
}
