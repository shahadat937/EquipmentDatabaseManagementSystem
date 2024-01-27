using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Tec.Validators;
using SchoolManagement.Application.Features.Tecs.Requests.Commands;

namespace SchoolManagement.Application.Features.Tecs.Handlers.Commands
{
    public class UpdateTecCommandHandler : IRequestHandler<UpdateTecCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTecCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTecCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTecDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.TecDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Tec = await _unitOfWork.Repository<Tec>().Get(request.TecDto.TecId);

            if (Tec is null)
                throw new NotFoundException(nameof(Tec), request.TecDto.TecId);

            _mapper.Map(request.TecDto, Tec);

            await _unitOfWork.Repository<Tec>().Update(Tec);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
