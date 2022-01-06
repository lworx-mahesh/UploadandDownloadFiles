using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using UploadandDownloadFiles.DTODocuSign;
using UploadandDownloadFiles.TableModel;

namespace UploadandDownloadFiles.Ilogics
{
  public  interface ISavedocumentsignparameters
    {
        void Savedocumentparameters();
        Documentdata getdata(string id);

       List<Documentdata> getdata(string id,string name);
        void SendDocumentWithAxis(DocumentWithAxisModel ObjModel);
       RecipientSignerForDocuments ShowDocumentAfterClickLink(string ChildUniqueId,string DocumentUniqueId);
        List<SaveControlAxis> ShowDocumentAxis(string DocumentUniqueId);
        void SaveBtnValueInfo(DocWithBtnValModel objmodel);
        List<ViewListModel> ShowSignerList(string DocumentUniqueId);
        List<ViewListModel> ShowAllSignerList();
      // List<PDFTrackerModel> DocumentStatus( string DocumentUniqueId);
    }
}
