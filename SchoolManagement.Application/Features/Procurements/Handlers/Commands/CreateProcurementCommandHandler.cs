using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Procurement.Validators;
using SchoolManagement.Application.Features.Procurements.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Commands
{
    public class CreateProcurementCommandHandler : IRequestHandler<CreateProcurementCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProcurementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProcurementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProcurementDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProcurementDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            try
            {
                var procurement = _mapper.Map<Procurement>(request.ProcurementDto);
                procurement = await _unitOfWork.Repository<Procurement>().Add(procurement);
                await _unitOfWork.Save(); 

                if (request.ProcurementDto.BaseSchoolNamesDtos != null && request.ProcurementDto.BaseSchoolNamesDtos.Any())
                {
                    var baseSchoolNames = request.ProcurementDto.BaseSchoolNamesDtos
                        .Select(dto => new ProcurementBaseSchoolName
                        {
                            ProcurementId = procurement.ProcurementId, 
                            BaseSchoolNameId = dto.BaseSchoolNameId
                        }).ToList();

                    await _unitOfWork.Repository<ProcurementBaseSchoolName>().AddRangeAsync(baseSchoolNames);
                    await _unitOfWork.Save();
                }

                if (request.ProcurementDto.ProcurementTenderOpeningDto != null && request.ProcurementDto.ProcurementTenderOpeningDto.Any())
                {
                    var tenderOpenings = request.ProcurementDto.ProcurementTenderOpeningDto
                        .Select(dto => new ProcurementTenderOpening
                        {
                            ProcurementId = procurement.ProcurementId,
                            TenderOpeningDate = dto.TenderOpeningDate
                        }).ToList();

                    await _unitOfWork.Repository<ProcurementTenderOpening>().AddRangeAsync(tenderOpenings);
                    await _unitOfWork.Save();
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = procurement.ProcurementId;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                response.Success = false;
                response.Message = "An error occurred while creating procurement.";
            }

            return response;
        }

    }
}