using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using BTCD_System.BTCD_DL.User;

namespace BTCD_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest()
        {
            HttpCookie authoCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authoCookies != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authoCookies.Value);

                if (ticket != null && !ticket.Expired)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    UserU user = js.Deserialize<UserU>(ticket.UserData);
                    clsIdentity clsIdentity = new clsIdentity(user);
                    clsPrincipal clsPrincipal = new clsPrincipal(clsIdentity);
                    HttpContext.Current.User = clsPrincipal;
                }
            }
        }
    }
}
