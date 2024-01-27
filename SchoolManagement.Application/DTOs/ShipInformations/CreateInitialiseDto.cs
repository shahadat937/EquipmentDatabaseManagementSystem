using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs.ShipInformations;

namespace SchoolManagement.Application.DTOs.ReadingMaterial
{
    public class CreateInitialiseDto
    {
        public IFormFile Doc { get; set; }
        public CreateShipInformationDto ShipInformationForm { get; set; }
    }
}
 