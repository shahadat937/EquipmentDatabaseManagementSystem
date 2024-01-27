using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Sqns.Validators;
using SchoolManagement.Application.Features.Sqns.Requests.Commands;

namespace SchoolManagement.Application.Features.Sqns.Handlers.Commands
{
    public class UpdateSqnCommandHandler : IRequestHandler<UpdateSqnCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSqnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSqnCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSqnDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.SqnDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Sqn = await _unitOfWork.Repository<Sqn>().Get(request.SqnDto.SqnId);

            if (Sqn is null)
                throw new NotFoundException(nameof(Sqn), request.SqnDto.SqnId);

            _mapper.Map(request.SqnDto, Sqn);

            await _unitOfWork.Repository<Sqn>().Update(Sqn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
