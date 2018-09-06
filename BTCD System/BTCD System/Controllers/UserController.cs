using BTCD_System.BTCD_DL.Master;
using BTCD_System.BTCD_DL.User;
using BTCD_System.Common;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BTCD_System.Controllers
{
    public class UserController : Controller
    {
        private clsU_User clsuser = new clsU_User();
        private List<SelectListItem> ListItem;
        private List<AutoComplete> lstAutoComplete;

        // GET: User
        [Authorize(Roles = "User - List")]
        public ActionResult Index()
        {
            List<UserU> lstEmsUser = clsuser.GetUserActiveAll(commonFunctions.GetTransactionUser());
            return View(lstEmsUser);
        }

        // GET: User/Details/5
        [Authorize(Roles = "User - Details")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        [Authorize(Roles = "User - Create")]
        public ActionResult Create()
        {
            ViewBag.Employee = GetAllEmployee(commonFunctions.GetTransactionUser());

            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserU users)
        {
            try
            {
                clsuser.createUser(users);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: User/Edit/5
        [Authorize(Roles = "User - Edit")]
        public ActionResult Edit(int id)
        {
            UserU emsUserU = new UserU();
            emsUserU = clsuser.GetUserById(id);
            return View(emsUserU);
        }


        // GET: User/EditPassword/5
        [Authorize(Roles = "User - ChangePassword")]
        public ActionResult EditPassword(int id)
        {
            UserU emsUserU = new UserU();
            emsUserU = clsuser.GetUserById(id);
            return View(emsUserU);
        }

        // POST: User/EditPassword/5
        [HttpPost]
        public ActionResult EditPassword(int id, UserU users)
        {
            try
            {
                clsuser.changeUserPassword(id, users);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "User - User Roles")]
        public ActionResult UserRoles(int id, string username)
        {
            List<UserRoleTUview> UserRoles = new List<UserRoleTUview>();
            UserRoles = clsuser.getUserRoleByUserID(id);

            TempData["keyUser"] = username;
            TempData["userid"] = id;


            return View(UserRoles);


        }

        [Authorize(Roles = "User - User Roles")]
        public ActionResult AllRoles(string username)
        {
            List<UserRoleU> roles = clsuser.getRolesNotAssignedForUser(username);
            return PartialView(roles);

        }

        [Authorize(Roles = "User - User Roles Add")]
        public ActionResult AddUserRoles(string RoleID, string username, int userid)
        {
            try
            {
                clsuser.addUserRoleToUser(RoleID, username);

                return RedirectToAction("UserRoles", new { id = userid, username = username });
            }
            catch
            {
                return RedirectToAction("UserRoles", new { id = userid, username = username });
            }
        }


        [Authorize(Roles = "User - User Roles Delete")]
        public ActionResult DeleteUserRoles(string RoleID, string username, int userid)
        {
            try
            {
                clsuser.deleteUserRoleToUser(RoleID, username);

                return RedirectToAction("UserRoles", new { id = userid, username = username });
            }
            catch
            {
                return RedirectToAction("UserRoles", new { id = userid, username = username });
            }
        }
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        private List<SelectListItem> GetAllEmployee(string username,string SelectedItem = null, string DisabledItem = null)
        {
            ListItem = new List<SelectListItem>();

            lstAutoComplete = new clsM_Employee().GetEmployee(username);

            foreach (AutoComplete Employee in lstAutoComplete)
            {
                ListItem.Add(new SelectListItem { Value = Employee.value.ToString(), Text = Employee.text, Selected = Employee.value.ToString() == SelectedItem ? true : false, Disabled = Employee.value.ToString() == DisabledItem ? true : false });

            }

            return ListItem;
        }
    }
}
