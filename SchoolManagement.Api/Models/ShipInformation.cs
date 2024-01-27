using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class ShipInformation
    {
        public int ShipInformationId { get; set; }
        public int? AuthorityId { get; set; }
        public int? BaseNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int SqnId { get; set; }
        public int? OperationalStatusId { get; set; }
        public int? ShipTypeId { get; set; }
        public string ControlCode { get; set; }
        public string CallSign { get; set; }
        public string NickName { get; set; }
        public string Class { get; set; }
        public string FileUpload { get; set; }
        public string DisplacementFullLoad { get; set; }
        public string DisplacementLightWeight { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? DateOfCommission { get; set; }
        public string LengthLoa { get; set; }
        public string LengthHom { get; set; }
        public string BreadthBmld { get; set; }
        public string Depth { get; set; }
        public string DraftFwd { get; set; }
        public string DraftAft { get; set; }
        public string FreshWaterCapacity { get; set; }
        public string MaximumSpeed { get; set; }
        public string FualCapacity { get; set; }
        public string MaximumContinuousSpeed { get; set; }
        public string LuboilCapacity { get; set; }
        public string CruisingSpeed { get; set; }
        public string EconomicSpeed { get; set; }
        public string Address { get; set; }
        public string Authority { get; set; }
        public string ContactNo { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }

        public virtual BaseSchoolName AuthorityNavigation { get; set; }
        public virtual BaseSchoolName BaseName { get; set; }
        public virtual BaseSchoolName BaseSchoolName { get; set; }
        public virtual OperationalStatus OperationalStatus { get; set; }
        public virtual ShipType ShipType { get; set; }
        public virtual Sqn Sqn { get; set; }
    }
}
