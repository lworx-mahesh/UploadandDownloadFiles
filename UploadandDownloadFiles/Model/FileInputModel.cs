using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.Model
{
    public class FileInputModel
    {
        public List<IFormFile> File { get; set; }
        public string Param { get; set; }

        public List<string> path { get; set; }
    }
}
