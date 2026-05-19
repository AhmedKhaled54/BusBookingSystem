using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileServices
{
    public  interface IFileService
    {
        Task<string> Upload (IFormFile file,string folder);
        bool Remove(string filepath);
    }
}
