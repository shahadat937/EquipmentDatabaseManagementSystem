using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RoleFeature.Validators;
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
    public class UpdateRoleFeatureCommandHandler : IRequestHandler<UpdateRoleFeatureCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoleFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRoleFeatureCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateRoleFeatureDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RoleFeatureDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            //var RoleFeature = await _unitOfWork.Repository<RoleFeature>().Get(request.RoleFeatureDto.RoleId);
            var RoleFeature = await _unitOfWork.Repository<RoleFeature>().FindOneAsync(x => x.RoleId == request.RoleId && x.FeatureKey == request.FeatureId);

            //if (RoleFeature is null)
            //    throw new NotFoundException(nameof(RoleFeature), request.RoleFeatureDto.RoleId);

            _mapper.Map(request.RoleFeatureDto, RoleFeature);

            await _unitOfWork.Repository<RoleFeature>().Update(RoleFeature);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}