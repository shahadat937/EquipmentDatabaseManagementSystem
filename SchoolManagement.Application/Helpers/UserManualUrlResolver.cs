using AutoMapper;
using SchoolManagement.Application.DTOs.UserManual;
using SchoolManagement.Domain;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Helpers
{
    public class UserManualUrlResolver : IValueResolver<UserManual, UserManualDto, string>
    {


        private readonly IConfiguration _config;
        public UserManualUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(UserManual source, UserManualDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Doc))
            {

                return _config["ApiUrl"] + source.Doc;
            }

            return null;
        }
    }
}
