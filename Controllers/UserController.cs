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
            bool userAlreadyExists = db.RegUsers.Any(x => x.username == uc.username);
            if (ModelState.IsValid && userAlreadyExists != true)
            {
                db.RegUsers.Add(new RegUser()
                {
                    username = uc.username,
                    password = uc.password,
                    name = uc.Name,
                    email = uc.email,
                    retypepassword = uc.retypepassword,
                    admin = uc.Admin.ToString()
                });
                db.SaveChanges();
                return Ok("User created successfully");
            }
            else
            {
                if (userAlreadyExists)
                {
                    return Content(HttpStatusCode.NotFound, "Username already exists");
                   // return Ok("Username already exists");
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Failed to save User");
                }
                
            }
            
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
      
        [Route("api/User/insertrequest/{MaintanenceTable}/{filepath}/{ext}")]
        public IHttpActionResult Insertrequest(MaintanenceTable mt, string filepath=null,string ext=null)
        {
            BMSdbEntities db = new BMSdbEntities();
            db.MaintanenceTables.Add(mt);
            db.SaveChanges();
            if (filepath!= "null")
            {
                fileupload fu = new fileupload();
                fu.Reqid = mt.Reqid;
                fu.filename= filepath + "." + ext;
                fu.filepath = HttpContext.Current.Server.MapPath("~/Uploads/") + filepath + "." + ext;
                db.fileuploads.Add(fu);
                db.SaveChanges();
            }
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
                fileupload fudel= (from c in db.fileuploads
                                   where c.Reqid == maintainreq.Reqid
                                   select c).FirstOrDefault();
                db.fileuploads.Remove(fudel);
                db.SaveChanges();
                MaintanenceTable updatedreq = (from c in db.MaintanenceTables
                                               where c.Reqid == maintainreq.Reqid
                                               select c).FirstOrDefault();

                db.MaintanenceTables.Remove(updatedreq);
                db.SaveChanges();
            }
        }

    }
}
