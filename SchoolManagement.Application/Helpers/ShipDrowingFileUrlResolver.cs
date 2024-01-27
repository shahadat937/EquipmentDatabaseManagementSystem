using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Domain;
using System.IO;
using SchoolManagement.Application.DTOs.ShipDrowings;

namespace SchoolManagement.Application.Helpers
{
    public class ShipDrowingFileUrlResolver : IValueResolver<ShipDrowing, ShipDrowingDto, string>
    {

        private readonly IConfiguration _config;
        public ShipDrowingFileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(ShipDrowing source, ShipDrowingDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.FileUpload))
            {

                return _config["ApiUrl"] + source.FileUpload;
            }
           

            return null;
        }
        
    }
    
}
