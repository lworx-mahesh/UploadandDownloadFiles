using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadandDownloadFiles.DTODocuSign;
using UploadandDownloadFiles.Ilogics;
using UploadandDownloadFiles.IRepository;
using UploadandDownloadFiles.Model;
using UploadandDownloadFiles.Services;

namespace UploadandDownloadFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        #region Property
        private readonly IFileService _fileService;
        #endregion

        #region Property
        private readonly ISavedocumentsignparameters _savedocumentsignparameters;
        private readonly ISenddocumentForSign _SenddocumentForSign;
        #endregion
        #region Constructor
        public FileController(IFileService fileService, ISavedocumentsignparameters savedocumentsignparameters, ISenddocumentForSign SenddocumentForSigns)
        {
            _fileService = fileService;
            _savedocumentsignparameters = savedocumentsignparameters;
            _SenddocumentForSign = SenddocumentForSigns;
       

        }
        #endregion

        #region Upload
     //   [HttpPost(nameof(Upload))]
        //public IActionResult Upload([Required] List<IFormFile> formFiles, [Required] string subDirectory)
        //{
        //    try
        //    {
        //        _fileService.UploadFile(formFiles, subDirectory);

        //        return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        #endregion

        #region Download File
        //[HttpGet(nameof(Download))]
        //public IActionResult Download([Required]string subDirectory)
        //{

        //    try
        //    {
        //        var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(subDirectory);

        //        return File(archiveData, fileType, archiveName);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

   

        #endregion


        // POST:api/File/UploadDocumentWithParameter/{formFiles}/{To}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFiles"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        #region Upload

        [HttpPost(nameof(UploadDocumentWithParameter))]
        public IActionResult UploadDocumentWithParameter([FromForm] MailModel ObjModel)
        {
            try
            {

               // List<MailModel> ObjModel = new List<MailModel>();

              var Return_UniqueDocumentid=  _fileService.UploadFilewithparameters(ObjModel);

                return Ok(new { ObjModel.File.Count, Size = _fileService.SizeConverter(ObjModel.File.Sum(f => f.Length)), Return_UniqueDocumentid });
                //return Ok(new
                //{
                //    responseObj,
                //    CleaningScheduleSaved
                //});
                // return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        #endregion


        #region SendDocument
        [HttpPost(nameof(SendDocumentWithAxis))]
        public IActionResult SendDocumentWithAxis( DocumentWithAxisModel ObjModel)
        {
            try
            {

                // List<MailModel> ObjModel = new List<MailModel>();

                _savedocumentsignparameters.SendDocumentWithAxis(ObjModel);

                //  return Ok(new { ObjModel.File.Count, Size = _fileService.SizeConverter(ObjModel.File.Sum(f => f.Length)) });
                return Ok("chck");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion






        #region Show Document After Click on Mail Link
        [HttpPost(nameof(ShowDocumentAfterClickLink))]
        public IActionResult ShowDocumentAfterClickLink(string ChildUniqueId, string DocumentUniqueId)
        {
            try
            {
              
               var obj=  _savedocumentsignparameters.ShowDocumentAfterClickLink(ChildUniqueId, DocumentUniqueId);
                var objAxis = _savedocumentsignparameters.ShowDocumentAxis( DocumentUniqueId);
                return Ok(new { obj,objAxis });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion


        #region Daocument Save With Button Value
        [HttpPost(nameof(DocumentSaveWithButtonValue))]
        public IActionResult DocumentSaveWithButtonValue(DocWithBtnValModel objmodel)
        {
            try
            {
                _savedocumentsignparameters.SaveBtnValueInfo(objmodel);
               
                return Ok(objmodel);
             }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion



        //#region Index
        //[HttpPost(nameof(Index))]
        //public IActionResult Index(string ChildUniqueId, string DocumentUniqueId)
        //{
        //    return null;
        //}
        //#endregion


        #region ViewSinerList
        [HttpPost(nameof(ViewSinerList))]
        public IActionResult ViewSinerList(string DocumentUniqueId)
        {
            try
            {


                var obj = _savedocumentsignparameters.ShowSignerList(DocumentUniqueId);
                return Ok(new { obj });

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }
        #endregion


        #region ViewAllSinerList
        [HttpPost(nameof(ViewAllSinerList))]
        public IActionResult ViewAllSinerList()
        {
            try
            {


                var obj = _savedocumentsignparameters.ShowAllSignerList();
                return Ok(new { obj });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PdfTracker
        [HttpPost(nameof(PdfTracker))]
        public IActionResult PdfTracker(string DocumentUniqueId)
        {

            try
            {
                var ListPdfTracker = _SenddocumentForSign.DocumentStatus(DocumentUniqueId);
            return Ok(new { ListPdfTracker });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
