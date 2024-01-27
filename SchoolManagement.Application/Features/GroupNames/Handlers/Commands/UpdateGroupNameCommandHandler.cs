using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.GroupNames.Validators;
using SchoolManagement.Application.Features.GroupNames.Requests.Commands;

namespace SchoolManagement.Application.Features.GroupNames.Handlers.Commands
{
    public class UpdateGroupNameCommandHandler : IRequestHandler<UpdateGroupNameCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGroupNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGroupNameCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateGroupNameDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.GroupNameDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var GroupName = await _unitOfWork.Repository<GroupName>().Get(request.GroupNameDto.GroupNameId);

            if (GroupName is null)
                throw new NotFoundException(nameof(GroupName), request.GroupNameDto.GroupNameId);

            _mapper.Map(request.GroupNameDto, GroupName);

            await _unitOfWork.Repository<GroupName>().Update(GroupName);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
