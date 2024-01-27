using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.ShipInformations;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Helpers
{
    public class FileUrlResolver : IValueResolver<ShipInformation, ShipInformationDto,  string>
    {
        private readonly IConfiguration _config;
        public FileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(ShipInformation source, ShipInformationDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.FileUpload))
            {

                return _config["ApiUrl"] + source.FileUpload;
            }


            return null;
        }


    }
}
