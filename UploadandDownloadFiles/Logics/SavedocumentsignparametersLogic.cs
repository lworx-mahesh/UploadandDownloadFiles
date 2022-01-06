//using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using UploadandDownloadFiles.DbcontextData;
using UploadandDownloadFiles.DTODocuSign;
using UploadandDownloadFiles.Ilogics;
using UploadandDownloadFiles.IRepository;
using UploadandDownloadFiles.TableModel;

namespace UploadandDownloadFiles.Logics
{
    public class SavedocumentsignparametersLogic : ISavedocumentsignparameters
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly docusigndbcontext _context;
        private readonly ISenddocumentForSign _SenddocumentForSignRepository;
        private readonly IRepository<StoreSignerInfo> _StoreSignerInfo;
        private readonly IRepository<SignerColourListModel> _SignerColourListModel;
        private readonly IRepository<SavePdfBtnValues> _SavePdfBtnValue;
        private readonly IRepository<SaveControlAxis> _SaveControlAxis;

        private readonly IRepository<SenddocumentForSigns> _SenddocumentForSigns;
        private readonly IRepository<RecipientSignerForDocuments> _RecipientSignerForDocument;
        public SavedocumentsignparametersLogic(
     IHostingEnvironment hostingEnvironment,
     IRepository<StoreSignerInfo> StoreSignerInfo, ISenddocumentForSign SenddocumentForSignRepository, IRepository<SenddocumentForSigns> SenddocumentForSign,
     IRepository<SignerColourListModel> SignerColourListModel, docusigndbcontext context, IRepository<SavePdfBtnValues> SavePdfBtnValues, IRepository<SaveControlAxis> SaveControlAxis, IRepository<RecipientSignerForDocuments> RecipientSignerForDocument)
        {
            _StoreSignerInfo = StoreSignerInfo;
            _SignerColourListModel = SignerColourListModel;
            _context = context;
            _SenddocumentForSigns = SenddocumentForSign;
            _hostingEnvironment = hostingEnvironment;
            _SavePdfBtnValue = SavePdfBtnValues;
            _SaveControlAxis = SaveControlAxis;
            _RecipientSignerForDocument = RecipientSignerForDocument;
            _SenddocumentForSignRepository = SenddocumentForSignRepository;
        }


        public Documentdata getdata(string id)
        {
            Documentdata obj = new Documentdata();
            obj.name = "dds";

            return obj;
        }

        public List<Documentdata> getdata(string id, string name)
        {
            List<Documentdata> hi = new List<Documentdata>();

            return hi;
        }

        public void Savedocumentparameters()
        {
            throw new NotImplementedException();
        }

        public void SaveBtnValueInfo(DocWithBtnValModel entity)
        {
         
                try
                {
                    if (entity == null)
                    {
                        throw new ArgumentNullException("entity is null");
                    }
                    else
                    {
                   // var btnorder = entity.docwithbtnval.Count;
                    var ListOfBtn = _SaveControlAxis.Where(x => x.DocumentUniqueId == entity.docwithbtnval[0].DocumentUniqueId).ToList();
                    if (ListOfBtn.Count != 0)
                    {
                        List<SaveControlAxis> dataList; 
                        Guid gid = Guid.NewGuid();
                        string guid = gid.ToString();
                        //   var count = entity.ButtonValue.Count;
                        var count = entity.docwithbtnval.Count;
                        for (int i = 0; i < count; i++)
                        {
                            dataList = ListOfBtn.Where(z => z.UserOrderId == entity.docwithbtnval[i].Order).ToList();
                           
                            for (int j=i;j== i;j++)
                            {


                                var SavePdfBtnValues = new SavePdfBtnValues()
                                {

                                    ButtonHeight = dataList[j].ButtonHeight,
                                    ButtonOffsetLeft = dataList[j].ButtonOffsetLeft,
                                    ButtonOffsetTop =  dataList[j].ButtonOffsetTop,
                                    ButtonWidth = dataList[j].ButtonWidth,                                
                                    DocumentUniqueId = entity.docwithbtnval[0].DocumentUniqueId,                                
                                       UserOrderId = entity.docwithbtnval[i].Order,
                                    controllerid = entity.docwithbtnval[i].ButtonName,
                                    ButtonValue = entity.docwithbtnval[i].ButtonValue

                                    //ButtonHeight = entity.ButtonHeight[i],
                                    //        ButtonOffsetLeft = entity.ButtonLeft[i],
                                    //        ButtonOffsetTop = entity.ButtonTop[i],
                                    //        ButtonWidth = entity.ButtonWidth[i],
                                    //        //
                                    //        DocumentUniqueId = entity.DocumentUniqueId,
                                    //        //    ControllerUniqueId = guid,
                                    //        //    ControlFieldId = entity.,
                                    //        //   UserOrderId = entity.UserOrderId,
                                    //        ButtonValue = entity.ButtonValue[i]


                                };
                                //            _context.SavePdfBtnValue.Add();
                                _SavePdfBtnValue.Insert(SavePdfBtnValues);
                            }

                        }
                    }



                        var returnid = _context.SaveChanges();

              
                    }
                    //}
                    //else
                    //{
                    //    return 0;
                    //}
                    // _context.Entry(entity).State = EntityState.Added;
                    // return _context.SaveChanges();
                }


                catch (Exception ex)
                {
                    //  ExceptionLogging.SendErrorToText(ex);
                    throw ex;
                    
                }
            }
 


        public void SendDocumentWithAxis(DocumentWithAxisModel ObjModel)
        {

            try
            {

                  string path = "\\Files";
                var pathroot = path.Replace("\\files", "/files/");
                var path1 = _hostingEnvironment.ContentRootPath + pathroot;    // use for get files folder name

                
                var Returnid = "";
                //  Get all files from Request object C:\Users\Spadez\Downloads\UploadandDownloadFiles\UploadandDownloadFiles\UploadandDownloadFiles
                //HttpFileCollectionBase files = Request.Files;Buttonfields
                //   var Pdfid = ObjModel.DocumentUniqueId;
                var Pdfid = ObjModel.Buttonfields[0].DocumentUniqueId;
                Saveaxis(ObjModel, Pdfid);   // save the  button axis..
                var objstoreinfo = _StoreSignerInfo.Where(x => x.DocumentUniqueId == Pdfid).FirstOrDefault();
                var pathfile = objstoreinfo.Filepath;
                var owners = System.IO.File.ReadAllLines(System.IO.Path.Combine(path1,pathfile));
               

                var subDirectory = "DownloadDocument";
                var target = Path.Combine(path1, subDirectory);


                Directory.CreateDirectory(target);
                //ObjModel.File.ForEach(async file =>
                //{
                //    if (file.Length <= 0) return;
                //    var filePath = Path.Combine(target, file.FileName);
                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await file.CopyToAsync(stream);
                //    }
                //});

                var Fetchrecordlist = _StoreSignerInfo.Wherecondtion(d => d.DocumentUniqueId == Pdfid).ToList();

                List<DocumentWithAxisModel> Listmodel = new List<DocumentWithAxisModel>();
                List<StoreSignerInfo> Listmodelsigners = new List<StoreSignerInfo>();
                /// use for get record  for signer when creator want to send data to signers
       //         var SignAndReturnList = _SavePdfBtnValue.Wherecondtion(d => d.DocumentUniqueId == Pdfid).ToList();
                
                //if (SignAndReturnList != null && SignAndReturnList.Count != 0)
                //{
                //    for (int y = 0; y < SignAndReturnList.Count; y++)
                //    {
                //        DocumentWithAxisModel objModelMail = new DocumentWithAxisModel();
                //        var getextenstion = SignAndReturnList[y].controllerid;
                //        var resultid = getextenstion.Split('_');
                //        var resultcontrolerid = resultid[0];
                //        //if (resultcontrolerid == "USERNAME")
                //        //{
                //        //    objModelMail.USERNAME =  SignAndReturnList[y].ButtonValue;
                //        //}
                //        //if (resultcontrolerid == "To")
                //        //{
                //        //    objModelMail.To = new string[] { SignAndReturnList[y].ButtonValue };
                //        //}

                //        //if (resultcontrolerid == "Subject")
                //        //{
                //        //    objModelMail.Subject = SignAndReturnList[y].ButtonValue;
                //        //}
                //        //if (resultcontrolerid == "Message")  // use body to store the Message value in object model
                //        //{
                //        //    objModelMail.Body = SignAndReturnList[y].ButtonValue;
                //        //}
                //        Listmodel.Add(objModelMail);
                //    }
                //}
                //else
                //{
                DocumentWithAxisModel objModelMail = new DocumentWithAxisModel();
       
                for (int y = 0; y < Fetchrecordlist.Count; y++)
                {
                    StoreSignerInfo obj = new StoreSignerInfo();
                    obj.SignerEmail = Fetchrecordlist[y].SignerEmail;
                    obj.Name = Fetchrecordlist[y].Name;
                    obj.SignerOrder = Convert.ToInt32(Fetchrecordlist[y].SignerOrder);
                    Listmodelsigners.Add(obj);
                }
                //      SendContractpdfToMail(Listmodelsigners, ObjModel.File, Pdfid);  // uncomment
                SendContractpdfToMail(Listmodelsigners, Pdfid);
            }
            catch (Exception ex)
            {
                throw ex;
            
            }

        }


        public int? Saveaxis(DocumentWithAxisModel ObjModel, string Pdfid)
        {
            try
            {
                if (ObjModel == null || Pdfid == null)
                {
                    throw new ArgumentNullException("entity is null");
                }
                else
                {
                    //      var count = ObjModel.ButtonName.Count;
                    var count = ObjModel.Buttonfields.Count();
                    for (int h = 0; h < count; h++)
                    {
                        Guid gid = Guid.NewGuid();
                        string guid = gid.ToString();
                        var SaveControlAxis = new SaveControlAxis()
                        {
                            ButtonHeight = ObjModel.Buttonfields[h].ButtonHeight,
                            ButtonOffsetLeft = ObjModel.Buttonfields[h].ButtonLeft,
                            ButtonOffsetTop = ObjModel.Buttonfields[h].ButtonTop,
                            ButtonWidth = ObjModel.Buttonfields[h].ButtonWidth,
                            controllerid = ObjModel.Buttonfields[h].ButtonName,
                            DocumentUniqueId = Pdfid,
                            ControllerUniqueId = guid,
                            UserOrderId= ObjModel.Buttonfields[h].Order


                            //ButtonHeight = ObjModel.ButtonHeight[h],
                            //ButtonOffsetLeft = ObjModel.ButtonLeft[h],
                            //ButtonOffsetTop = ObjModel.ButtonTop[h],
                            //ButtonWidth = ObjModel.ButtonWidth[h],
                            //controllerid = ObjModel.ButtonName[h],
                            //DocumentUniqueId = Pdfid,
                            //ControllerUniqueId = guid

                            //ControlFieldId = entity.ControlFieldId,
                            //UserOrderId = entity.UserOrderId,   // allocate to button according signers
                            //ColorCode = entity.ColorCode

                        };
                        _context.SaveControlAxis.Add(SaveControlAxis);

                    }
                    return _context.SaveChanges();
                    //   return 1;

                }
                //}
                //else
                //{
                //    return 0;
                //}
                // _context.Entry(entity).State = EntityState.Added;
                // return _context.SaveChanges();
            }


            catch (Exception ex)
            {
                //  ExceptionLogging.SendErrorToText(ex);
                throw ex;
                //   return null;
            }
        }




        //public void SendContractpdfToMail(List<StoreSignerInfo> objModelMail, List<IFormFile> files, string uniquiepdfId)
        //{

        public void SendContractpdfToMail(List<StoreSignerInfo> objModelMail, string uniquiepdfId)
        {

            try
            {
                string path = "\\Files";
                var pathroot = path.Replace("\\files", "/files/");
                var path1 = _hostingEnvironment.ContentRootPath + pathroot;
                var Returnid = "";
           
                var subDirectory = "DownloadDocument";
                var target = Path.Combine(path1, subDirectory);
                Directory.CreateDirectory(target);
                //files.ForEach(async file =>
                //{
                //    if (file.Length <= 0) return;
                //    var filePath = Path.Combine(target, file.FileName);
                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await file.CopyToAsync(stream);
                //    }
                //});

                string from = "testmail6324@gmail.com"; //example:- sourabh9303@gmail.com
                string Tomail = "";
                string[] arrmail;
                List<string> listdata = new List<string>();
                List<string> listUsername = new List<string>();
                foreach (var a in objModelMail)
                {
                    try
                    {
                        var b = a.SignerEmail;
                        if (b != null)
                        {
                            var t = a.SignerEmail;
                            listdata.Add(t);
                        }
                        var name = a.Name;
                        if (name != null)
                        {
                            var t = a.Name;
                            listUsername.Add(t);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;


                    }
                }
                var countuser = objModelMail.Count;
                string SignerTypeMail = "";
                var SignerTypeRecords = _StoreSignerInfo.Wherecondtion(d => d.DocumentUniqueId == uniquiepdfId).FirstOrDefault();
                if (SignerTypeRecords != null)
                {
                    SignerTypeMail = SignerTypeRecords.SignerType;
                }

                if (countuser > 1 && SignerTypeMail == "Send Mail One By One")
                {
                    for (int s = 0; s < objModelMail.Count; s++)
                    {
                        //     var order = objModelMail[s].Order[s];     //use only for single user
                        Tomail = objModelMail[s].SignerEmail;
                        break;
                    }
                    using (MailMessage mail = new MailMessage(from, Tomail))
                    {
                        //if (files != null)
                        //{
                        //    //string _fileName = Path.GetFileName(files.FileName);
                        //    //string fileName = _fileName + ".pdf";
                        //    //mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                        //}
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.UseDefaultCredentials = false;
                        NetworkCredential networkCredential = new NetworkCredential(from, "Test@123");
                        smtp.EnableSsl = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        //saving file details into database
                        //    var unqiueid = savesendmail(objModelMail, fileUploader, shortPath, uniquiepdfId);
                     //   var unqiueid = savesendmail(objModelMail, files, path1, uniquiepdfId);
                        var unqiueid = savesendmail(objModelMail, path1, uniquiepdfId);
                        var Childdatafile = from documentdata in _RecipientSignerForDocument.Wherecondtion(s => s.DocumentUniqueId == uniquiepdfId)
                                            select documentdata;
                        string ChildSignerId = Childdatafile.ToList().FirstOrDefault().SingerUniqueId;

                        var varifyUrl = "https://localhost:44361/Home/Index/" + uniquiepdfId + "/" + ChildSignerId; //local

                        //var UrlPath = ConfigurationManager.AppSettings["UrlForSigner"];
                        //var varifyUrl = UrlPath + "/" + uniquiepdfId + "/" + ChildSignerId; //local

                        //      var varifyUrl = "http://docusign.spadez.co/Home/Index/" + uniquiepdfId + "/" + ChildSignerId; //local

                        //  string path = System.Web.HttpContext.Current.Server.MapPath("~/Views/EmailTemplate/EmailwithLogo.cshtml");
                        //StreamReader mailstr = new StreamReader(path);
                        //string MailMessage = mailstr.ReadToEnd();
                        //mailstr.Close();
                        string link = "<br/> Please click on the below link to Sign Your Contract<br/><a href='" + varifyUrl + "'>" + "Click On This" + "</a> ";
                        // MailMessage = MailMessage.Replace("{UserEmailLink}", varifyUrl);
                        ////  MailMessage = MailMessage.Replace("{SignerMail}", from);
                        // MailMessage = MailMessage.Replace("[SignerMailUser]", from);
                        //     string content = System.IO.File.ReadAllText(path);
                        //         LinkedResource imageResource = new LinkedResource(strPath, MediaTypeNames.Image.Jpeg);
                        //       imageResource.ContentId = "DocuSignID";
                        string body = " <br/><br/> Please click on the below link to Sign Your Contract<br/><br/>" +
                                         "<br/><br/><a href='" + varifyUrl + "'>" + "Click On This" + "</a> ";
                        mail.Body = "";
                        //   mail.Body = mail.Body + link;
                        //     mail.Body = mail.Body + MailMessage;
                        mail.Body = mail.Body;
                        //       AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
                        //            htmlView.LinkedResources.Add(imageResource);
                        //    mail.AlternateViews.Add(htmlView);
                        mail.Body = body;
                        var EmailTO = "";
                        for (int j = 0; j <= objModelMail.Count; j++)
                        {
                            EmailTO = objModelMail[j].SignerEmail;
                            if (EmailTO != "" || EmailTO != null)
                            {
                                try
                                {
                                    smtp.Send(mail);
                                }
                                catch (SmtpException ex)
                                {
                                    //    ExceptionLogging.SendErrorToText(ex);
                                    throw ex;
                                    //    return null;
                                }
                                Returnid = SignerStatusUpdate(ChildSignerId);
                                break;
                            }
                            else
                            {
                                var showerror = "enter email id";
                            }
                        }
                        //   ViewBag.Message = "Sent"; Send Mail One By One
                    }
                }

                //       if (countuser > 1 && SignerTypeMail == "Send Mail To All")
                if (countuser > 1 && SignerTypeMail != "Send Mail One By One")
                {
                    try
                    {
          //              SendAllMail(objModelMail, files, path1, uniquiepdfId, listdata);
                        SendAllMail(objModelMail, path1, uniquiepdfId, listdata);
                    }
                    catch (Exception ex)
                    {
                        // ExceptionLogging.SendErrorToText(ex);
                        throw ex;
                        //    return null;
                    }
                }

                //          if (countuser > 1 && SignerTypeMail == "I'm the only signer")      // this condition use for "I'm the only signer"
                //          {
                ////              SignandReturn objdata = new SignandReturn();
                //              for (int s = 0; s < objModelMail.Count; s++)
                //              {
                //                  //     var order = objModelMail[s].Order[s];     //use only for single user
                //                  var checkemailid = objModelMail[s].SignerEmail;
                //                  if (checkemailid != null)
                //                  {
                //                      Tomail = objModelMail[s].SignerEmail;
                //                      break;
                //                  }

                //              }
                //              if (Tomail == "")
                //              {
                //             //     ViewBag.Message = "EmailId"; // use for single email id
                //               //   return View();
                //              }
                //              using (MailMessage mail = new MailMessage(from, Tomail))
                //              {
                //                  //if (fileUploader != null)
                //                  //{
                //                  //    string FileNamecheck = DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_" + Path.GetFileName(fileUploader.FileName);
                //                  //    string _FileNames = FileNamecheck + ".pdf";
                //                  //    mail.Attachments.Add(new Attachment(fileUploader.InputStream, _FileNames));
                //                  //}
                //                  mail.IsBodyHtml = true;
                //                  SmtpClient smtp = new SmtpClient();
                //                  smtp.Host = "smtp.gmail.com";
                //                  smtp.UseDefaultCredentials = false;
                //                  NetworkCredential networkCredential = new NetworkCredential(from, "Test@123");
                //                  smtp.EnableSsl = true;
                //                  smtp.Credentials = networkCredential;
                //                  smtp.Port = 587;
                //                  smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //             //     string path = System.Web.HttpContext.Current.Server.MapPath("~/Views/EmailTemplate/CreatorSendMailToSigners.cshtml");
                //                  //StreamReader mailstr = new StreamReader(path);
                //                  //string MailMessage = mailstr.ReadToEnd();
                //                  //mailstr.Close();
                //                  //string content = System.IO.File.ReadAllText(path);
                //                  //MailMessage = MailMessage.Replace("[SignerMailUser]", from);
                //                  //LinkedResource imageResource = new LinkedResource(strPath, MediaTypeNames.Image.Jpeg);
                //                  //imageResource.ContentId = "DocuSignID";
                //                  mail.Body = "";
                //              // string body = "/n <br/><br/>check your document with creator fields<br/><br/>";
                //              //  mail.Body = mail.Body+body;
                //              //  mail.Body = mail.Body + content;
                //              //   mail.Body = mail.Body + MailMessage;
                //              mail.Body = mail.Body;
                //              //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
                //              //    htmlView.LinkedResources.Add(imageResource);
                //              //    mail.AlternateViews.Add(htmlView);

                //                  //    mail.Body = mail.Body + body;
                //                  var EmailTO = "";
                //                  for (int j = 0; j <= listdata.Count; j++)
                //                  {
                //                      mail.To.Clear();
                //                      mail.To.Add(new MailAddress(listdata[j]));
                //                      try
                //                      {
                //                          Guid g = Guid.NewGuid();
                //                          string Userguid = g.ToString();
                //                          smtp.Send(mail);
                //                          objdata.SignerEmail = listdata[j];
                //                          objdata.Name = listUsername[j];
                //                          objdata.Filepath = shortPath;
                //                          objdata.DocumentUniqueId = uniquiepdfId;
                //                          objdata.SignerUniqueId = Userguid;
                //                          var Returnid = _SenddocumentForSignRepository.SignAndResultsavedata(objdata);
                //                      }
                //                      catch (SmtpException ex)
                //                      {
                //                          ExceptionLogging.SendErrorToText(ex);
                //                          return null;
                //                      }
                //                  }
                //                  ViewBag.Message = "Sent";
                //              }
                //          }

                //}
                if (countuser == 1)      // this condition use for "I'm the only signer"
                {
                    for (int s = 0; s < objModelMail.Count; s++)
                    {
                        //     var order = objModelMail[s].Order[s];     //use only for single user
                        //   Tomail = objModelMail[s].To[s];
                        Tomail = objModelMail[s].SignerEmail;
                        break;
                    }
                    if (Tomail == "")
                    {
                        //ViewBag.Message = "EmailId"; // use for single email id
                        //return View();
                    }
                    using (MailMessage mail = new MailMessage(from, Tomail))
                    {
                        //if (fileUploader != null)
                        //{
                        //    string FileNamecheck = DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_" + Path.GetFileName(fileUploader.FileName);
                        //    string _FileNames = FileNamecheck + ".pdf";
                        //    mail.Attachments.Add(new Attachment(fileUploader.InputStream, _FileNames));
                        //}
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.UseDefaultCredentials = false;
                        NetworkCredential networkCredential = new NetworkCredential(from, "Test@123");
                        smtp.EnableSsl = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //saving file details into database
                        //    var unqiueid = savesendmail(objModelMail, fileUploader, shortPath, uniquiepdfId);
                        var unqiueid = savesendmail(objModelMail,  path1, uniquiepdfId);
                        var Childdatafile = from documentdata in _RecipientSignerForDocument.Wherecondtion(s => s.DocumentUniqueId == uniquiepdfId)
                                            select documentdata;
                        string ChildSignerId = Childdatafile.ToList().FirstOrDefault().SingerUniqueId;

                        //  var UrlPath = ConfigurationManager.AppSettings["UrlForSigner"];
                        //  var varifyUrl = UrlPath + "/" + uniquiepdfId + "/" + ChildSignerId;

                        var varifyUrl = " https://localhost:44361/Home/Index/" + uniquiepdfId + "/" + ChildSignerId; //local
                                                                                                                     // var varifyUrl = "http://docusign.spadez.co/Home/Index/" + uniquiepdfId + "/" + ChildSignerId; //local
                        string body = " <br/><br/> Please click on the below link to Sign Your Contract<br/><br/>" +
                                         "<br/><br/><a href='" + varifyUrl + "'>" + "Click On This" + "</a> ";
                        mail.Body = @"This is an email with image included <br />
                                       <img src=""D:\DigitalSignJainiview\digisign\PDFJSWebApplication\images\Logo\DocuSignLogo.jpg"" alt=""Logo"" /><br />
                                       But, many mail clients will block external images by default <br/><a href='" + varifyUrl + "'>" + "Click On This" + "</a> ";
                        var EmailTO = "";
                        for (int j = 0; j <= objModelMail.Count; j++)
                        {
                            var emailcheck = objModelMail[j].SignerEmail;
                            if (emailcheck != null)
                            {
                                EmailTO = objModelMail[j].SignerEmail;
                                try
                                {
                                    smtp.Send(mail);
                                }
                                catch (SmtpException ex)
                                {
                                    throw ex;
                                    //ExceptionLogging.SendErrorToText(ex);
                                    //return null;
                                }
                                break;
                            }
                            else
                            {
                                var showerror = "enter email id";
                            }
                        }

                        //   ViewBag.Message = "Sent";
                    }
                }
                //   return View();
            }
            catch (Exception ex)
            {
                throw ex;
                //ExceptionLogging.SendErrorToText(ex);

                //return null;
            }

        }

        //public string savesendmail(List<StoreSignerInfo> objModelMail, List<IFormFile> files,string path1, string uniquiepdfId)
        // {
        public string savesendmail(List<StoreSignerInfo> objModelMail, string path1, string uniquiepdfId)
        {
            try
            {
             //   var CreatorEmailRecord = _AccountLogin.Wherecondtion(x => x.Email == Creatoremail).FirstOrDefault();
                // string creatoruniqueid = CreatorEmailRecord.UserId;
                Guid g = Guid.NewGuid();
                var StoresignerFilePath="";
                string Userguid = g.ToString();
                var SaveAxisuserid = _SaveControlAxis.Wherecondtion(i => i.DocumentUniqueId == uniquiepdfId).ToList();  // get 1st ControllerUniqueId id from saveaxiscontrole
                var Storesignerfilepath = _StoreSignerInfo.Wherecondtion(i => i.DocumentUniqueId == uniquiepdfId).FirstOrDefault();  // get 1st ControllerUniqueId id from saveaxiscontrole
                if(Storesignerfilepath!=null)
                {
                    StoresignerFilePath=Storesignerfilepath.Filepath;
                }
                int? Returnid = 0;

                int ordernumber = 0;
                if (objModelMail != null && objModelMail.Count > 1)
                {
                    for (int a = 0; a < objModelMail.Count; a++)
                    {
                        if (a == objModelMail.Count - 1)
                        {
                            /*var autoincrementid = InsertSendDocumentforsignMaster(objModelMail, files, uniquiepdfId, path1);*/  // store last record in master table
                            var autoincrementid = InsertSendDocumentforsignMaster(objModelMail, uniquiepdfId, path1);
                        }
                        ordernumber = ordernumber + 1;

                        RecipientSignerForDocuments obj = new RecipientSignerForDocuments();
                        obj.SignerEmail = objModelMail[a].SignerEmail;
                        obj.Name = objModelMail[a].Name;
                        //obj.Filepath = "/" + StoresignerFilePath;
                        obj.Filepath = StoresignerFilePath;
                        obj.SignerOrder = ordernumber;
                        obj.Signdate = null;
                        obj.DocumentUniqueId = uniquiepdfId;
                        obj.IsSign = false;
                        //obj.ParentsentId = autoincrementid;
                        obj.SingerUniqueId = SaveAxisuserid[a].ControllerUniqueId;
                       // obj.CreatorUniqueId = creatoruniqueid;
                        Returnid = _SenddocumentForSignRepository.saveMultipleSigner(obj);
                    }
                    if (Returnid > 0)
                    {
                        return uniquiepdfId.ToString();
                    }
                }

                else
                {
                    for (int a = 0; a < objModelMail.Count; a++)
                    {
                        ordernumber = ordernumber + 1;
                        RecipientSignerForDocuments obj = new RecipientSignerForDocuments();
                        obj.SignerEmail = objModelMail[a].SignerEmail;
                        obj.Name = objModelMail[a].Name;
                        //obj.Filepath = "/" + path1;
                        obj.Filepath = StoresignerFilePath;
                        obj.SignerOrder = ordernumber;
                        obj.Signdate = null;
                        obj.DocumentUniqueId = uniquiepdfId;
                        obj.IsSign = false;
                        //obj.ParentsentId = autoincrementid;
                        obj.SingerUniqueId = SaveAxisuserid[0].ControllerUniqueId;
              //          obj.CreatorUniqueId = creatoruniqueid;
                        Returnid = _SenddocumentForSignRepository.saveMultipleSigner(obj);
                        if (Returnid > 0)
                        {
                        //    return guid.ToString();  //  use for future
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return "check";
        }

        //public int? InsertSendDocumentforsignMaster(List<StoreSignerInfo> objModelMail, List<IFormFile> files, string guid, string shortPath)
        //{
        public int? InsertSendDocumentforsignMaster(List<StoreSignerInfo> objModelMail, string guid, string shortPath)
        {
            try
            {
                SenddocumentForSigns obj = new SenddocumentForSigns();
                //string Creatoremail = Session["Email"].ToString();
                var signerinfopath="";
                var result = "";
                var datasignerifo = _StoreSignerInfo.Where(x => x.DocumentUniqueId == guid).FirstOrDefault();
                // var CreatorEmailRecord = _AccountLogin.Wherecondtion(x => x.Email == Creatoremail).FirstOrDefault();
                //   string creatoruniqueid = CreatorEmailRecord.UserId;
              
                if (datasignerifo!=null)
                {
                    signerinfopath= datasignerifo.Filepath;
                    result = Path.GetFileName(signerinfopath);
                }
                if (objModelMail != null && objModelMail.Count > 1)
                {


                    for (int a = 0; a < objModelMail.Count; a++)
                    {
                        if (a == objModelMail.Count - 1)
                        {
                            obj.EmailTo = objModelMail[a].SignerEmail;
                            //obj. = objModelMail[a].USERNAME[0];
                            obj.Filepath = "/" + shortPath;
                            //int i = objModelMail.To.Length - 1;
                            int j = objModelMail.Count;
                            //      obj.DocumentName = files[0].FileName; shortPath
                            //obj.DocumentName = shortPath; //brack file name
                            obj.DocumentName = result;
                            obj.Filepath = signerinfopath;
                            obj.createddate = DateTime.Now;
                            obj.UniqueId = guid.ToString();
                            obj.Subject = objModelMail[a].Subject;
                            obj.Issign = false;
                            obj.Signdate = null;
                            obj.OrderNumber = j;
                            obj.TotalSigner = j;
                            obj.DocumentExpireDays = datasignerifo.Days;
                            obj.Subject = datasignerifo.Subject;
                           // obj.CreatorUniqueId = creatoruniqueid;
                        }
                    }

                }
                if (obj != null)
                {
                    var Returnid = _SenddocumentForSignRepository.Insert(obj);
                    return Returnid;
                }
                return 1;
            }
            catch (Exception ex)
            {
                //     ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return 0;
            }

        }
        public List<PDFTrackerModel> DocumentStatus(string DocumentUniqueId)
        {
            try
            {
                if (DocumentUniqueId != "" || DocumentUniqueId != null)
                {
                    PDFTrackerModel model = new PDFTrackerModel();
                    List<PDFTrackerModel> lstobj = new List<PDFTrackerModel>();
                    var dataAxisList = _SenddocumentForSigns.Where(x => x.UniqueId == DocumentUniqueId).FirstOrDefault();
                    // var count = dataAxisList.Count();

                    //for (int i = 0; i < count; i++)
                    //{
                    //    {
                    model.SignDateTime = dataAxisList.Signdate;
                            model.DocumentName = dataAxisList.DocumentName;
                            model.NoOfSigner = dataAxisList.OrderNumber.ToString();
                            model.Issign = dataAxisList.Issign;
                        //    model.EmailTo = dataAxisList[i].SignerEmail;

                       // };
                        lstobj.Add(model);

                 //   }
                    return lstobj;
                }

                else
                {
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     //   public int Saveaxis(DocumentWithAxisModel ObjModel)
        //{
        //    if (ObjModel != null)
        //    {
        //        string[] reqObj;
        //        string documentuniqueid = null;
        //        SaveControlAxis obj = new SaveControlAxis();
        //        var count = ObjModel.ButtonName.Count;
        //    //    JsonResult result = new JsonResult();
        //    //      dynamic myJsonObjects = JsonConvert.DeserializeObject<dynamic>(model);
        //        for (int i = 0; i < count; i++)
        //        {
        //            obj.ButtonHeight = ObjModel.ButtonHeight[i];
        //            obj.ButtonOffsetLeft = ObjModel.ButtonLeft[i];
        //            obj.ButtonOffsetTop = ObjModel.ButtonTop[i];
        //            obj.ButtonWidth = ObjModel.ButtonWidth[i];
        //            obj.controllerid = ObjModel.ButtonName[i]; 
        //            documentuniqueid = ObjModel.DocumentUniqueId;
        //            //obj.ControlFieldId = myJsonObjects[i].ControlFieldId;
        //            //obj.UserOrderId = myJsonObjects[i].UserOrderId;
        //            //obj.ColorCode = myJsonObjects[i].ColorCode;
        //            var returnid = _SenddocumentForSignRepository.SaveAxisInfo(obj, documentuniqueid);
        //        }
        //    }
        //    return 0;
        // //   return Json(new { request = "success" }, JsonRequestBehavior.AllowGet);
        //}

        public string SignerStatusUpdate(string ChildSignerId)
        {

            try
            {
                string documentuniqueId = ChildSignerId;
                var Returnid = _SenddocumentForSignRepository.SignerUpdate(documentuniqueId);
                if (Returnid > 0)
                {
                    return Returnid.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                //   ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }

        }


        public int? SignerUpdate(string unqiueid)
        {
            try
            {
                if (unqiueid == null)
                {
                    throw new ArgumentNullException("ID is null");
                }
                else
                {

                    var Data = _context.RecipientSignerForDocuments.Where(x => x.SingerUniqueId == unqiueid && x.IsSign == false).FirstOrDefault();
                    if (Data != null)
                    {
                        Data.IsMailSend = true;
                        Data.MailSendDate = DateTime.Now;
                    }
                    //  _context.SenddocumentForSign.Add(Data);
                    return _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                //  ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
        }



        //public string SendAllMail(List<StoreSignerInfo> objModelMail, List<IFormFile> files, string shortPath, string guid, List<string> EmailList)
        //{
        public string SendAllMail(List<StoreSignerInfo> objModelMail, string shortPath, string guid, List<string> EmailList)
        {
            try
            {
                string path = "/images/Logo/";
                var pathroot = path.Replace("/images/Logo/", "/images/Logo/");
                var strPath = _hostingEnvironment.ContentRootPath + pathroot+ "DigiSign.png";          
                MailMessage mail = new MailMessage();
                string from = "testmail6324@gmail.com"; //example:- sourabh9303@gmail.com    replace  here from creator email
                string Tomail = "";        
                 mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                var signerinfopath = _StoreSignerInfo.Where(x => x.DocumentUniqueId == guid).FirstOrDefault();

              var  result = Path.GetFileName(signerinfopath.Filepath);

                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(result);
                //mail.Attachments.Add(attachment);
                string TemplateEmailWithLogo = _hostingEnvironment.ContentRootPath + "/View/EmailTemplate/EmailwithLogo.cshtml" + result;

                System.Net.Mail.Attachment attachment;
                string pathurlpdf = _hostingEnvironment.ContentRootPath + "/Files/DownloadDocument/" + result;
                attachment = new System.Net.Mail.Attachment(pathurlpdf);
                mail.Attachments.Add(attachment);
                mail.Body = TemplateEmailWithLogo;


                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                NetworkCredential networkCredential = new NetworkCredential(from, "Test@123");
                mail.From = new MailAddress(from, "Test@123");
                smtp.EnableSsl = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                var documentuniqueid = guid;
          //      var unqiueid = savesendmail(objModelMail, files, shortPath, documentuniqueid);
                var unqiueid = savesendmail(objModelMail, shortPath, documentuniqueid);
                var Childdatafile = from documentdata in _RecipientSignerForDocument.Wherecondtion(s => s.DocumentUniqueId == unqiueid)
                                    select documentdata;
                var ChildSignerIds = Childdatafile.ToList();
                for (int i = 0; i < ChildSignerIds.Count; i++)
                {
                    if (EmailList[i] == ChildSignerIds[i].SignerEmail)
                    {
                        var ChildSignerId = "";
                        ChildSignerId = ChildSignerIds[i].SingerUniqueId;
                        //  string imagepath = @"D:\DigitalSignJainiview\digisign\PDFJSWebApplication\images\Logo\DocuSignLogoWhite.png";
                        string directoryName;
                        int u = 0;
                          var varifyUrl = "https://localhost:44378/api/File/Index/" + documentuniqueid + "/" + ChildSignerId; //local
                                                                                                                              //     var  varifyUrl = "http://docusign.spadez.co/Home/Index/" + documentuniqueid + "/" + ChildSignerId; //local

                        string pathurl = _hostingEnvironment.ContentRootPath + "/View/EmailTemplate/EmailwithLogo.cshtml";
                        //string pathurl = System.Web.HttpContext.Current.Server.MapPath("~/Views/EmailTemplate/EmailwithLogo.cshtml");
                        StreamReader mailstr = new StreamReader(pathurl);
                        string MailMessage = mailstr.ReadToEnd();
                        mailstr.Close();
                        //string link = "<br/> Please click on the below link o Sign Your Contract<br/><a href='" + varifyUrl + "'>" + "Click On This" + "</a> ";
                        MailMessage = MailMessage.Replace("{UserEmailLink}", varifyUrl);
                        MailMessage = MailMessage.Replace("{SignerMail}", from);
                        MailMessage = MailMessage.Replace("[SignerMailUser]", from);
                        string content = System.IO.File.ReadAllText(pathurl);
                        LinkedResource imageResource = new LinkedResource(strPath, MediaTypeNames.Image.Jpeg);
                        imageResource.ContentId = "DocuSignID";
                           mail.Body = "";
                        //   mail.Body = mail.Body + link;
                        mail.Body = mail.Body + MailMessage;
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
                        htmlView.LinkedResources.Add(imageResource);
                        mail.AlternateViews.Add(htmlView);               
                        mail.To.Clear();
                        mail.To.Add(new MailAddress(EmailList[i]));
                        try
                        {
                            smtp.Send(mail);

                        }
                        catch (SmtpException ex)
                        {
                            //    ExceptionLogging.SendErrorToText(ex);
                            throw ex;
                            return null;
                        }
                        var Returnid = SignerStatusUpdate(ChildSignerId);
                    }
                }

            }
            catch (Exception ex)
            {
                // ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
            return null;
        }


      public RecipientSignerForDocuments  ShowDocumentAfterClickLink(string ChildUniqueId, string DocumentUniqueId)
        {

            var pathroot = _hostingEnvironment.ContentRootPath;
            RecipientSignerForDocuments model = new RecipientSignerForDocuments();
            if ((ChildUniqueId!="" && DocumentUniqueId!="") || (ChildUniqueId != null && DocumentUniqueId != null))
            {
                model = _RecipientSignerForDocument.Wherecondtion(x => x.DocumentUniqueId == DocumentUniqueId && x.SingerUniqueId == ChildUniqueId).FirstOrDefault();

                return model;
            }
            else
            {
                return null;
            }
        }

        public List<SaveControlAxis> ShowDocumentAxis(string DocumentUniqueId)
        {
            try
            {


                var pathroot = _hostingEnvironment.ContentRootPath;
                SaveControlAxis model = new SaveControlAxis();
                if ( DocumentUniqueId != ""||  DocumentUniqueId != null)
                {
                    var dataAxisList = _SaveControlAxis.Wherecondtion(x => x.DocumentUniqueId == DocumentUniqueId).ToList();

                    return dataAxisList;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ViewListModel> ShowSignerList(string DocumentUniqueId)
        {
            try
            {              
                if (DocumentUniqueId != "" || DocumentUniqueId != null)
                {
                    ViewListModel model = new ViewListModel();
                    List<ViewListModel> lstobj = new List<ViewListModel>();
                    var dataAxisList = _RecipientSignerForDocument.Where(x => x.DocumentUniqueId == DocumentUniqueId).ToList();
                    var dataAxisListMaster = _SenddocumentForSigns.Where(x => x.UniqueId == DocumentUniqueId).FirstOrDefault();

                    var count = dataAxisList.Count();
                   
                    for (int i = 0; i < count; i++)
                    {
                       
                        {
                            model.SignerName = dataAxisList[i].Name;
                            model.Signdate = dataAxisList[i].Signdate;
                            model.EmailTo = dataAxisList[i].SignerEmail;
                            model.createddate = dataAxisList[i].Createdate;
                            model.Issign = dataAxisList[i].IsSign;
                            model.EmailTo = dataAxisList[i].SignerEmail;
                            if (dataAxisListMaster != null)
                            {


                                model.DocumentName = dataAxisListMaster.DocumentName;
                            }
                        };
                        
                        lstobj.Add(model);
                      
                    }
                    return lstobj;
                }
             
                else
                {
                    return null;
                }
               return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ViewListModel> ShowAllSignerList()
        {
            //if (DocumentUniqueId != "" || DocumentUniqueId != null)
            //{
            try
            {
                ViewListModel model = new ViewListModel();
                List<ViewListModel> lstobj = new List<ViewListModel>();
                var dataAxisList = _RecipientSignerForDocument.Where(x => x.DocumentUniqueId != null).ToList();
                var count = dataAxisList.Count();

                for (int i = 0; i < count; i++)
                {

                    {
                        model.SignerName = dataAxisList[i].Name;
                        model.Signdate = dataAxisList[i].Signdate;
                        model.EmailTo = dataAxisList[i].SignerEmail;
                        model.createddate = dataAxisList[i].Createdate;
                        model.Issign = dataAxisList[i].IsSign;
                        model.EmailTo = dataAxisList[i].SignerEmail;


                    };
                    lstobj.Add(model);

                }
                return lstobj;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        
        }
            //}
        //public string GetSignerRecord(string ChildSignerId , string DocumentUniqueId)
        //{

        //    try
        //    {
        //       // string documentuniqueId = ChildSignerId;
        //        List<RecipientSignerForDocument> Returnid = _RecipientSignerForDocument.Wherecondtion(x => x.DocumentUniqueId == DocumentUniqueId && x.SingerUniqueId == ChildSignerId).ToList();
        //        if (Returnid.Count > 0)
        //        {
        //            return Returnid.ToList();
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        //   ExceptionLogging.SendErrorToText(ex);
        //        throw ex;
        //        return null;
        //    }

        //}

    }
}
    
