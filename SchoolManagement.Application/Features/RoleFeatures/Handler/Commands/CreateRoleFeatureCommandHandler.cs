using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RoleFeature.Validators;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.RoleFeatures.Handler.Commands
{
    public class CreateRoleFeatureCommandHandler : IRequestHandler<CreateRoleFeatureCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public CreateRoleFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateRoleFeatureCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateRoleFeatureDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RoleFeatureDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var RoleFeature = _mapper.Map<RoleFeature>(request.RoleFeatureDto);

                RoleFeature = await _unitOfWork.Repository<RoleFeature>().Add(RoleFeature);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                //response.Id = RoleFeature.RoleId;
            }

            return response;
        }
    }
}
