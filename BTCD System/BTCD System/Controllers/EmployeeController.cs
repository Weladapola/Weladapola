using BTCD_System.BTCD_DL.Master;
using BTCD_System.Common;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTCD_System.Controllers
{
    public class EmployeeController : Controller
    {
        private List<SelectListItem> ListItem;
        private List<BankM> lstBank;
        private List<EmployeeCategory> lstEmployeeCategory;
        private List<BankBranchM> lstBankBranch;
        private List<AutoComplete> lstAutoComplete;

        private clsM_Employee clsM_Employee = new clsM_Employee();

        // GET: Employee
        [Authorize(Roles = "Create-Employee")]
        public ActionResult Create()
        {
            ViewBag.Bank = getBank();
            ViewBag.EmployeeCategory = getEmployeeCategory();
            return View();
        }

    
        [Authorize(Roles = "Create-Employee")]
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Post()
        {
            EmployeeM Employee = new EmployeeM();

            TryUpdateModel(Employee);

            if (ModelState.IsValid)
            {
                string ErrMsg = clsM_Employee.SaveEmployee(Employee,commonFunctions.GetTransactionUser());

                if (ErrMsg == string.Empty)
                {
                   return RedirectToAction("Create");
                }
            }

            ViewBag.EmployeeCategory = getEmployeeCategory();
            ViewBag.Bank = getBank();

            return View(Employee);
        }


        [NonAction]
        private List<SelectListItem> getBank(string SelectedItem = null, string DisabledItem = null)
        {
            ListItem = new List<SelectListItem>();
            lstBank = new clsM_Bank().GetAllBanks();

            foreach (BankM Bank in lstBank)
            {
                ListItem.Add(new SelectListItem { Value = Bank.BankCode.ToString(), Text = Bank.BankName, Selected = Bank.BankCode.ToString() == SelectedItem ? true : false, Disabled = Bank.BankCode.ToString() == DisabledItem ? true : false });

            }

            return ListItem;
        }


        private List<SelectListItem> getEmployeeCategory(string SelectedItem = null, string DisabledItem = null)
        {
            ListItem = new List<SelectListItem>();
            lstEmployeeCategory = new clsM_Employee().GetCategory();

            foreach (EmployeeCategory EmployeeCategory in lstEmployeeCategory)
            {
                ListItem.Add(new SelectListItem { Value = EmployeeCategory.EmployeeCategoryCode.ToString(), Text = EmployeeCategory.EmployeeCategoryName, Selected = EmployeeCategory.EmployeeCategoryCode.ToString() == SelectedItem ? true : false, Disabled = EmployeeCategory.EmployeeCategoryCode.ToString() == DisabledItem ? true : false });

            }

            return ListItem;
        }

        [HttpPost]
        public JsonResult getBankBranch(string BankCode)
        {
            lstAutoComplete = new clsM_Bank().GetBranchInBank(BankCode);

            return Json(lstAutoComplete);
        }
    }
}