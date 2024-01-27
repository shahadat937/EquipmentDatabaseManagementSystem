using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Brands.Validators;
using SchoolManagement.Application.Features.Brands.Requests.Commands;

namespace SchoolManagement.Application.Features.Brands.Handlers.Commands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBrandDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.BrandDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Brand = await _unitOfWork.Repository<Brand>().Get(request.BrandDto.BrandId);

            if (Brand is null)
                throw new NotFoundException(nameof(Brand), request.BrandDto.BrandId);

            _mapper.Map(request.BrandDto, Brand);

            await _unitOfWork.Repository<Brand>().Update(Brand);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
