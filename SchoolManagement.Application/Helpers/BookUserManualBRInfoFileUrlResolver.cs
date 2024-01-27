using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Helpers
{
    public class BookUserManualBRInfoFileUrlResolver : IValueResolver<BookUserManualBRInfo, BookUserManualBRInfoDto,  string>
    {
        private readonly IConfiguration _config;
        public BookUserManualBRInfoFileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(BookUserManualBRInfo source, BookUserManualBRInfoDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.UploadDocument))
            {

                return _config["ApiUrl"] + source.UploadDocument;
            }


            return null;
        }


    }
}
