using BTCD_System.BTCD_DL.User;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace BTCD_System.Controllers
{
    public class AccountController : Controller
    {
        private clsU_User clsU_User = new clsU_User();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogIn login, string returnUrl = "")
        {


            if (ModelState.IsValid)
            {
                bool isValidUser = Membership.ValidateUser(login.Username, login.Password);
                if (isValidUser)
                {
                    UserU UserU = null;
                    UserU = clsU_User.GetUserByUsername(login.Username);

                    if (User != null)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string data = js.Serialize(UserU);

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UserU.Username, DateTime.Now, DateTime.Now.AddMinutes(30), false, data);
                        string encToken = FormsAuthentication.Encrypt(ticket);

                        HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                        Response.Cookies.Add(authCookies);
                        //return Redirect(returnUrl);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }


                }
            }

            return View();
        }



        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}