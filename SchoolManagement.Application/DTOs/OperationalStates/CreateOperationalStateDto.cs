namespace SchoolManagement.Application.DTOs.OperationalStates
{
    public class CreateOperationalStateDto : IOperationalStateDto
    {
        public int OperationalStateId { get; set; }
        public int? EquipmentNameId { get; set; }
        public DateTime? DateOfDefect { get; set; }
        public string? DurationOfDefect { get; set; }
        public string? CausesOfDefect { get; set; }
        public string? StepsTakenByShipsStaff { get; set; }
        public string? StepTakenByDockyard { get; set; }
        public string? StepTakenByNhq { get; set; }
        public string? StepTakenByOem { get; set; }
        public string? PartNo { get; set; }
        public int? SparePartsHeldIn { get; set; }
        public string? ProcurementStatusOfSpare { get; set; }
        public string? ImpactOnOtherSyatem { get; set; }
        public string? RequirmentOfTheShip { get; set; }
        public string? Remarks { get; set; }
        public string? DescriptionOfDefect { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 