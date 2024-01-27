using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs.ShipDrowinges;

namespace SchoolManagement.Application.DTOs.ShipDrowingBoards
{
    public class CreateShipDrowing
  {
    public IFormFile Doc { get; set; }
    public CreateShipDrowingDto ShipDrowingForm { get; set; }
  }
} 
