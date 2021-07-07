using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BMS.Models;

namespace BMS.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult userRegform(UserClass uc)
        {
            BMSdbEntities db = new BMSdbEntities();
            db.RegUsers.Add(new RegUser()
            {
                username = uc.username,
                password=uc.password,
                name=uc.Name,
                email=uc.email,
                retypepassword=uc.retypepassword,
                admin=uc.Admin.ToString()
            }) ;
            db.SaveChanges();
            return Ok();
            
        }
        [Route("api/User/Logapi/{uname}/{pwd}")]
        [HttpGet]
        public IHttpActionResult Loginform(string uname,string pwd)
        {
            BMSdbEntities db = new BMSdbEntities();
            var result = db.RegUsers.Where(a => a.username==uname && a.password== pwd ).FirstOrDefault();
            if (result != null)
            {
                return Ok(result.uid.ToString());
            }
            else
            {
                return NotFound() ;
            }
           

        }
        [Route("api/User/fileupload")]
        public HttpResponseMessage UploadFiles()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = HttpContext.Current.Request.Form["fileName"] + Path.GetExtension(postedFile.FileName);

            //Save the File.
            postedFile.SaveAs(path + fileName);


            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK,fileName);
        }
        //public Task<HttpResponseMessage> Post()
        //{
        //    List<string> savedFilePath = new List<string>();
        //    // Check if the request contains multipart/form-data
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }
        //    //Get the path of folder where we want to upload all files.
        //    string rootPath = HttpContext.Current.Server.MapPath("~/documents");
        //    var provider = new MultipartFileStreamProvider(rootPath);
        //    // Read the form data.
        //    //If any error(Cancelled or any fault) occurred during file read , return internal server error
        //    var task = Request.Content.ReadAsMultipartAsync(provider).
        //        ContinueWith<HttpResponseMessage>(t =>
        //        {
        //            if (t.IsCanceled || t.IsFaulted)
        //            {
        //                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
        //            }
        //            foreach (MultipartFileData dataitem in provider.FileData)
        //            {
        //                try
        //                {
        //                    //Replace / from file name
        //                    string name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "");
        //                    //Create New file name using GUID to prevent duplicate file name
        //                    string newFileName = Guid.NewGuid() + Path.GetExtension(name);
        //                    //Move file from current location to target folder.
        //                    File.Move(dataitem.LocalFileName, Path.Combine(rootPath, newFileName));


        //                }
        //                catch (Exception ex)
        //                {
        //                    string message = ex.Message;
        //                }
        //            }

        //            return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
        //        });
        //    return task;
        //}
        [Route("api/User/insertrequest/{MaintanenceTable}/{filepath}/{ext}")]
        public IHttpActionResult Insertrequest(MaintanenceTable mt, string filepath,string ext)
        {
            BMSdbEntities db = new BMSdbEntities();
            db.MaintanenceTables.Add(mt);
            db.SaveChanges();
            fileupload fu = new fileupload();
            fu.Reqid = mt.Reqid;
            fu.filepath = HttpContext.Current.Server.MapPath("~/Uploads/") +filepath +"."+ ext;
            db.fileuploads.Add(fu);
            db.SaveChanges();
            return Ok("Request Send Successfully");

        }

        [Route("api/User/UpdateReq")]
        [HttpPost]
        public bool Updatereq(MaintanenceTable maintainreq)
        {
            using (BMSdbEntities db =  new BMSdbEntities())
            {
                MaintanenceTable updatedreq = (from c in db.MaintanenceTables
                                            where c.Reqid == maintainreq.Reqid
                                            select c).FirstOrDefault();
                updatedreq.request = maintainreq.request;
                updatedreq.status = maintainreq.status;
                if (maintainreq.status != "Pending")
                {
                    updatedreq.RejApproveDate = maintainreq.RejApproveDate;
                }
                db.SaveChanges();
            }

            return true;
        }

        [Route("api/Users/DeleteReq")]
        [HttpPost]
        public void DeleteReq(MaintanenceTable maintainreq)
        {
            using (BMSdbEntities db = new BMSdbEntities())
            {
                MaintanenceTable updatedreq = (from c in db.MaintanenceTables
                                               where c.Reqid == maintainreq.Reqid
                                               select c).FirstOrDefault();

                db.MaintanenceTables.Remove(updatedreq);
                db.SaveChanges();
            }
        }

    }
}
