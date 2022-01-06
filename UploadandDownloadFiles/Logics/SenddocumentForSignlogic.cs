using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadandDownloadFiles.DbcontextData;
using UploadandDownloadFiles.DTODocuSign;
using UploadandDownloadFiles.IRepository;
using UploadandDownloadFiles.TableModel;

namespace UploadandDownloadFiles.Logics
{
    public class SenddocumentForSignlogic: ISenddocumentForSign
    {
        private readonly docusigndbcontext _context;
        private readonly IRepository<SenddocumentForSigns> _SenddocumentForSigns;
        public SenddocumentForSignlogic(docusigndbcontext docusigndbcontext, IRepository<SenddocumentForSigns> SenddocumentForSign)
        {
            this._context = docusigndbcontext;
            _SenddocumentForSigns = SenddocumentForSign;
        }

        public void Dispose()
        {
          //  throw new NotImplementedException();
        }

        public IEnumerable<SenddocumentForSigns> GetAll()
        {
            return _context.SenddocumentForSigns.AsEnumerable();
        }
        ///
        public bool? checkIssignOfChild(string id)
        {

            bool? checkstatus = false;
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException("uniqueid is null");
                }
                else
                {

                    var Data = _context.RecipientSignerForDocuments.Where(x => x.DocumentUniqueId == id && x.IsSign == true).FirstOrDefault();
                    var SignerDataReminder = _context.RecipientSignerForDocuments.Where(x => x.DocumentUniqueId == id && x.IsSign == false).FirstOrDefault();
                    if (SignerDataReminder != null)
                    {
                        checkstatus = Convert.ToBoolean(SignerDataReminder.IsSign);
                    }
                    else
                    //if (Data != null)
                    {
                        checkstatus = Convert.ToBoolean(Data.IsSign);
                    }



                    //  _context.SenddocumentForSign.Add(Data);
                    return checkstatus;
                }
            }
            catch (Exception ex)
            {
                //   ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
        }


        public bool? checkIssign(string id)
        {

            bool? checkstatus = false;
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException("uniqueid is null");
                }
                else
                {

                    var Data = _context.SenddocumentForSigns.Where(x => x.UniqueId == id && x.Issign == true).FirstOrDefault();

                    if (Data != null)
                    {
                        checkstatus = Convert.ToBoolean(Data.Issign);

                    }



                    //  _context.SenddocumentForSign.Add(Data);
                    return checkstatus;
                }
            }
            catch (Exception ex)
            {
                //    ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
        }
        public int? Update(string id, string downloadpdffilepath)
        {
            //Int32 StudentId = (int)TempData["StudentId"];
            //var SenddocumentForDownloadPdfFileds = new SenddocumentForDownloadPdf() UpdateChildIsSign(id);
            //{
            //    Issign = true,
            //    Signdate = DateTime.Now
            //};
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException("uniqueid is null");
                }
                else
                {

                    var Data = _context.SenddocumentForSigns.Where(x => x.UniqueId == id).FirstOrDefault();
                    if (Data != null)
                    {
                        Data.Issign = true;
                        Data.Signdate = DateTime.Now;
                        Data.SignpdfFilePath = downloadpdffilepath;

                    }
                    //  _context.SenddocumentForSign.Add(Data);
                    return _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //      ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
        }

        //public int? UpdateSignerInfo(AccountLogin entity, string EmailId)
        //{
        //    try
        //    {
        //        if (EmailId == null)
        //        {
        //            throw new ArgumentNullException("EmailId is null");
        //        }
        //        else
        //        {

        //            var Data = _context.AccountLogin.Where(x => x.Email == EmailId).FirstOrDefault();
        //            if (Data != null)
        //            {
        //                Data.UserName = entity.UserName;
        //                Data.LastName = entity.LastName;
        //                Data.Email = entity.Email;
        //                Data.MobileNumber = entity.MobileNumber;
        //                Data.Password = entity.Password;
        //                //    Data.Filepath = filepath;

        //            }
        //            //  _context.SenddocumentForSign.Add(Data);
        //            return _context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionLogging.SendErrorToText(ex);
        //        return null;
        //    }
        //}
        public int? UpdateChildIsSign(string id)
        {
            //Int32 StudentId = (int)TempData["StudentId"];
            //var SenddocumentForDownloadPdfFileds = new SenddocumentForDownloadPdf() 
            //{
            //    Issign = true,
            //    Signdate = DateTime.Now
            //};
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException("uniqueid is null");
                }
                else
                {
                    var Data = _context.RecipientSignerForDocuments.Where(x => x.SingerUniqueId == id && x.IsSign == false).FirstOrDefault();

                    //         var Data = _context.RecipientSignerForDocument.Where(x => x.DocumentUniqueId == id && x.IsSign==false).FirstOrDefault();
                    if (Data != null && Data.IsMailSend != false)
                    {
                        Data.IsSign = true;
                        Data.Signdate = DateTime.Now;
                        //    Data.Filepath = filepath;

                    }
                    //  _context.SenddocumentForSign.Add(Data);
                    return _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //   ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
        }

        public int? UpdateChildSignPdfPath(string documentuniqueId, string DownloadPdfwithSign)
        {
            //Int32 StudentId = (int)TempData["StudentId"];
            //       var senddocumentForSign = new SenddocumentForSign(); 
            //{
            //    Issign = true,
            //    Signdate = DateTime.Now
            //};
            try
            {
                if (documentuniqueId == null)
                {
                    throw new ArgumentNullException("uniqueid is null");
                }
                else
                {
                    var Data = _context.RecipientSignerForDocuments.Where(x => x.DocumentUniqueId == documentuniqueId && x.IsSign == true).OrderByDescending(s => s.Id).FirstOrDefault();

                    //     var Data = _context.RecipientSignerForDocument.Where(x => x.DocumentUniqueId == documentuniqueId && x.IsSign == true).LastOrDefault();
                    //    var Data = _context.RecipientSignerForDocument.Where(x => x.DocumentUniqueId == documentuniqueId && x.IsSign==false).FirstOrDefault();
                    if (Data != null && Data.IsMailSend != false)
                    {



                        Data.SignPdfFilepath = DownloadPdfwithSign;


                        //       _context.RecipientSignerForDocument.Add(Data);
                        //  Data.IsSign = true;
                        //    senddocumentForSign.SignpdfFilePath= DownloadPdfwithSign;
                        // Data
                        //               Data.SignPdfFilepath = senddocumentForSign.SignpdfFilePath;
                        //  Data.Signdate = DateTime.Now;
                        //    Data.Filepath = filepath;

                    }

                    // _context.Entry(entity).State = EntityState.Added;
                    ///     _context.SaveChanges();
                    //  _context.SenddocumentForSign.Add(Data);
                    return _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //        ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
        }


        public int? Insert(SenddocumentForSigns entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity is null");
                }
                else
                {
                    var senddocfields = new SenddocumentForSigns()
                    {

                        DocumentName = entity.DocumentName,
                        Filepath = entity.Filepath,
                        createddate = entity.createddate,
                        EmailTo = entity.EmailTo,
                        UniqueId = entity.UniqueId,
                        Subject = entity.Subject,
                        Issign = entity.Issign,
                        Signdate = null,
                        OrderNumber = entity.OrderNumber,
                        DocumentExpireDays = entity.DocumentExpireDays,
                        CreatorUniqueId = null,
                        SignpdfFilePath = null,
                        TotalSigner=entity.TotalSigner,
                        Isdeleted=false


                    };

                    _context.SenddocumentForSigns.Add(senddocfields);
                    // _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();
                    return senddocfields.Id;
                }

            }
            catch (Exception ex)
            {
                //     ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
        }

        public int? saveIPObjval(string childSignerid, string RecipientIpaddress)
        {

            try
            {
                if (childSignerid == null && RecipientIpaddress == "")
                {
                    throw new ArgumentNullException("check Uniqueid and RecipientIpaddress");
                }
                else
                {
                    var Data = _context.RecipientSignerForDocuments.Where(x => x.SingerUniqueId == childSignerid && x.IsSign == false).FirstOrDefault();

                    if (Data != null && Data.IsMailSend != false)
                    {
                        Data.Ipaddress = RecipientIpaddress;

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
       
        public int? SaveAxisInfo(SaveControlAxis entity, string Documentrecord)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity is null");
                }
                else
                {
                    Guid gid = Guid.NewGuid();
                    string guid = gid.ToString();
                    var SaveControlAxis = new SaveControlAxis()
                    {
                        ButtonHeight = entity.ButtonHeight,
                        ButtonOffsetLeft = entity.ButtonOffsetLeft,
                        ButtonOffsetTop = entity.ButtonOffsetTop,
                        ButtonWidth = entity.ButtonWidth,
                        controllerid = entity.controllerid,
                        DocumentUniqueId = Documentrecord,
                        ControllerUniqueId = guid,
                        //ControlFieldId = entity.ControlFieldId,
                        //UserOrderId = entity.UserOrderId,
                        //ColorCode = entity.ColorCode

                    };


                    _context.SaveControlAxis.Add(SaveControlAxis);
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


        /// <summary>
        /// use for change issign status and signdate..
        /// </summary>
        /// <param name="Uniqueid"></param>
        /// <returns></returns>
        public int? LastSignerUpdate(string Uniqueid, string FilePath)
        {
            try
            {
                if (Uniqueid == null)
                {
                    throw new ArgumentNullException("ID is null");
                }
                else
                {

                    var Data = _context.SenddocumentForSigns.Where(x => x.UniqueId == Uniqueid && x.Issign == false).FirstOrDefault();
                    if (Data != null)
                    {
                        Data.Issign = true;
                        Data.Signdate = DateTime.Now;
                        Data.SignpdfFilePath = FilePath;
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







                //  var RecipientSignerForDocument = new RecipientSignerForDocument()
                //   {

                //SignerEmail = entity.SignerEmail,
                //Name = entity.Name,
                //Filepath = entity.Filepath,
                //SignerOrder = entity.SignerOrder,
                //Signdate = entity.Signdate,
                //DocumentUniqueId = entity.DocumentUniqueId,
                //IsSign = entity.IsSign,
                //ParentsentId = entity.ParentsentId,
                //  Returnid = _SenddocumentForSignRepository.saveMultipleSigner(obj);

                //    };

                //_context.RecipientSignerForDocument.Add(RecipientSignerForDocument);
                //// _context.Entry(entity).State = EntityState.Added;
                //return _context.SaveChanges();
                //     }

            }
            catch (Exception ex)
            {
                //    ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
            }
        }


        //public int? ReminderSignerUpdate(string unqiueid)
        //{
        //    try
        //    {
        //        if (unqiueid == null)
        //        {
        //            throw new ArgumentNullException("ID is null");
        //        }
        //        else
        //        {

        //            var Data = _context.SignerReminder.Where(x => x.SignerUniqueId == unqiueid && x.IsMailSend == false).FirstOrDefault();
        //            if (Data != null)
        //            {
        //                Data.IsMailSend = true;
        //                //Data.MailSendDate = DateTime.Now;
        //            }
        //            //  _context.SenddocumentForSign.Add(Data);
        //            return _context.SaveChanges();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionLogging.SendErrorToText(ex);
        //        return null;
        //    }
        //}



        //public List<SenddocumentForSign> Where(Expression<Func<SenddocumentForSign, bool>> predicate)
        //{
        //    try
        //    {

        //        var list = new List<SenddocumentForSign>(_context.Set<SenddocumentForSign>().Where(predicate));
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

  
        public int? saveMultipleSigner(RecipientSignerForDocuments entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity is null");
                }
                else
                {
                    var recipientSignerForDocuments = new RecipientSignerForDocuments()
                    {
                        SignerEmail = entity.SignerEmail,
                        Name = entity.Name,
                        Filepath = entity.Filepath,
                        SignerOrder = entity.SignerOrder,
                        MailSendDate = DateTime.Now,
                   //     Signdate = entity.Signdate,
                        DocumentUniqueId = entity.DocumentUniqueId,
                        IsSign = entity.IsSign,
                     //   ParentsentId = entity.ParentsentId,
                        SingerUniqueId = entity.SingerUniqueId,
                     //   CreatorUniqueId = entity.CreatorUniqueId
                        //  Returnid = _SenddocumentForSignRepository.saveMultipleSigner(obj);

                    };
                    var recipientSignerForDocuments1 = _context.RecipientSignerForDocuments.Where(p => p.DocumentUniqueId == "47851142-4d9c-4d8a-b04a-5515a34c3331").FirstOrDefault();

                       _context.RecipientSignerForDocuments.Add(recipientSignerForDocuments);
           //         .Add(recipientSignerForDocuments);
                    // _context.Entry(entity).State = EntityState.Added;
                    return _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                // ExceptionLogging.SendErrorToText(ex);
                throw ex;
                return null;
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

                    if (dataAxisList != null)
                    {


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
                    }

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
    }
}
