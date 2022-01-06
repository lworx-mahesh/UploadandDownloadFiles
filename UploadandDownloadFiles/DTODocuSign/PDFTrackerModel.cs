using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.DTODocuSign
{
    public class PDFTrackerModel
    {
        public DateTime? SignDateTime { get; set; }
        public string DocumentName { get; set; }
        public string NoOfSigner { get; set; }
        public Boolean? Issign { get; set; }
    }
}
