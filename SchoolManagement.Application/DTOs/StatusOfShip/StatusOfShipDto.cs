using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.StatusOfShip
{
    public class StatusOfShipDto
    {
        public int StatusOfShipId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? OperationalStatusId { get; set; }
        public String? ReasonOfBeingNonOperation { get; set; }
        public DateTime? DateFromNonOperational { get; set; }
        public string? SchoolName { get; set; }
        public string? OperationalStatus { get; set; }
        public int? MenuPosition { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
