using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem.Validator;
using SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Commands;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Handlers.Commands
{
    public class CreateOperationalStatusOfEquipmentSystemCommandHandler : IRequestHandler<CreateOperationalStatusOfEquipmentSystemCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOperationalStatusOfEquipmentSystemCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(CreateOperationalStatusOfEquipmentSystemCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateOperationalStatusOfEquipmentSystemDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OperationalStatusOfEquipmentSystemDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var OperationalStatusOfEquipmentSystem = _mapper.Map<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem>(request.OperationalStatusOfEquipmentSystemDto);

                OperationalStatusOfEquipmentSystem = await _unitOfWork.Repository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem>().Add(OperationalStatusOfEquipmentSystem);

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
                response.Id = OperationalStatusOfEquipmentSystem.OperationalStatusOfEquipmentSystemId;
            }

            return response;
        }
    }
}
