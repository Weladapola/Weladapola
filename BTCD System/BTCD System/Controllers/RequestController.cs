using BTCD_System.BTCD_DL.Master;
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
    public class RequestController : Controller
    {
        private clsT_Stock clsT_Stock = new clsT_Stock();
        private clsT_Bids clsT_Bids = new clsT_Bids();
        private List<StockM> lstStock;
        private List<RequestT> lstRequest;
        private StockM Stock;
        private BidsM Bids;

        private List<SelectListItem> ListItem;
        private List<LocationM> lstLocation;
        private List<ItemGradeM> lstItemGrade;
        private List<UnitofMeasurementM> lstUnitofMeasurement;


        private string RequestNo = "";
        private string ErrorMsg = "";

        // GET: Request
        [Authorize(Roles = "Create-Request")]
        [HttpPost]
        public ActionResult Create(int StockId)
        {
            Stock = new clsT_Stock().GetStockDetailByStockId(StockId);
            Bids = new BidsM { StockId = Stock.StockId, ItemId = Stock.ItemId, ItemCode = Stock.ItemCode, ItemName = Stock.ItemName, LocationId = Stock.LocationId, GradeId = Stock.GradeId, UOMId = Stock.UOMId, Quantity = Stock.Quantity, UnitPrice = Stock.UnitPrice };

            ViewBag.Location = getLocation();
            ViewBag.Grade = GetItemGrade(Stock.ItemId);
            ViewBag.UOM = getUOM();

            return View(Bids);
        }


        [Authorize(Roles = "Create-Request")]
        [HttpPost]
        public ActionResult SaveRequest()
        {
            BidsM Bids = new BidsM();
            Bids.RequestedBy = commonFunctions.GetTransactionEmployeeCode();
            TryUpdateModel(Bids);
            
            if (ModelState.IsValid)
            {
               

                ErrorMsg = clsT_Bids.SaveRequest(Bids, out RequestNo);


                if (!string.IsNullOrEmpty(ErrorMsg))
                {
                    TempData["Message"] = new MessageBox { CssClassName = ".alert-danger", Title = "Error!", Message = "Transaction was rollback.Try again." };

                    ViewBag.Location = getLocation();
                    ViewBag.Grade = GetItemGrade(Bids.ItemId);
                    ViewBag.UOM = getUOM();

                    return RedirectToAction("Create", Bids.StockId);
                }
                else
                {

                    ViewBag.Location = getLocation();
                    ViewBag.Grade = GetItemGrade(Bids.StockId);
                    ViewBag.UOM = getUOM();

                    TempData["Message"] = new MessageBox { CssClassName = ".alert-success", Title = "Success!", Message = "Your Request No: " + RequestNo };

                    return RedirectToAction("ViewStock", "Stock");
                }
            }

            return View();

        }


        [Authorize(Roles = "View-Request")]
        [HttpPost]
        public ActionResult ViewRequest(int StockId)
        {
            lstRequest = new List<RequestT>();

            if (StockId != 0)
            {
                lstRequest = clsT_Bids.ViewRequest(StockId);
            }

            return View(lstRequest);
        }

        [Authorize(Roles = "Payment - Confirm Payment")]
        //[HttpPost]
        public ActionResult ViewPendingPayment()
        {
            lstRequest = new List<RequestT>();

            
            lstRequest = clsT_Bids.GetPendingPaymentAll();
            

            return View(lstRequest);
        }


        [NonAction]
        private List<SelectListItem> getLocation(string SelectedItem = null, string DisabledItem = null)
        {
            ListItem = new List<SelectListItem>();
            lstLocation = new clsM_Location().GetAllLocations();

            foreach (LocationM Location in lstLocation)
            {
                ListItem.Add(new SelectListItem { Value = Location.LocationId.ToString(), Text = Location.LocationName, Selected = Location.LocationId.ToString() == SelectedItem ? true : false, Disabled = Location.LocationId.ToString() == DisabledItem ? true : false });

            }

            return ListItem;
        }


        [NonAction]
        private List<SelectListItem> GetItemGrade(int ItemId)
        {
            ListItem = new List<SelectListItem>();
            lstItemGrade = new clsM_Grade().GetItemGradeByItemId(ItemId);

            foreach (ItemGradeM ItemGrade in lstItemGrade)
            {
                ListItem.Add(new SelectListItem { Value = ItemGrade.GradeId.ToString(), Text = ItemGrade.GradeDescription });

            }

            return ListItem;
        }

        [NonAction]
        private List<SelectListItem> getUOM(string SelectedItem = null, string DisabledItem = null)
        {
            ListItem = new List<SelectListItem>();
            lstUnitofMeasurement = new clsM_UOM().GetAllUnitofMeasurement();

            foreach (UnitofMeasurementM UnitofMeasurement in lstUnitofMeasurement)
            {
                ListItem.Add(new SelectListItem { Value = UnitofMeasurement.UOMId.ToString(), Text = UnitofMeasurement.UOMName, Selected = UnitofMeasurement.UOMId.ToString() == SelectedItem ? true : false, Disabled = UnitofMeasurement.UOMId.ToString() == DisabledItem ? true : false });

            }

            return ListItem;
        }



        [HttpPost]
        public ActionResult Accept(int RequirementId,int StockId)
        {
            if (RequirementId != 0)
            {
                clsT_Bids.ApproveRequest(RequirementId);
            }

            return RedirectToAction("ViewMyStock", "Stock");
        }

        [HttpPost]
        public ActionResult ConfirmPayment(int RequirementId, int StockId)
        {
            if (RequirementId != 0)
            {
                clsT_Bids.ConfirmPayment(RequirementId);
            }

            return RedirectToAction("ViewPendingPayment", "Request");
        }



        [HttpPost]
        public ActionResult Reject(int RequirementId, int StockId)
        {
            if (RequirementId != 0)
            {
                clsT_Bids.RejectRequest(RequirementId);
            }

            return RedirectToAction("ViewMyStock", "Stock");
        }
    }
}