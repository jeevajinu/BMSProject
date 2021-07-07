using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace BMS.Models
{
    public class UserClass
    {
        [Required(ErrorMessage="Please enter the username")]
        [Display(Name="Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please enter the password")]
        [Display(Name = "Password")]
        public string password { get; set; }
      
        [Required(ErrorMessage = "Please enter the Re-password")]
        [Display(Name = "Confirm Pasword")]
        [Compare("password")]
        public string retypepassword { get; set; }
        [Required(ErrorMessage = "Please enter the name")]
        [Display(Name = "Name of the User")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the Email")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name="Admin")]
        public char Admin { get; set; }

    }
    public class Login
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
    public class NormalUser
    {
        [Required(ErrorMessage = "Enter request")]
        public string MaintainReq { get; set; }
        public DateTime Reqdate { get; set; }
        public string ReqUser { get; set; }
       
        public string reqstatus { get; set; }
        public string fileupload { get; set; }

    }
}