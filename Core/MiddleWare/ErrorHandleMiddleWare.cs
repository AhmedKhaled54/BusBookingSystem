using Core.Base;
using FluentValidation;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.MiddleWare
{
    public class ErrorHandleMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ErrorHandleMiddleWare> _logger;

        public ErrorHandleMiddleWare(RequestDelegate next, IWebHostEnvironment environment, ILogger<ErrorHandleMiddleWare> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task Invoke(HttpContext conetxt)
        {
            try
            {
                await _next(conetxt);

            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                var response = conetxt.Response;
                response.ContentType = "application/json";

                var responsemodel = new Response<string>() { Message = error.Message, Success = false };

                var (code, message) = error switch
                {
                    ValidationException e => (HttpStatusCode.UnprocessableEntity, GetMessage(e)),
                    KeyNotFoundException e => (HttpStatusCode.NotFound, GetMessage(e)),
                    UnauthorizedAccessException e => (HttpStatusCode.Unauthorized, GetMessage(e)),
                    DbUpdateException e => (HttpStatusCode.Unauthorized, GetMessage(e)),
                    Exception e => (HttpStatusCode.BadRequest, GetMessage(e)),

                    _=>(HttpStatusCode.InternalServerError,GetMessage(error))
                };

                response.StatusCode = (int)code;
                responsemodel.Message = message;
                responsemodel.Status = code;
                var result = JsonSerializer.Serialize(responsemodel);
                await response.WriteAsync(result);


            }
        }




        private string GetMessage(Exception ex)
        {
            if (_environment.IsDevelopment())
            {
                return ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message : "") + ex.StackTrace;
            }
            return ex switch
            {
                ValidationException e => e.Message,
                Exception e => e.Message,
                _ => "Internal Server Error"
            };
        }
    }
}
