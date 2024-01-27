using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Envelopes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Envelopes.Handlers.Commands
{
    public class DeleteEnvelopeCommandHandler : IRequestHandler<DeleteEnvelopeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEnvelopeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEnvelopeCommand request, CancellationToken cancellationToken)
        {
            var Envelope = await _unitOfWork.Repository<Envelope>().Get(request.EnvelopeId);

            if (Envelope == null)
                throw new NotFoundException(nameof(Envelope), request.EnvelopeId);

            await _unitOfWork.Repository<Envelope>().Delete(Envelope);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
