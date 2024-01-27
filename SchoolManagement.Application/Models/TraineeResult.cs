using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Models
{
  public class TraineeResult<T>
    {
        public List<T> Items { get; set; }
        //public int TotalPages { get; set; }
        //public int ItemsFrom { get; set; }
        //public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }

        public TraineeResult(List<T> items,int totalCount)
        {
            Items = items;
            TotalItemsCount = totalCount;
            //ItemsFrom = pageSize * (pageNumber - 1) + 1;
            //ItemsTo = ItemsFrom + pageSize - 1;
            //TotalPages =(int) Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
