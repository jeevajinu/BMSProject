using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BMS.Models;

namespace BMS.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            BMSdbEntities entities = new BMSdbEntities();
            return View(from MaintanenceTable in entities.MaintanenceTables.Take(10)
                        select MaintanenceTable);
        }
    }
}