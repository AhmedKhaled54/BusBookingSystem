using MimeKit.Tnef;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Wrappers
{
    public  class Pagination<T>
    {
        public Pagination()
        {
             
        }

        public Pagination(List<T> data)
        {
            Data = data;
        }

        public Pagination(bool success,List<T> data=default,int pageSize=10,int totalCount=0, int currentPage=1, string message=null) : this(data)
        {
         
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
            CurrentPage = currentPage;
            Success = success;
            Message = message;
            Data = data;
        }


        public static Pagination<T> Paginated(List<T> data, int count, int pageindex, int pagesize)
            =>new (true,data,pagesize,count,pageindex);

        public List<T> Data  { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public bool PreviousPage => CurrentPage > 1;
        public bool NextPage => CurrentPage <TotalCount;
        public bool Success { get; set; }
        public string Message   { get; set; }

    }
}
