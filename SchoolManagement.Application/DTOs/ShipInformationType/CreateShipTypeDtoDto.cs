﻿namespace SchoolManagement.Application.DTOs.ShipInformationTypes
{
    public class CreateShipTypeDto : IShipTypeDto
    {
        public int ShipTypeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 