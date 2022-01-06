using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.DTODocuSign
{
   
        public class MailModel
        {
            public List<string> To { get; set; }
            public List<string> USERNAME { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string Days { get; set; }
            public List<int> Order { get; set; }
            public string? myTypeMailSend { get; set; }
            public string? mysignervalue { get; set; }

        public List<IFormFile> File { get; set; }
        //public string Param { get; set; }

        //public List<string> path { get; set; }

    }
    

}
