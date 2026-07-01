using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(string message)
        {
            Message = message;
            Success = false;

        }
        public Response(T data, string message = null)
        {
            Data = data;
            Message = message;

        }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
    }
}
