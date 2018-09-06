using BTCD_System.BTCD_DL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;


namespace BTCD_System
{
    public class clsRoleProvider : RoleProvider
    {
        clsU_User clsU_User = new clsU_User();
        private int _cacheTimeoutInMinute = 20;

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            //var cacheKey = string.Format("{0_role}", username);
            var cacheKey = username;
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return (string[]) HttpRuntime.Cache[cacheKey];
            }

            string[] roles = new string[] {};

            roles = clsU_User.GetUserRolesByUsername(username);

            if (roles.Count() > 0)
            {
                HttpRuntime.Cache.Insert(cacheKey,roles,null,DateTime.Now.AddMinutes(_cacheTimeoutInMinute),Cache.NoSlidingExpiration);
            }

            return roles;

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}