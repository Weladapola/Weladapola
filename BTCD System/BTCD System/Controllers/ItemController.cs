using BTCD_System.BTCD_DL.Master;
using BTCD_System.BTCD_DL.Transaction;
using BTCD_System.Common;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<CategoryM> lstCategories;

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

        [Authorize(Roles = "Add-Item")]
        public ActionResult DetailsGrid()
        {
            ViewBag.ItemCategory = GetCategories();

            return View();
        }

        #region Comment
        //[HttpPost]
        //public ActionResult AddItem()
        //{
        //    HttpFileCollectionBase files = Request.Files;

        //    for (int i = 0; i < files.Count; i++)
        //    {
        //        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
        //        //string filename = Path.GetFileName(Request.Files[i].FileName);  

        //        HttpPostedFileBase file = files[i];
        //        string fname;

        //        // Checking for Internet Explorer  
        //        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
        //        {
        //            string[] testfiles = file.FileName.Split(new char[] { '\\' });
        //            fname = testfiles[testfiles.Length - 1];
        //        }
        //        else
        //        {
        //            fname = file.FileName;
        //        }

        //        // Get the complete folder path and store the file inside it.  
        //        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
        //        file.SaveAs(fname);
        //    }
        //        //string msg = clsM_Item.CreateItem(ItemCategory, ItemCode, ItemName, ItemDescription, ItemSinhalaDescription, ItemTamilDescription);

        //        //if (!string.IsNullOrEmpty(msg))
        //        //{
        //        //    TempData["Message"] = new MessageBox { CssClassName = "alert-danger", Title = "Error!", Message = msg };
        //        //}
        //        //else
        //        //{                
        //        //    TempData["Message"] = new MessageBox { CssClassName = "alert-success", Title = "Success!", Message = "Item Created. Code: " + ItemCode };
        //        //}
        //        return Content("msg");
        //}

        #endregion

        [HttpPost]
        public ActionResult AddItem(ItemM item)
        {
            string fileName = "";
            string extension = "";
            string fName = "";
            if (item.ImageUpload != null)
                {
                 fileName = Path.GetFileNameWithoutExtension(item.ImageUpload.FileName);
                 extension = Path.GetExtension(item.ImageUpload.FileName);
                 fName = item.ItemName + extension;
                item.ImageUrl = @"~\Content\Images\Vegetables\" + fName;
            }
            else
            {
                item.ImageUrl = "";
            }

            if (item.TamilDescription == null)
            {
                item.TamilDescription = "";
            }

                string guid = Guid.NewGuid().ToString();     

            string msg = clsM_Item.CreateItem(item.CategoryId, guid, item.ItemName, item.Description, item.SinghalaDescription, item.TamilDescription,item.ImageUrl);

            if (!string.IsNullOrEmpty(msg))
            {
                TempData["Message"] = new MessageBox { CssClassName = "alert-danger", Title = "Error!", Message = msg };
            }
            else
            {
                try
                {
                    if(System.IO.File.Exists(item.ImageUrl))
                    {
                        System.IO.File.Delete(item.ImageUrl);
                    }

                    if (item.ImageUrl != "")
                    {
                        item.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/Vegetables"), fName));
                    }
                    TempData["Message"] = new MessageBox { CssClassName = "alert-success", Title = "Success!", Message = "Item Created. Name: " + item.ItemName };
                }
                catch(Exception ex)
                {
                    TempData["Message"] = new MessageBox { CssClassName = "alert-danger", Title = "Error!", Message = ex.ToString() };
                }
            }
            
            return Content(msg);
        }

        [Authorize(Roles = "Item - List")]
        public ActionResult ItemDetails()
        {
            ViewBag.ItemCategory = GetCategories();

            return View();
        }        

        [HttpPost]
        public ActionResult ItemDetails(int _CategoryId)
        {
            List<ItemM> item = new List<ItemM>();
            item = clsM_Item.GetItemsByCategories(_CategoryId);

            return PartialView("_PartialItemDetailGrid", item);
        }

        [HttpPost]
        public ActionResult ItemDelete(int _CategoryId, string _ItemID)
        {
            string msg = clsM_Item.DeleteItem(int.Parse(_ItemID));

            List<ItemM> item = new List<ItemM>();
            item = clsM_Item.GetItemsByCategories(_CategoryId);

            return PartialView("_PartialItemDetailGrid", item);
        }


        [NonAction]
        private List<SelectListItem> GetCategories()
        {
            ListItem = new List<SelectListItem>();
            lstCategories = new clsM_Category().GetAllCategories();

            foreach (CategoryM Category in lstCategories)
            {
                ListItem.Add(new SelectListItem { Value = Category.CategoryId.ToString(), Text = Category.CategoryName });

            }

            return ListItem;
        }

        [Authorize(Roles = "Create-Stock")]
        [HttpPost]
        [ActionName("Details")]
        public ActionResult DetailsByItemSinghlaName(string SinghalaName, int laLanguageId)
        {
            lstItem = new clsM_Item().GetItemsBySinghalaName(SinghalaName, laLanguageId);

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

                    TempData["Message"] = new MessageBox { CssClassName = ".alert-success", Title = "Success!", Message = "Your Last Stock Nos: " + StockNo };

                    return RedirectToAction("CreateStock", stock.ItemId);
                }
            }

            return View();
        }


        [Authorize(Roles = "Create-Stock")]
        [HttpPost]
        public ActionResult UpdateStock_POST()
        {
            StockM stock = new StockM();

            TryUpdateModel(stock);

            if (ModelState.IsValid)
            {
                stock.EmployeeCode = commonFunctions.GetTransactionEmployeeCode();

                ErrorMsg = clsT_Stock.UpdateStock(stock, out StockNo);


                if (!string.IsNullOrEmpty(ErrorMsg))
                {
                    TempData["Message"] = new MessageBox { CssClassName = ".alert-danger", Title = "Error!", Message = "Your Stock No: " + StockNo + "-" + ErrorMsg };

                    ViewBag.Location = getLocation();
                    ViewBag.Grade = GetItemGrade(stock.ItemId);
                    ViewBag.UOM = getUOM();

                    return View("EditStock");
                }
                else
                {

                    ViewBag.Location = getLocation();
                    ViewBag.Grade = GetItemGrade(stock.ItemId);
                    ViewBag.UOM = getUOM();

                    TempData["Message"] = new MessageBox { CssClassName = ".alert-success", Title = "Success!", Message = "Your Stock No: " + StockNo + "- Successfully Updated" };
                    return View("EditStock");
                    //return RedirectToAction("UpdateStock", stock.ItemId);
                }
            }

            return View();
        }

        [Authorize(Roles = "Create-Stock")]
        [HttpPost]

        public ActionResult EditStock(string stockId)
        {

            StockM stock = new StockM();

            if (stockId != string.Empty)
            {
                stock = clsT_Stock.GetStockDetailByStockId(int.Parse(stockId));
            }

            ViewBag.Location = getLocation();
            ViewBag.Grade = GetItemGrade(stock.ItemId);
            ViewBag.UOM = getUOM();

            return View(stock);
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


        [HttpPost]
        public ActionResult ItemEdit(int ItemID)
        {
            ItemM ItemM = clsM_Item.GetItemByItemId(ItemID);
            return View(ItemM);
        }

        [HttpPost]
        public ActionResult ItemEditByID(int _ItemID,string _ItemName,string _Description,string _SinghalaDescription,string _TamilDescription)
        {
            var msg = clsM_Item.UpdateItemById(_ItemID, _ItemName, _Description, _SinghalaDescription, _TamilDescription,"a");

            //msg = "Item: " + _ItemName + "- Successfully Updated";
            TempData["Message"] = new MessageBox { CssClassName = ".alert-success", Title = "Success!", Message = "Item: " + _ItemName + "- Successfully Updated" };
            return Content(msg);
        }

        [HttpPost]
        public ActionResult UpdateImage(ItemM item)
        {
            string fileName = Path.GetFileNameWithoutExtension(item.ImageUpload.FileName);
            string extension = Path.GetExtension(item.ImageUpload.FileName);
            string fName = item.ItemName + extension;
            item.ImageUrl = @"~\Content\Images\Vegetables\" + fName;

            string msg = clsM_Item.UpdateItemImageUrlId(item.ItemId,item.ImageUrl);
            if (msg== "Item Not Updated")
            {
                TempData["Message"] = new MessageBox { CssClassName = "alert-danger", Title = "Error!", Message = msg };
            }
            else
            {
                try
                {
                    if (System.IO.File.Exists(item.ImageUrl))
                    {
                        System.IO.File.Delete(item.ImageUrl);
                    }

                    item.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/Vegetables"), fName));
                    TempData["Message"] = new MessageBox { CssClassName = "alert-success", Title = "Success!", Message = "Item Created. Name: " + item.ItemName };
                }
                catch (Exception ex)
                {
                    TempData["Message"] = new MessageBox { CssClassName = "alert-danger", Title = "Error!", Message = ex.ToString() };
                    msg = "Not updated";
                }
            }

            return Content(msg);
        }
    }
}