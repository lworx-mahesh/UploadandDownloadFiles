using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.DTODocuSign
{
    public class test
    {
       public List<buttonlabelval> butnfield { get; set; }
    }
    public class buttonlabelval
    {
        public string ButtonName { get; set; }
        public string ButtonLeft { get; set; }
        public string ButtonTop { get; set; }
        public string ButtonHeight { get; set; }
        public string ButtonWidth { get; set; }
        public string DocumentUniqueId { get; set; }
        public int OrderNo { get; set; }

    }
}
