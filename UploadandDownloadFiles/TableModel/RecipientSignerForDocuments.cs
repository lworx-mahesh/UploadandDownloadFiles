using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.TableModel
{
    public class RecipientSignerForDocuments
    {
        [Key]
        public int Id { get; set; }

        public string SignerEmail { get; set; }
        public string Name { get; set; }

        public string Filepath { get; set; }

        public int? SignerOrder { get; set; }

        public DateTime? Signdate { get; set; }

        public string DocumentUniqueId { get; set; }

        public Boolean? IsSign { get; set; }
        public string Ipaddress { get; set; }

        public int? ParentsentId { get; set; }

        public DateTime? Createdate { get; set; }

        public Boolean? Isdeleted { get; set; }
        public Boolean? IsMailSend { get; set; }
        public DateTime? MailSendDate { get; set; }
        public string SignPdfFilepath { get; set; }
        public string SingerUniqueId { get; set; }

        public string CreatorUniqueId { get; set; }   // use for get list 
    }
}
