using AutoMapper;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;
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
    public class HalfYearlyReturnFileUrlResolver : IValueResolver<HalfYearlyReturn, HalfYearlyReturnDto, string>
    {

        private readonly IConfiguration _config;
        public HalfYearlyReturnFileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

       

        public string Resolve(HalfYearlyReturn source, HalfYearlyReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.UploadDocument))
            {

                return _config["ApiUrl"] + source.UploadDocument;
            }


            return null;
        }
    }
}
