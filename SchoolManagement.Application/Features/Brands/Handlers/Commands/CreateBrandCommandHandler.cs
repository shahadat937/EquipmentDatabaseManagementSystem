using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Brands.Validators;
using SchoolManagement.Application.Features.Brands.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Brands.Handlers.Commands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBrandDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BrandDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Brand = _mapper.Map<Brand>(request.BrandDto);

                Brand = await _unitOfWork.Repository<Brand>().Add(Brand);

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
                response.Id = Brand.BrandId;
            }

            return response;
        }
    }
}
