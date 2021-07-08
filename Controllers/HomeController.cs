using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BMS.Models;

namespace BMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Show()
        {
            string x = System.Web.HttpContext.Current.Session["UserId"].ToString();
            x = x.Replace("\"", "");
            int idx = Convert.ToInt32(x);
           
            BMSdbEntities entities = new BMSdbEntities();
            List<MaintanenceTable> maintainreq = entities.MaintanenceTables.Where(a => a.uid == idx).OrderBy(a => a.reqdate).ToList();

            //List<MaintanenceTable> maintainreq = (from c in entities.MaintanenceTables
            //                                      where c.uid == idx
            //                                      select c).ToList() ;
            if (maintainreq.Count > 0)
            {
                //maintainreq.Add(new MaintanenceTable());
                return View(maintainreq);
            }
            else
            {
                return View();
            }
           
         

           
        }
        public ActionResult History()
        {
            string x = System.Web.HttpContext.Current.Session["UserId"].ToString();
            x = x.Replace("\"", "");
            int idx = Convert.ToInt32(x);

            BMSdbEntities entities = new BMSdbEntities();
            List<MaintanenceTable> maintainreq = entities.MaintanenceTables.Where(a => a.uid == idx).OrderBy(a => a.reqdate).ToList();

            //List<MaintanenceTable> maintainreq = (from c in entities.MaintanenceTables
            //                                      where c.uid == idx
            //                                      select c).ToList() ;
            if (maintainreq.Count > 0)
            {
                //maintainreq.Add(new MaintanenceTable());
                return View(maintainreq);
            }
            else
            {
                return View();
            }




        }
        public ActionResult ShowAdmin()
        {
            BMSdbEntities entities = new BMSdbEntities();
            List<MaintanenceTable> maintainreq = entities.MaintanenceTables.ToList();
            if (maintainreq.Count == 0)
            {
                maintainreq.Add(new MaintanenceTable());
            }

            return View(maintainreq);



        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserId"] != null)
                base.OnActionExecuting(filterContext);
            else
                filterContext.Result = new RedirectResult("~/Login/Index");
        }
    }
}
