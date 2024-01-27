using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Brands.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Brands.Handlers.Commands
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var Brand = await _unitOfWork.Repository<Brand>().Get(request.BrandId);

            if (Brand == null)
                throw new NotFoundException(nameof(Brand), request.BrandId);

            await _unitOfWork.Repository<Brand>().Delete(Brand);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
