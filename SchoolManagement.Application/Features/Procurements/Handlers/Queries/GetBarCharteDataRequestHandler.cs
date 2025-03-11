using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetBarCharteDataRequestHandler : IRequestHandler<GetBarCharteDataRequest, ProcurementDataForBarChartDto>
    {

        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;

        public GetBarCharteDataRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository, IMapper mapper)
        {
            _ProcurementRepository = ProcurementRepository;
            _mapper = mapper;

        }
        public Task<ProcurementDataForBarChartDto> Handle(GetBarCharteDataRequest request, CancellationToken cancellationToken)
        {
            var procurementData = _ProcurementRepository.FilterWithInclude(x => true);

            var sentForAipCount = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate == null && x.TenderFloatedDate == null && x.ContractSignedDate == null && x.IndentSentDate == null && x.SentForContractDate == null && (x.ProcurementTenderOpenings == null ||
                !x.ProcurementTenderOpenings.Any(p => p.TenderOpeningDate != null))).Count();

            var aipAprrovedCount = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.TenderFloatedDate == null && x.ContractSignedDate == null && x.IndentSentDate == null && x.SentForContractDate == null && (x.ProcurementTenderOpenings == null ||
             !x.ProcurementTenderOpenings.Any(p => p.TenderOpeningDate != null))).Count();

            var indentCount = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.IndentSentDate != null && x.TenderFloatedDate == null && x.ContractSignedDate == null && x.SentForContractDate == null && (x.ProcurementTenderOpenings == null ||
              !x.ProcurementTenderOpenings.Any(p => p.TenderOpeningDate != null))).Count();


            var floatingCount = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.IndentSentDate != null && x.TenderFloatedDate != null && x.ContractSignedDate == null && x.SentForContractDate == null && (x.ProcurementTenderOpenings == null ||
              !x.ProcurementTenderOpenings.Any(p => p.TenderOpeningDate != null))).Count();

            var offerReceive = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.IndentSentDate != null && x.TenderFloatedDate != null && x.OfferReceivedDateAndUpdateEvaluation != null && x.ContractSignedDate == null && x.SentForContractDate == null).Count();

            var contactSigned = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.IndentSentDate != null && x.TenderFloatedDate != null && x.OfferReceivedDateAndUpdateEvaluation != null && x.SentForContractDate != null && x.ContractSignedDate != null).Count();

            var aipSentItems = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate == null && x.TenderFloatedDate == null && x.ContractSignedDate == null && x.IndentSentDate == null && x.SentForContractDate == null && (x.ProcurementTenderOpenings == null ||
                !x.ProcurementTenderOpenings.Any(p => p.TenderOpeningDate != null))).Select(x => x.EqupmentName.Name)
                .ToList();

            var aipAprrovedItems = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.TenderFloatedDate == null && x.ContractSignedDate == null && x.IndentSentDate == null && x.SentForContractDate == null && (x.ProcurementTenderOpenings == null ||
             !x.ProcurementTenderOpenings.Any(p => p.TenderOpeningDate != null))).Select(x => x.EqupmentName.Name)
                .ToList();

            var indentItems = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.IndentSentDate != null && x.TenderFloatedDate == null && x.ContractSignedDate == null && x.SentForContractDate == null && (x.ProcurementTenderOpenings == null ||
             !x.ProcurementTenderOpenings.Any(p => p.TenderOpeningDate != null))).Select(x => x.EqupmentName.Name)
                .ToList();

            var floatingItems = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.IndentSentDate != null && x.TenderFloatedDate != null && x.ContractSignedDate == null && x.SentForContractDate == null && (x.ProcurementTenderOpenings == null ||
              !x.ProcurementTenderOpenings.Any(p => p.TenderOpeningDate != null))).Select(x => x.EqupmentName.Name)
                .ToList(); ;

            var offerReceiveItems = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.IndentSentDate != null && x.TenderFloatedDate != null && x.OfferReceivedDateAndUpdateEvaluation != null && x.ContractSignedDate == null && x.SentForContractDate == null).Select(x => x.EqupmentName.Name)
                .ToList();

            var contactSignedItems = procurementData.Where(x => x.SentForAIPDate != null && x.AIPApprovalDate != null && x.IndentSentDate != null && x.TenderFloatedDate != null && x.OfferReceivedDateAndUpdateEvaluation != null && x.SentForContractDate != null && x.ContractSignedDate != null).Select(x => x.EqupmentName.Name)
                .ToList();

            var result = new ProcurementDataForBarChartDto
            {
                SentAipCount = sentForAipCount,
                AipApprovedCount = aipAprrovedCount,
                IndentCount = indentCount,
                TenderFloatedCount = floatingCount,
                OfferReceivedCount = offerReceive,
                CountractSingCount = contactSigned,

                SentAipItems = aipSentItems,
                AipApprovedItems = aipAprrovedItems,
                IndentItems = indentItems,
                TenderFloatedItem = floatingItems,
                OfferEvulationItems = offerReceiveItems,
                ContractSingedItems = contactSignedItems
            };

            return Task.FromResult(result);
        }
    }
}