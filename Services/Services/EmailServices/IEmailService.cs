using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.EmailServices
{
    public  interface IEmailService
    {
        Task SendEmail (string Subject , string Body,string ToEmail  );

    }
}
