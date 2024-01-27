using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DgdpNssd.Validators;
using SchoolManagement.Application.Features.DgdpNssds.Requests.Commands;

namespace SchoolManagement.Application.Features.DgdpNssds.Handlers.Commands
{
    public class UpdateDgdpNssdCommandHandler : IRequestHandler<UpdateDgdpNssdCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDgdpNssdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDgdpNssdCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDgdpNssdDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.DgdpNssdDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var DgdpNssd = await _unitOfWork.Repository<DgdpNssd>().Get(request.DgdpNssdDto.DgdpNssdId);

            if (DgdpNssd is null)
                throw new NotFoundException(nameof(DgdpNssd), request.DgdpNssdDto.DgdpNssdId);

            _mapper.Map(request.DgdpNssdDto, DgdpNssd);

            await _unitOfWork.Repository<DgdpNssd>().Update(DgdpNssd);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
