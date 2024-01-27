using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Envelope.Validators;
using SchoolManagement.Application.Features.Envelopes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Envelopes.Handlers.Commands
{
    public class CreateEnvelopeCommandHandler : IRequestHandler<CreateEnvelopeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEnvelopeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEnvelopeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEnvelopeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EnvelopeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Envelope = _mapper.Map<Envelope>(request.EnvelopeDto);

                Envelope = await _unitOfWork.Repository<Envelope>().Add(Envelope);

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
                response.Id = Envelope.EnvelopeId;
            }

            return response;
        }
    }
}
