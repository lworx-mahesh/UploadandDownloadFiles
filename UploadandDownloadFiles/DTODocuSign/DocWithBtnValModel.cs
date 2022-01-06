using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.DTODocuSign
{
    public class DocWithBtnValModel
    {
      public List<DocWithBtnVal> docwithbtnval { get; set; }
    }
    public class DocWithBtnVal
    {
        public string DocumentUniqueId { get; set; }
        public string ButtonName { get; set; }
        public string ButtonValue { get; set; }
      //  public string Signature_stamp { get; set; }
        public int Order { get; set; }
       
    }
}
