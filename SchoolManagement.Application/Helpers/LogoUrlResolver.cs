using AutoMapper;
using SchoolManagement.Domain;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.DTOs.BaseSchoolNames;

namespace SchoolManagement.Application.Helpers
{
    public class LogoUrlResolver : IValueResolver<BaseSchoolName, BaseSchoolNameDto, string>
    {
        private readonly IConfiguration _config;
        public LogoUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(BaseSchoolName source, BaseSchoolNameDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.SchoolLogo))
            {

                return _config["ApiUrl"] + source.SchoolLogo;
            }

            return null;
        }
    }
}
