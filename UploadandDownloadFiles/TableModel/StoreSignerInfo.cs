using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.TableModel
{
   [Table("StoreSignerInfoes")]
    public class StoreSignerInfo
    {
        public int Id { get; set; }

        public string SignerEmail { get; set; }
        public string Name { get; set; }

        public string Filepath { get; set; }

        public int? SignerOrder { get; set; }
        public string DocumentUniqueId { get; set; }
        public string SignerUniqueId { get; set; }
        public string Days { get; set; }
        public string SignerType { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
   //     public List<StoreSignerInfo> SignerList { get; set; }
    }
}
