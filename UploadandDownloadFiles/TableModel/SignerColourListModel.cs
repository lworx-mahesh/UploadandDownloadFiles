using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.TableModel
{
    public class SignerColourListModel
    {
        public SignerColourListModel()
        {
            SignerColorList = new List<SignerColourListModel>();
        }
        //public StoreSignerInfo StoreSignerInfolist { get; set; }
        //public MasterColorCode MasterColorCodelist { get; set; }
        public int? Id { get; set; }
        public string SignerEmail { get; set; }
        public string Name { get; set; }
        public string Filepath { get; set; }
        public int? SignerOrder { get; set; }
        public string DocumentUniqueId { get; set; }
        public string SignerUniqueID { get; set; }
        public string ColorCode { get; set; }

        public List<SignerColourListModel> SignerColorList { get; set; }


    }
}
