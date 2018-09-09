using BTCD_System.BTCD_DL.Transaction;
using BTCD_System.Common;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTCD_System.Controllers
{
    public class StockController : Controller
    {
        private clsT_Stock clsT_Stock = new clsT_Stock();
        private List<StockM> lstStock;

        [Authorize(Roles = "View-My-Stock")]
        public ActionResult ViewMyStock()
        {
            lstStock = new clsT_Stock().GetStockByUserCode(commonFunctions.GetTransactionEmployeeCode());

            return View(lstStock);
        }

      

        [Authorize(Roles = "View-All-Stock")]
        public ActionResult ViewStock()
        {
            lstStock = new clsT_Stock().GetStock();

            return View(lstStock);
        }

        [Authorize(Roles = "View-My-Stock")]
        public ActionResult ViewPaymentConfirm()
        {
            lstStock = new clsT_Stock().GetStockByUserCode(commonFunctions.GetTransactionEmployeeCode());

            return View(lstStock);
        }

        

    }
}