using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class CodeValueType : BaseDomainEntity
    {
        public CodeValueType()
        {
            CodeValues = new HashSet<CodeValue>();
        }

        public int CodeValueTypeId { get; set; }
        public string Type { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CodeValue> CodeValues { get; set; }
    }
}
