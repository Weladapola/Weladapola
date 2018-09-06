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
    public class CustomerRequirmentController : Controller
    {

        private List<SelectListItem> ListItem;

        private List<CustomerM> lstCustomer;
        private List<CategoryM> lstCategories;
        private List<ItemGradeM> lstItemGrade;
        private List<UnitofMeasurementM> lstUnitofMeasurement;

        private List<AutoComplete> lstAutoComplete;


        private string RequestNo = "";
        private string ErrorMsg = "";

        [Authorize(Roles = "Requrement-Create")]
        public ActionResult Create()
        {
            List<CustomerRequirment> lstCustomerRequirment = new List<CustomerRequirment>();


            ViewBag.Category = GetCategories();
            ViewBag.Customer = GetCustomer();
            ViewBag.UOM = getUOM();
       

            return View(lstCustomerRequirment);
        }

        [Authorize(Roles = "Requrement-Create")]
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            CustomerRequirment CustomerRequirment = new CustomerRequirment();
            List<CustomerRequirment> lstCustomerRequirment = new List<CustomerRequirment>();

            if (TempData["lstCustomerRequirment"] == null)
                TempData["lstCustomerRequirment"] = lstCustomerRequirment;
            else
                lstCustomerRequirment = TempData["lstCustomerRequirment"] as List<CustomerRequirment>;

            TryUpdateModel(CustomerRequirment);

            if (lstCustomerRequirment.Count > 0)
            {

                //Update customer and create date 
                lstCustomerRequirment.ForEach(e => e.CustomerId = 1);
                lstCustomerRequirment.ForEach(e => e.CreatedBy = commonFunctions.GetTransactionEmployeeCode());

                ErrorMsg = new clsT_CustomerRequirment().SaveCustomerRequirment(lstCustomerRequirment, out RequestNo);


                //Error Found
                if (!string.IsNullOrEmpty(ErrorMsg))
                {
                    TempData["Message"] = new MessageBox { CssClassName = ".alert-danger", Title = "Error!", Message = "Transaction was rollback.Try again." };

                    ViewBag.Category = GetCategories();
                    ViewBag.Customer = GetCustomer();
                    ViewBag.UOM = getUOM();

                    TempData["lstCustomerRequirment"] = lstCustomerRequirment;

                    return View(lstCustomerRequirment);
                }
                else
                {
                    
                    TempData["lstCustomerRequirment"] = null;

                    TempData["Message"] = new MessageBox { CssClassName = ".alert-success", Title = "Success!", Message = "Your Customer Requirement No: " + RequestNo };

                    return RedirectToAction("Create");
                }


            }
            else
            {

                ViewBag.Category = GetCategories();
                ViewBag.Customer = GetCustomer();
                ViewBag.UOM = getUOM();

                TempData["Message"] = new MessageBox { CssClassName = ".alert-warning", Title = "Warning!", Message = "Request details not found." };

                TempData["lstCustomerRequirment"] = lstCustomerRequirment;

                return View(lstCustomerRequirment);
            }
        }



        public ActionResult PartialRequiredItem()
        {
            CustomerRequirment CustomerRequirment = new CustomerRequirment();
            List<CustomerRequirment> lstCustomerRequirment = new List<CustomerRequirment>();

            TryUpdateModel(CustomerRequirment);

            if (TempData["lstCustomerRequirment"] == null)
                TempData["lstCustomerRequirment"] = lstCustomerRequirment;
            else
                lstCustomerRequirment = TempData["lstCustomerRequirment"] as List<CustomerRequirment>;


            if (ModelState.IsValid)
            {

                if (!lstCustomerRequirment.Exists(e => e.ItemId == CustomerRequirment.ItemId))
                {

                    CustomerRequirment.ItemName = new clsM_Item().GetItemByItemId(CustomerRequirment.ItemId).ItemName;
                    CustomerRequirment.GradeDesc = new clsM_Grade().GetItemGradeByGradeId(CustomerRequirment.GradeId).GradeDescription;
                    CustomerRequirment.UOMDesc = new clsM_UOM().GetUnitofMeasurementFromId(CustomerRequirment.UOMId).UOMName;

                    lstCustomerRequirment.Add(CustomerRequirment);
                }
                else
                    TempData["Message"] = new MessageBox { CssClassName = ".alert-warning", Title = "Warning!", Message = "This item exist in the list." };

            }
            else
                TempData["Message"] = new MessageBox { CssClassName = ".alert-warning", Title = "Warning!", Message = "Missing some details." };


            int Row = 0;
            lstCustomerRequirment.ForEach(e => e.RequirementId = (Row = Row + 1));

       
            TempData["lstCustomerRequirment"] = lstCustomerRequirment;

            return PartialView("_PartialItemDetails", lstCustomerRequirment);

        }


        [HttpPost]
        public ActionResult PartialEditQty(int? ItemId, int Qty)
        {

            List<CustomerRequirment> lstCustomerRequirment = new List<CustomerRequirment>();

            if (TempData["lstCustomerRequirment"] == null)
                TempData["lstCustomerRequirment"] = lstCustomerRequirment;
            else
                lstCustomerRequirment = TempData["lstCustomerRequirment"] as List<CustomerRequirment>;


            if (ItemId != null && Qty > 0)
            {
                if(lstCustomerRequirment.Count > 0)
                    lstCustomerRequirment.Where(e => e.ItemId == ItemId).ToList().ForEach(e => e.RequiredQty = Qty);      
            }
            else
                TempData["Message"] = new MessageBox { CssClassName = ".alert-warning", Title = "Warning!", Message = "Invalid requested qty." };


            int Row = 0;
            lstCustomerRequirment.ForEach(e => e.RequirementId = (Row = Row + 1));

            TempData["lstCustomerRequirment"] = lstCustomerRequirment;

            return PartialView("_PartialItemDetails", lstCustomerRequirment);
        }



        [HttpPost]
        public ActionResult PartialEditPrice(int? ItemId, decimal Price)
        {

            List<CustomerRequirment> lstCustomerRequirment = new List<CustomerRequirment>();

            if (TempData["lstCustomerRequirment"] == null)
                TempData["lstCustomerRequirment"] = lstCustomerRequirment;
            else
                lstCustomerRequirment = TempData["lstCustomerRequirment"] as List<CustomerRequirment>;


            if (ItemId != null && Price > 0)
            {
                if (lstCustomerRequirment.Count > 0)
                    lstCustomerRequirment.Where(e => e.ItemId == ItemId).ToList().ForEach(e => e.RequiredPrice = Price);
            }
            else
                TempData["Message"] = new MessageBox { CssClassName = ".alert-warning", Title = "Warning!", Message = "Invalid requested price." };


            int Row = 0;
            lstCustomerRequirment.ForEach(e => e.RequirementId = (Row = Row + 1));

            TempData["lstCustomerRequirment"] = lstCustomerRequirment;

            return PartialView("_PartialItemDetails", lstCustomerRequirment);
        }


        [HttpPost]
        public ActionResult PartialDeleteItem(int ItemId)
        {
            List<CustomerRequirment> lstCustomerRequirment = new List<CustomerRequirment>();

            if (TempData["lstCustomerRequirment"] == null)
                TempData["lstCustomerRequirment"] = lstCustomerRequirment;
            else
                lstCustomerRequirment = TempData["lstCustomerRequirment"] as List<CustomerRequirment>;


            if (ItemId != 0)
            {

                if (lstCustomerRequirment != null)
                {
                    lstCustomerRequirment.Remove(lstCustomerRequirment.SingleOrDefault(e => e.ItemId == ItemId));
                }
            }

            int Row = 0;
            lstCustomerRequirment.ForEach(e => e.RequirementId = (Row = Row + 1));

            TempData["lstCustomerRequirment"] = lstCustomerRequirment;

            return PartialView("_PartialItemDetails", lstCustomerRequirment);
        }



        [NonAction]
        private List<SelectListItem> GetCustomer()
        {
            ListItem = new List<SelectListItem>();
            lstCustomer = new clsM_Customer().GetAllCustomers();

            foreach (CustomerM Customer in lstCustomer)
            {
                ListItem.Add(new SelectListItem { Value = Customer.CustomerId.ToString(), Text = Customer.CustomerName });

            }

            return ListItem;
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
        public JsonResult getItemsFromCategory(int CategoryId)
        {
            lstAutoComplete = new clsM_Item().GetItemsByCateroryId(CategoryId);

            return Json(lstAutoComplete);
        }


        [HttpPost]
        public JsonResult getGradeFromItem(int ItemId)
        {
            lstAutoComplete = new clsM_Grade().GetItemGradeFromItemId(ItemId);

            return Json(lstAutoComplete);
        }

        public ActionResult ViewCustomerRequirments()
        {
            List<CustomerRequirment> lstCustomerRequirment = new List<CustomerRequirment>();         
            lstCustomerRequirment = new clsT_CustomerRequirment().GetCustomerRequirmentByCreatedUser(commonFunctions.GetTransactionEmployeeCode());
            return View(lstCustomerRequirment);
        }

    }
}