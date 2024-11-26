using AutoMapper;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.DTOs.ShipDrowings;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Helpers
{
    public class YearlyReturnFileUrlResolver : IValueResolver<YearlyReturn, YearlyReturnDto, string>
    {

        private readonly IConfiguration _config;
        public YearlyReturnFileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

       

        public string Resolve(YearlyReturn source, YearlyReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.FileUpload))
            {

                return _config["ApiUrl"] + source.FileUpload;
            }


            return null;
        }
    }
}
