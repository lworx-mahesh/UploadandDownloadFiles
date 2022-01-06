using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadandDownloadFiles.DTODocuSign;
using UploadandDownloadFiles.TableModel;

namespace UploadandDownloadFiles.IRepository
{
   public interface ISenddocumentForSign: IDisposable
    {


        IEnumerable<SenddocumentForSigns> GetAll();
        //ViewListModel ShowSignerList(string DocumentUniqueId);
        int? Insert(SenddocumentForSigns entity);
        List<PDFTrackerModel> DocumentStatus(string DocumentUniqueId);
        //       int? Update(string id, string downloadpdffilepath);
        //      int? UpdateChildIsSign(string id);
        //  int? UpdateSignerInfo(AccountLogin entity, string EmailId);
        //      int? saveIPObjval(string ChildSignerId, string RecipientIpaddress);
        //     bool? checkIssign(string id);
        //      bool? checkIssignOfChild(string id);
        //       int? saveSigner(SignerTrack entity);
        int? saveMultipleSigner(RecipientSignerForDocuments entity);
  //      int? storesignerinfo(StoreSignerInfo entity);
  //      int? SignAndResultsavedata(SignandReturn entity);
 //       int? saveReminderSigner(SignerReminder entity);
    //    int? SaveNewUserInfo(AccountLogin entity);
        int? SaveAxisInfo(SaveControlAxis entity, string Documentrecord);

    //    int? SaveBtnValueInfo(SavePdfBtnValue entity, string Documentrecord, string btnvalue);
 //       int? SaveUserPlanInfo(SubscriptionUser entity);

        int? SignerUpdate(string unqiueid);
        int? LastSignerUpdate(string Uniqueid, string FilePath);
    //    int? ReminderSignerUpdate(string unqiueid);
     //   int? UpdateChildSignPdfPath(string documentuniqueId, string DownloadPdfwithSign);

    }
}
