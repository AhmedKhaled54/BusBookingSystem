using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHost;
        private long MixSize = 109951163;
        private List<string>_allawExtension = new List<string>() { ".jpeg",".png",".jpg"};

        public FileService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
      

        public async Task<string> Upload(IFormFile file, string folder)
        {
            if (!_allawExtension.Contains(Path.GetExtension(file.FileName.ToLower())))
                    throw new Exception("Image Must Be Allawed (.png) / (.jpg) / (.jpeg)");
            if (file.Length>MixSize)
                throw new Exception("ax size is 2MB");

            var FileName=Guid.NewGuid().ToString() + file.FileName;
            var PathFile =Path.Combine(_webHost.WebRootPath,folder,FileName);
            using var stream = new FileStream(PathFile, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"{folder}/{FileName}";

        }

        public bool  Remove(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
                return false;
            
            var fullpath =Path.Combine(_webHost.WebRootPath,filepath);
            if (!File.Exists(fullpath))
                return false;
          
            File.Delete(fullpath);
            return true;
        }
    }
}
