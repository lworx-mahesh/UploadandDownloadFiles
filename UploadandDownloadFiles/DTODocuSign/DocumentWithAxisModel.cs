using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.DTODocuSign
{
    public class DocumentWithAxisModel
    {
        //public List<string> To { get; set; }
        //public List<string> USERNAME { get; set; }
        //public List<int> Order { get; set; }

        //public List<IFormFile> File { get; set; }

        //public List<string> ButtonName { get; set; }
        //public List<string> ButtonLeft { get; set; }
        //public List<string> ButtonTop { get; set; }
        //public List<string> ButtonHeight { get; set; }
        //public List<string> ButtonWidth { get; set; }
        public List<buttonlabel> Buttonfields { get; set; }


    }
    public class buttonlabel
    {
        public string ButtonName { get; set; }
        public string ButtonLeft { get; set; }
        public string ButtonTop { get; set; }
        public string ButtonHeight { get; set; }
        public string ButtonWidth { get; set; }
        public int Order { get; set; }
        public string DocumentUniqueId { get; set; }

    }
}
