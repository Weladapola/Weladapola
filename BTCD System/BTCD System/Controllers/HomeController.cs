using BTCD_System.BTCD_DL.Master;
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
    public class HomeController : Controller
    {


  
       [Authorize]
        public ActionResult Index()
        {
          

            return View();
        }



        [Authorize]
        public ActionResult Lock()
        {
            return View();
        }


 
    }
}