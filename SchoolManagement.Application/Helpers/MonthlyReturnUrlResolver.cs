using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Helpers
{
    public class MonthlyReturnUrlResolver : IValueResolver<MonthlyReturn, MonthlyReturnDto,  string>
    {
        private readonly IConfiguration _config;
        public MonthlyReturnUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(MonthlyReturn source, MonthlyReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.UploadDocument))
            {

                return _config["ApiUrl"] + source.UploadDocument;
            }


            return null;
        }


    }
}
