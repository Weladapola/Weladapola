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
    public class ItemController : Controller
    {
        private clsM_Item clsM_Item = new clsM_Item();
        private clsT_Stock clsT_Stock = new clsT_Stock();

        private List<SelectListItem> ListItem;
        private List<CategoryM> lstCategory;

        private List<ItemM> lstItem;
        private List<LocationM> lstLocation;
        private List<ItemGradeM> lstItemGrade;
        private List<UnitofMeasurementM> lstUnitofMeasurement;

        private string StockNo = "";
        private string ErrorMsg = "";

        [Authorize(Roles = "Create-Stock")]
        public ActionResult Category()
        {
            lstCategory = new clsM_Category().GetAllCategories();

            return View(lstCategory);
        }

        [Authorize(Roles = "Create-Stock")]
        public ActionResult Details(int ID)
        {
            lstItem = new clsM_Item().GetItemsByCategories(ID);

            return View(lstItem);
        }

        [Authorize(Roles = "Create-Stock")]
        public ActionResult CreateStock(int ID)
        {
            StockM stock = new StockM();
            ItemM ItemM = clsM_Item.GetItemByItemId(ID);

            ViewBag.Location = getLocation();
            ViewBag.Grade = GetItemGrade(ID);
            ViewBag.UOM = getUOM();

            stock.ItemId = ItemM.ItemId;
            stock.ItemCode = ItemM.ItemCode;
            stock.ItemName = ItemM.ItemName;


            return View(stock);
        }


        [Authorize(Roles = "Create-Stock")]
        [HttpPost]
        [ActionName("CreateStock")]
        public ActionResult CreateStock_POST()
        {
            StockM stock = new StockM();

            TryUpdateModel(stock);

            if (ModelState.IsValid)
            {
                stock.EmployeeCode = commonFunctions.GetTransactionEmployeeCode();

                ErrorMsg = clsT_Stock.SaveStock(stock, out StockNo);


                if (!string.IsNullOrEmpty(ErrorMsg))
                {
                    TempData["Message"] = new MessageBox { CssClassName = ".alert-danger", Title = "Error!", Message = "Transaction was rollback.Try again.<br>" };

                    ViewBag.Location = getLocation();
                    ViewBag.Grade = GetItemGrade(stock.ItemId);
                    ViewBag.UOM = getUOM();

                    return View(stock);
                }
                else
                {

                    ViewBag.Location = getLocation();
                    ViewBag.Grade = GetItemGrade(stock.ItemId);
                    ViewBag.UOM = getUOM();

                    TempData["Message"] = new MessageBox { CssClassName = ".alert-success", Title = "Success!", Message = "Your Stock No: " + StockNo };

                    return RedirectToAction("CreateStock", stock.ItemId);
                }
            }

            return View();
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


        public ActionResult Cancel()
        {
            return RedirectToAction("Category", "Item");
        }
    }
}