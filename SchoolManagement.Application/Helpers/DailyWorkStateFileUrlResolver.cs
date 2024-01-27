using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.DailyWorkState;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Helpers
{
    public class DailyWorkStateFileUrlResolver : IValueResolver<DailyWorkState, DailyWorkStateDto,  string>
    {
        private readonly IConfiguration _config;
        public DailyWorkStateFileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(DailyWorkState source, DailyWorkStateDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.FileUpload))
            {

                return _config["ApiUrl"] + source.FileUpload;
            }


            return null;
        }


    }
}
