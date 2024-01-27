using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.GroupNames.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.GroupNames.Handlers.Commands
{
    public class DeleteGroupNameCommandHandler : IRequestHandler<DeleteGroupNameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteGroupNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteGroupNameCommand request, CancellationToken cancellationToken)
        {
            var GroupName = await _unitOfWork.Repository<GroupName>().Get(request.GroupNameId);

            if (GroupName == null)
                throw new NotFoundException(nameof(GroupName), request.GroupNameId);

            await _unitOfWork.Repository<GroupName>().Delete(GroupName);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
