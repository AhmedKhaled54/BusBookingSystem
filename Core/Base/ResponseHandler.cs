using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public  class ResponseHandler
    {
        public ResponseHandler()
        {
             
        }


        public Response<T> Success<T> (T entity ,string message = null)
        {
            return new Response<T>
            {
                Data = entity,
                Status = System.Net.HttpStatusCode.OK,
                Message = message ?? "Successfuly",
                Success = true

            };
        }


        public Response<T> BadRequest<T>(string message = null)
        {
            return new Response<T>
            {
                Message = message ?? "BadRequest",
                Success = false,
                Status = System.Net.HttpStatusCode.BadRequest
            };
        }


        public Response<T> NotFound<T>( string message =null)
        {
            return new Response<T>
            {
                Message = message ?? "Not Found ",
                Success = false,
                Status = System.Net.HttpStatusCode.NotFound
            };
        }
        public Response<T> Created<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                Status = HttpStatusCode.Created,
                Success = true,
                Message = "Created Successfuly"
            };
        }

        public Response<T> UnAuthorize<T>(string message = null)
        {
            return new Response<T>()
            {
                Message = message == null ? "UnAthorize" : message,
                Success = false,
                Status = HttpStatusCode.Unauthorized
            };
        }



    }
}
