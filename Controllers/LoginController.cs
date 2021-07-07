using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using BMS.Models;

namespace BMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
       
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Index(Login lc)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44375/api/User/Logapi");
                var responseTask =hc.GetAsync("Logapi/"+lc.UserName+"/"+lc.Password);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    if (lc.UserName.ToUpper() == "ADMIN")
                    {
                        Session["UserId"] = "3";
                        return RedirectToAction("ShowAdmin", "Home");
                    }
                    else
                    {
                        Session["UserId"] = "3";
                        return RedirectToAction("Show", "Home");
                    }
                }


            }
            return View();
           

        }


    }
}