using AutoMapper;
using FluentValidation;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.StateOfEquipments.Validators;
using SchoolManagement.Application.DTOs.StatusOfShip.Validators;
using SchoolManagement.Application.Features.StatusOfShip.Request.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfShip.Handler.Command
{
    public class CreateStatusOfShipCommandHandler : IRequestHandler<CreateStatusOfShipCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStatusOfShipCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateStatusOfShipCommand request, CancellationToken cancellationToken)
        {

            var response = new BaseCommandResponse();

     
            var validator = new CreateStatusOfShipDtoValidator();
            var validationResult = await validator.ValidateAsync(request.StatusOfShipDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

     
            var statusOfShip = _mapper.Map<Domain.StatusOfShip>(request.StatusOfShipDto);

            statusOfShip = await _unitOfWork.Repository<Domain.StatusOfShip>().Add(statusOfShip);

            try
            {

               

                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = statusOfShip.StatusOfShipId;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error occurred while saving.";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
