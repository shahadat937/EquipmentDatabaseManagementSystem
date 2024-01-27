using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.RoleFeatures.Handler.Commands
{
    public class DeleteRoleFeatureCommandHandler : IRequestHandler<DeleteRoleFeatureCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRoleFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteRoleFeatureCommand request, CancellationToken cancellationToken)
        {
            var RoleFeature = await _unitOfWork.Repository<RoleFeature>().FindOneAsync(x => x.RoleId == request.RoleId && x.FeatureKey == request.FeatureId);

            //if (RoleFeature == null)
                //throw new NotFoundException(nameof(RoleFeature), request.RoleId, request.FeatureId);

           
            try
            {
                    await _unitOfWork.Repository<RoleFeature>().Delete(RoleFeature);
                    await _unitOfWork.Save();
            }
                catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Unit.Value;
        }
    }
}