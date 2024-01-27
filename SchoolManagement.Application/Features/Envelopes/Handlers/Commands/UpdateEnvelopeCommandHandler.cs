using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Envelope.Validators;
using SchoolManagement.Application.Features.Envelopes.Requests.Commands;

namespace SchoolManagement.Application.Features.Envelopes.Handlers.Commands
{
    public class UpdateEnvelopeCommandHandler : IRequestHandler<UpdateEnvelopeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEnvelopeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEnvelopeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEnvelopeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.EnvelopeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Envelope = await _unitOfWork.Repository<Envelope>().Get(request.EnvelopeDto.EnvelopeId);

            if (Envelope is null)
                throw new NotFoundException(nameof(Envelope), request.EnvelopeDto.EnvelopeId);

            _mapper.Map(request.EnvelopeDto, Envelope);

            await _unitOfWork.Repository<Envelope>().Update(Envelope);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
