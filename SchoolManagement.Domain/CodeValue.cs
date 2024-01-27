using SchoolManagement.Domain.Common;


namespace SchoolManagement.Domain
{
    public class CodeValue : BaseDomainEntity
    {
       

        public int CodeValueId { get; set; }
        public int CodeValueTypeId { get; set; }
        public string Code { get; set; } = null!;
        public string? TypeValue { get; set; }
        public string? AdditonalValue { get; set; }
        public string? DisplayCode { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }


        public virtual CodeValueType CodeValueType { get; set; } = null!;


    }
}
