using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.AppFeature.Requests.Commands;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.AppFeature.Handlers.Commands
{
    public class DeleteFeatureCommandHandler : IRequestHandler<DeleteFeatureCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public DeleteFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
        {  
            var Feature = await _unitOfWork.Repository<Feature>().Get(request.Id);

            if (Feature == null)  
                throw new NotFoundException(nameof(Feature), request.Id);
             
            await _unitOfWork.Repository<Feature>().Delete(Feature); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}