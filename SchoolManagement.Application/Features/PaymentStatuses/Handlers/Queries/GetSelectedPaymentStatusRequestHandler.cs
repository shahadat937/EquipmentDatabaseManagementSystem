using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PaymentStatuses.Handlers.Queries
{
    public class GetSelectedPaymentStatusRequestHandler : IRequestHandler<GetSelectedPaymentStatusRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PaymentStatus> _PaymentStatusRepository;


        public GetSelectedPaymentStatusRequestHandler(ISchoolManagementRepository<PaymentStatus> PaymentStatusRepository)
        {
            _PaymentStatusRepository = PaymentStatusRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPaymentStatusRequest request, CancellationToken cancellationToken)
        {
            ICollection<PaymentStatus> codeValues = await _PaymentStatusRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.PaymentStatusId
            }).ToList();
            return selectModels;
        }
    }
}
