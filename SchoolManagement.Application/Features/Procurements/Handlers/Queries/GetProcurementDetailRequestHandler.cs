using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetProcurementDetailRequestHandler : IRequestHandler<GetProcurementDetailRequest, ProcurementDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;
        private readonly ISchoolManagementRepository<ProcurementBaseSchoolName> _ProcurementBaseSchoolName;
        private readonly ISchoolManagementRepository<ProcurementTenderOpening> _ProcurementTenderOpening;
        public GetProcurementDetailRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository, IMapper mapper, ISchoolManagementRepository<ProcurementBaseSchoolName> procurementBaseSchoolName, ISchoolManagementRepository<ProcurementTenderOpening> procurementTenderOpening)
        {
            _ProcurementRepository = ProcurementRepository;
            _mapper = mapper;
            _ProcurementBaseSchoolName = procurementBaseSchoolName;
            _ProcurementTenderOpening = procurementTenderOpening;
        }
        public async Task<ProcurementDto> Handle(GetProcurementDetailRequest request, CancellationToken cancellationToken)
        {
            var procurement = await _ProcurementRepository.Get(request.ProcurementId);

            // Fetch only the BaseSchoolNameId values
            var baseSchoolIds = await _ProcurementBaseSchoolName
                                 .Where(x => x.ProcurementId == request.ProcurementId)
                                 .Select(x => x.BaseSchoolNameId)
                                 .ToListAsync();

            var tenderOpenings = await _ProcurementTenderOpening
                                .Where(x => x.ProcurementId == request.ProcurementId)
                                .ToListAsync();

            var procurementDto = _mapper.Map<ProcurementDto>(procurement);

            procurementDto.BaseSchoolNameId =  baseSchoolIds;
            try
            {
                procurementDto.ProcurementTenderOpeningDto = tenderOpenings
                .Select(x => new ProcurementTenderOpeningDto
                {
 
                    ProcurementId = x.ProcurementId,
                    TenderOpeningDate = x.TenderOpeningDate,
                    TenderOpeningCount = x.TenderOpeningCount
                }).ToList();

            }
            catch (Exception ex)
            {

            }
   


            return procurementDto;
        }


    }
}
