using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace BTCD_System.BTCD_DL.User
{
    public class clsPrincipal : IPrincipal
    {
        private readonly clsIdentity clsIdentity;

        public clsPrincipal(clsIdentity _clsIdentity)
        {
            clsIdentity = _clsIdentity;
        }

        public IIdentity Identity
        {
            get
            {
                return clsIdentity;
            }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}