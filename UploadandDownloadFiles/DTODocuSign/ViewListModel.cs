using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.DTODocuSign
{
    public class ViewListModel
    {
        public string SignerName { get; set; }
        public string DocumentName { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public DateTime? createddate { get; set; }
        public DateTime? Signdate { get; set; }
        public Boolean? Issign { get; set; }
        public string Ipaddress { get; set; }
           
    }
}
