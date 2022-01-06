using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using UploadandDownloadFiles.DbcontextData;
using UploadandDownloadFiles.DTODocuSign;
using UploadandDownloadFiles.IRepository;
using UploadandDownloadFiles.Repository;
using UploadandDownloadFiles.TableModel;

namespace UploadandDownloadFiles.Services
{
    public class FileService : IFileService
    {
        #region Property
        private IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Constructor
        //public FileService(IHostingEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //}
        #endregion


        private readonly docusigndbcontext _context;
        private readonly IRepository<StoreSignerInfo> _StoreSignerInfo;
        private readonly IRepository<SignerColourListModel> _SignerColourListModel;
        public FileService(
            IHostingEnvironment hostingEnvironment,
            IRepository<StoreSignerInfo> StoreSignerInfo,
            IRepository<SignerColourListModel> SignerColourListModel, docusigndbcontext context)
        {
            _StoreSignerInfo = StoreSignerInfo;
            _SignerColourListModel = SignerColourListModel;
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }

        #region Upload File
            public void UploadFile(List<IFormFile> files, string subDirectory)
        {
            subDirectory = subDirectory ?? string.Empty;
            var target = Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory);

            Directory.CreateDirectory(target);

            files.ForEach(async file =>
            {
                if (file.Length <= 0) return;
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            });
        }
        #endregion

        #region Download File
        public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
        {
            var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            var files = Directory.GetFiles(Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory)).ToList();

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    files.ForEach(file =>
                    {
                        var theFile = archive.CreateEntry(file);
                        using (var streamWriter = new StreamWriter(theFile.Open()))
                        {
                            streamWriter.Write(File.ReadAllText(file));
                        }

                    });
                }

                return ("application/zip", memoryStream.ToArray(), zipName);
            }

        }
        #endregion

        #region Size Converter
        public string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }

        public void UploadFile(List<IFormFile> files)
        {
            var subDirectory = "" ?? string.Empty;
            var target = Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory);

            Directory.CreateDirectory(target);

            files.ForEach(async file =>
            {
                if (file.Length <= 0) return;
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            });
        #endregion

        }


        #region Upload File with parameters
        public string UploadFilewithparameters(MailModel ObjModel)
        {
            string path = "\\Files";
            var pathroot = path.Replace("\\files", "/files/");
            var path1 = _hostingEnvironment.ContentRootPath + pathroot;

          //  string path1 =@"C:\Users\Spadez\Downloads\UploadandDownloadFiles\UploadandDownloadFiles\UploadandDownloadFiles\Files";
            //string path2 = "My Music";
            // fullPath = Path.Combine(path1, path2);

            //var subDirectory = "DownloadDocument";
            //var target = Path.Combine(path1, subDirectory);

            //Directory.CreateDirectory(target);


            var Returnid = "";

            StoreSignerInfo obj = new StoreSignerInfo();
                StoreSignerInfo objlistval = new StoreSignerInfo();
              //  objlistval.SignerList = new List<StoreSignerInfo>();
                //string email = Session["Email"].ToString();
                //var emailid = TempData["Emailid"];
                if (ObjModel.mysignervalue == "I'm the only signer")
                {
                    try
                    {
                        //@ViewBag.Message = "Welcome to new page";
                        int usernamendex = 0;
                        int ordernumber = 0;
            
                        string Result = null;
                        string ResultPdfPath = null;
                    //   string FilesName = DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_" + Path.GetFileName(FileUploader.FileName);

                   // string path1 = @"C:\Users\Spadez\Downloads\UploadandDownloadFiles\UploadandDownloadFiles\UploadandDownloadFiles\Files";
                    //string path2 = "My Music";
                    // fullPath = Path.Combine(path1, path2);

                    var subDirectory = "DownloadDocument";
                    var target = Path.Combine(path1, subDirectory);


                    Directory.CreateDirectory(target);
                    ObjModel.File.ForEach(async file =>
                    {
                        if (file.Length <= 0) return;
                        var filePath = Path.Combine(target, file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    });


                    //string FilesName = DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_" + Path.GetFileName(ObjModel.FileUploader.FileName);
                       string _FileName = subDirectory + ".pdf";
                    //    //  string filePath = Path.Combine(Microsoft.SqlServer.Server.MapPath("~/files////"), _FileName);
                    //    //  HttpContext.Current.Server.MapPath
                    //    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/files////"), _FileName);
                        string _filePath = Path.GetDirectoryName(path1);
                        string shortPath = Path.Combine(Path.GetFileName(_filePath), _FileName);
                    //    //  fileUploader.SaveAs(filePath);
                    //    ObjModel.FileUploader.SaveAs(filePath);
                    Guid g = Guid.NewGuid();
                    var uniqueid="";
                        string guid = g.ToString();
                        foreach (var toname in ObjModel.To)
                        {
                            Guid usergid = Guid.NewGuid();
                            string SignerUnique_Id = usergid.ToString();
                            ordernumber = ordernumber + 1;
                            obj.SignerEmail = toname;
                            obj.Name = ObjModel.USERNAME[usernamendex];
                            obj.Filepath = "/" + shortPath;
                            obj.SignerOrder = ordernumber;
                            obj.Days = ObjModel.Days;
                            obj.DocumentUniqueId = guid;
                            obj.SignerUniqueId = SignerUnique_Id;
                            obj.Subject = ObjModel.Subject;
                            obj.Message = ObjModel.Body;
                            if (ObjModel.mysignervalue == null)
                            {
                                obj.SignerType = ObjModel.myTypeMailSend;

                            }
                            else
                            {
                                obj.SignerType = ObjModel.mysignervalue;
                                //@ViewBag.signertype = obj.SignerType;   // use for storing single singe
                                //@ViewBag.SignerEmailid = obj.SignerEmail;
                                //@ViewBag.SignerName = obj.Name;
                            }



                        Returnid = storesignerinfo(obj);


                        usernamendex = usernamendex + 1;
                        }
  



                    if (Returnid != "")
                        {
                            SignerColourListModel listobj = new SignerColourListModel();
                            var Signerrecords = _StoreSignerInfo.Wherecondtion(s => s.DocumentUniqueId == guid).ToList();
                        var Results = SignerrecordsList("1", guid);
                        if (Results.Count == 0)
                        {
                           // @ViewBag.AxisError = "true";
                        }
                        else
                        {
                            foreach (var item in Results)
                            {
                                listobj.SignerColorList.Add(new SignerColourListModel
                                {
                                    SignerEmail = item.SignerEmail,
                                    Name = item.Name,
                                    SignerOrder = item.SignerOrder,
                                    DocumentUniqueId = item.DocumentUniqueId,
                                    Filepath = item.Filepath,
                                    ColorCode = item.ColorCode,
                                    SignerUniqueID = item.SignerUniqueID

                                });
                                Result = item.Filepath;
                                ResultPdfPath = Result.Replace("files\\", "files/");
                               // ViewBag.fiepath = ResultPdfPath;
                               // ViewBag.filename = _FileName.Split('_')[2];
                            }
                           // TempData["test"] = guid;
                          //  return View(listobj);
                          //  return "check value";
                        }
                    }
                      //  return "check value";
                    }
                    catch (Exception ex)
                    {
                   // return Ok(ex);
                  //  return BadRequest(ex.Message);
                    //ExceptionLogging.SendErrorToText(ex);
                    //  return "check catch";

                }
                }
                else if (ObjModel.mysignervalue != "I'm the only signer")
                {
                    try
                    {
                        //  @ViewBag.Message = "Welcome to new page";
                        int usernamendex = 0;
                        int ordernumber = 0;
                   
                        string Result = null;
                        string ResultPdfPath = null;
            var subfileName = ObjModel.File[0].FileName;
                   var subDirectory = "DownloadDocument";
                        var target = Path.Combine(path1, subDirectory);


                          Directory.CreateDirectory(target);
                       ObjModel.File.ForEach(async file =>
                         {
                        if (file.Length <= 0) return;
                        var filePath = Path.Combine(target, file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    });


                  //  string FilesName = DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_" + Path.GetFileName(ObjModel.File);
                    string _FileName = subfileName;
                    //    //  string filePath = Path.Combine(Microsoft.SqlServer.Server.MapPath("~/files////"), _FileName);
                    //    //  HttpContext.Current.Server.MapPath
                    //    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/files////"), _FileName);
              //      string _filePath = Path.GetDirectoryName(path1);
                    string shortPath = Path.Combine(Path.GetFileName(target), _FileName);
             
                             var docfilepath = Path.Combine(target, _FileName);
                    Guid g = Guid.NewGuid();
                        string guid = g.ToString();
                        foreach (var toname in ObjModel.To)
                        {
                            Guid usergid = Guid.NewGuid();
                            string SignerUnique_Id = usergid.ToString();
                            ordernumber = ordernumber + 1;
                            obj.SignerEmail = toname;
                            obj.Name = ObjModel.USERNAME[usernamendex];
                        //          obj.Filepath = "/" + shortPath;
                        obj.Filepath = docfilepath;
                            obj.SignerOrder = ordernumber;
                            obj.Days = ObjModel.Days;
                            obj.DocumentUniqueId = guid;
                            obj.SignerUniqueId = SignerUnique_Id;
                            obj.Subject = ObjModel.Subject;
                            obj.Message = ObjModel.Body;
                            if (ObjModel.mysignervalue == null)
                            {
                                obj.SignerType = ObjModel.myTypeMailSend;

                            }
                            else
                            {
                                obj.SignerType = ObjModel.mysignervalue;
                                //@ViewBag.signertype = obj.SignerType;   // use for storing single singe
                                //@ViewBag.SignerEmailid = obj.SignerEmail;
                                //@ViewBag.SignerName = obj.Name;
                            }
                            Returnid = storesignerinfo(obj);
                            usernamendex = usernamendex + 1;
                        }

                        if (Returnid != "")
                        {
                            SignerColourListModel listobj = new SignerColourListModel();
                            var Signerrecords = _StoreSignerInfo.Wherecondtion(s => s.DocumentUniqueId == guid).ToList();
                        var Results = SignerrecordsList("1", guid);
                        if (Results.Count == 0)
                        {
                          //  @ViewBag.AxisError = "true";
                        }
                        else
                        {
                            foreach (var item in Results)
                            {
                                listobj.SignerColorList.Add(new SignerColourListModel
                                {
                                    SignerEmail = item.SignerEmail,
                                    Name = item.Name,
                                    SignerOrder = item.SignerOrder,
                                    DocumentUniqueId = item.DocumentUniqueId,
                                    Filepath = item.Filepath,
                                    ColorCode = item.ColorCode,
                                    SignerUniqueID = item.SignerUniqueID

                                });
                                Result = item.Filepath;
                                ResultPdfPath = Result.Replace("files\\", "files/");
                             //   ViewBag.fiepath = ResultPdfPath;
                            //    ViewBag.filename = _FileName.Split('_')[2];
                            }
                          //  TempData["test"] = guid;
                           // return View(listobj);

                        }
                //         return "check value";
                    }
                     //   return "obj";

                    }
                    catch (Exception ex)
                    {
                      //  ExceptionLogging.SendErrorToText(ex);
                     //   return "objvalue";
                        //  return null;
                    }
                }
            return Returnid;
                //else
                //{
                //    //@ViewBag.Error = "Enter Correct Email id";
                //    //return View("SendContractpdfToMail");
                //}
        }
        //  return "check value";
        #endregion


        #region store signer info
        public string storesignerinfo(StoreSignerInfo obj)
        {
            var count = 0;
            try
            {
                string Returnid;

                var StoreSignerInfo = new StoreSignerInfo()
                {
                    SignerEmail = obj.SignerEmail,
                    Name = obj.Name,
                    Filepath = obj.Filepath,
                    SignerOrder = obj.SignerOrder,
                    DocumentUniqueId = obj.DocumentUniqueId,
                    Days = obj.Days,
                    SignerUniqueId = obj.SignerUniqueId,
                    SignerType = obj.SignerType,
                    Subject = obj.Subject,
                    Message = obj.Message,
                    //     StoreSignerInfoId = 0
                    //  Returnid = _SenddocumentForSignRepository.saveMultipleSigner(obj);
                  
                };

           
                _context.StoreSignerInfo.Add(StoreSignerInfo);
                // _context.SaveChanges();
                // var RID=   _StoreSignerInfo.InsertAndGetId(obj);
                // _context._StoreSignerInfo.Add(StoreSignerInfo);
                // _context.Entry(entity).State = EntityState.Added;
                _context.SaveChanges();
                Returnid = obj.DocumentUniqueId;
                return Returnid;

            }
            catch(Exception ex)
            {
             //   count++;
                return "check any error";
            }
         
             //   return 1;
           
           
        }
        #endregion


        #region store signer info
        public List<SignerColourListModel> SignerrecordsList(string id, string guid)
        {
            try
            {

            //    JsonResult result = new JsonResult();
                string procName = "usp_getsignerListbydocumentid";
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@Opcode", Value = "1", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@Documentid", Value = guid, DbType = System.Data.DbType.String };

                var resultData = _SignerColourListModel.ExecuteWithJsonResult(procName, "SignerList", ParamsArray);
                var finalList = resultData != null ? resultData : new List<SignerColourListModel>();
                return finalList;
            }
            catch (Exception ex)
            {
             //   ExceptionLogging.SendErrorToText(ex);
                throw ex;
            }
        }
        #endregion
    }
}

