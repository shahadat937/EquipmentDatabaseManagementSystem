using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Procurement.Validators;
using SchoolManagement.Application.Features.Procurements.Requests.Commands;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Commands
{
    public class UpdateProcurementCommandHandler : IRequestHandler<UpdateProcurementCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ProcurementBaseSchoolName> _procuremenetBaseSchoolNameRepository;
        private readonly ISchoolManagementRepository<ProcurementTenderOpening> _pocurementTenderOpeningRepository;

        public UpdateProcurementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<ProcurementBaseSchoolName> procuremenetBaseSchoolNameRepository, ISchoolManagementRepository<ProcurementTenderOpening> pocurementTenderOpeningRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _procuremenetBaseSchoolNameRepository = procuremenetBaseSchoolNameRepository;
            _pocurementTenderOpeningRepository = pocurementTenderOpeningRepository;
        }

        public async Task<Unit> Handle(UpdateProcurementCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProcurementDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ProcurementDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Procurement = await _unitOfWork.Repository<Procurement>().Get(request.ProcurementDto.ProcurementId);

            if (Procurement is null)
                throw new NotFoundException(nameof(Procurement), request.ProcurementDto.ProcurementId);

            _mapper.Map(request.ProcurementDto, Procurement);

            await _unitOfWork.Repository<Procurement>().Update(Procurement);
            await _unitOfWork.Save();

            var procurementBaseSchool = _procuremenetBaseSchoolNameRepository.FilterWithInclude(x => x.ProcurementId == request.ProcurementDto.ProcurementId);

            var procuremenTenderOpening = _pocurementTenderOpeningRepository.FilterWithInclude(x => x.ProcurementId == request.ProcurementDto.ProcurementId);

            await _procuremenetBaseSchoolNameRepository.RemoveRangeAsync(procurementBaseSchool);
            await _pocurementTenderOpeningRepository.RemoveRangeAsync(procuremenTenderOpening);

            if (request.ProcurementDto.BaseSchoolNameId != null && request.ProcurementDto.BaseSchoolNameId.Any())
            {
                var baseSchoolNames = request.ProcurementDto.BaseSchoolNameId
                    .Select(baseSchoolId => new ProcurementBaseSchoolName
                    {
                        ProcurementId = request.ProcurementDto.ProcurementId,
                        BaseSchoolNameId = baseSchoolId
                    }).ToList();

                await _unitOfWork.Repository<ProcurementBaseSchoolName>().AddRangeAsync(baseSchoolNames);
                await _unitOfWork.Save();
            }


            if (request.ProcurementDto.ProcurementTenderOpeningDto != null && request.ProcurementDto.ProcurementTenderOpeningDto.Any())
            {
                var tenderOpenings = request.ProcurementDto.ProcurementTenderOpeningDto
                    .Where(dto => dto.TenderOpeningDate != null)  // Filter out null TenderOpeningDate
                    .Select(dto => new ProcurementTenderOpening
                    {
                        ProcurementId = request.ProcurementDto.ProcurementId,
                        TenderOpeningDate = dto.TenderOpeningDate,
                        TenderOpeningCount = dto.TenderOpeningCount
                    }).ToList();

                if (tenderOpenings.Any())  // Ensure there are items to add
                {
                    await _unitOfWork.Repository<ProcurementTenderOpening>().AddRangeAsync(tenderOpenings);
                    await _unitOfWork.Save();
                }
            }




            return Unit.Value;
        }
    }
}
