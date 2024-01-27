using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Helpers
{
    public static class ConvertDatetimeExtensions
    {
        public static DateTime ConvertToDateTime(this string date)
        {
            DateTime dtFinaldate;
            try { dtFinaldate = Convert.ToDateTime(date); }
            catch (Exception e)
            {
                string[] sDate = date.Split('/');
                date = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                dtFinaldate = Convert.ToDateTime(date);
            }
            return dtFinaldate;
        }
    }
}
