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
            BMSdbEntities entities = new BMSdbEntities();
            List<MaintanenceTable> maintainreq = entities.MaintanenceTables.ToList();
            if (maintainreq.Count == 0)
            {
                maintainreq.Add(new MaintanenceTable());
            }

            return View(maintainreq);
         

           
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
    }
}
