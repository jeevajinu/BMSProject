using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BMS.Models;
using System.Net.Http;

namespace BMS.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserClass uc) 
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44375/api/User");
                var insertreg = hc.PostAsJsonAsync<UserClass>("User", uc);
                insertreg.Wait();
                var savereg = insertreg.Result;
                if (savereg.IsSuccessStatusCode)
                {
                    ViewBag.message = savereg.Content.ReadAsStringAsync().Result;
                    ModelState.Clear();
                    //ViewBag.message = "The user " + uc.username + " created successfully";
                }
                else
                {
                    ViewBag.message = savereg.Content.ReadAsStringAsync().Result;
                }
            }
            
            return View();
            
        }
    }
}