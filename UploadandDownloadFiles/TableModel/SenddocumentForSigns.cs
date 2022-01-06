using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.TableModel
{
    public class SenddocumentForSigns
    {
        [Key]
        public int Id { get; set; }

        public string DocumentName { get; set; }
        public string Filepath { get; set; }


        public DateTime? createddate { get; set; }


        public string EmailTo { get; set; }

        public string UniqueId { get; set; }
        public string Subject { get; set; }
        public bool? Issign { get; set; }

        public bool? Isdeleted { get; set; }

        public DateTime? Signdate { get; set; }

        public string SignpdfFilePath { get; set; }

        public int? OrderNumber { get; set; }

        public int? TotalSigner { get; set; }
        public string DocumentExpireDays { get; set; }
        public string CreatorUniqueId { get; set; }
    }

}
