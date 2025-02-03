using AutoMapper;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Helpers
{
    public class ShipEquipmentFileUrlResolver : IValueResolver<ShipEquipmentInfo, ShipEquipmentInfoDto, string>
    {
        private readonly IConfiguration _config;
        public ShipEquipmentFileUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(ShipEquipmentInfo source, ShipEquipmentInfoDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.FileUpload))
            {

                return _config["ApiUrl"] + source.FileUpload;
            }


            return null;
        }
    }
}
