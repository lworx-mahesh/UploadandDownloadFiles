using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.TableModel
{
    public class SavePdfBtnValues
    {
        [Key]
        public int Id { get; set; }
        public string ButtonHeight { get; set; }
        public string ButtonOffsetLeft { get; set; }
        public string ButtonOffsetTop { get; set; }
        public string ButtonWidth { get; set; }
        public string controllerid { get; set; }
        public string DocumentUniqueId { get; set; }
        public string ControllerUniqueId { get; set; }
        public int ControlFieldId { get; set; }
        public string ColorCode { get; set; }
        public int UserOrderId { get; set; }
        public string ButtonValue { get; set; }
    }
}
