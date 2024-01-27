﻿using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetSelectedShipEquipmentInfoRequestHandler : IRequestHandler<GetSelectedShipEquipmentInfoRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _ShipEquipmentInfoRepository;


        public GetSelectedShipEquipmentInfoRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> ShipEquipmentInfoRepository)
        {
            _ShipEquipmentInfoRepository = ShipEquipmentInfoRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShipEquipmentInfoRequest request, CancellationToken cancellationToken)
        {
            ICollection<ShipEquipmentInfo> codeValues = await _ShipEquipmentInfoRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Model,
                Value = x.ShipEquipmentInfoId
            }).ToList();
            return selectModels;
        }
    }
}
