using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FcLc.Validators;
using SchoolManagement.Application.Features.FcLcs.Requests.Commands;

namespace SchoolManagement.Application.Features.FcLcs.Handlers.Commands
{
    public class UpdateFcLcCommandHandler : IRequestHandler<UpdateFcLcCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFcLcCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFcLcCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFcLcDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.FcLcDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var FcLc = await _unitOfWork.Repository<FcLc>().Get(request.FcLcDto.FcLcId);

            if (FcLc is null)
                throw new NotFoundException(nameof(FcLc), request.FcLcDto.FcLcId);

            _mapper.Map(request.FcLcDto, FcLc);

            await _unitOfWork.Repository<FcLc>().Update(FcLc);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
