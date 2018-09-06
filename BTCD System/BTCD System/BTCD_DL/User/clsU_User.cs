using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using BTCD_System.Models;
using BTCD_System.BTCD_DL.Connection;

namespace BTCD_System.BTCD_DL.User
{
    public class clsU_User
    {
        #region Fields

        private string strquery = "";
        private SqlParameter[] p;
        private List<RecentActivityU> lstRecentActivity;
        private SqlDataReader reader;
        #endregion


        public static void WriteUserActivity(RecentActivityU emsRecentActivity)
        {

            SqlParameter[] p = new SqlParameter[7];

            try
            {
                p[0] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[0].Value = emsRecentActivity.TrnUser;
                p[1] = new SqlParameter("@LogType", SqlDbType.SmallInt);
                p[1].Value = emsRecentActivity.LogType;
                p[2] = new SqlParameter("@TransactionType", SqlDbType.SmallInt);
                p[2].Value = emsRecentActivity.TransactionType;
                p[3] = new SqlParameter("@RecordType", SqlDbType.SmallInt);
                p[3].Value = emsRecentActivity.RecordType;
                p[4] = new SqlParameter("@Description", SqlDbType.VarChar, 150);
                p[4].Value = emsRecentActivity.Description;
                p[5] = new SqlParameter("@ReferenceNo", SqlDbType.VarChar, 50);
                p[5].Value = emsRecentActivity.ReferenceNo;
                p[6] = new SqlParameter("@Remarks", SqlDbType.VarChar, 150);
                p[6].Value = emsRecentActivity.Remarks;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spInsertUserActivity", p);

            }
            catch (Exception ex)
            {

                throw;
            }


        }


        /// <summary>
        /// Get All user activity details where the logged username 
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public static List<RecentActivityU> GetUserActivity(string Username)
        {
            SqlParameter[] p = new SqlParameter[1];

            List<RecentActivityU> lstRecentActivity = new List<RecentActivityU>();

            if (string.IsNullOrEmpty(Username)) return null;

            try
            {
                p[0] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[0].Value = Username;

                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spGetAllDetailsRecentActivity", p))
                {

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstRecentActivity.Add(new RecentActivityU { ID = long.Parse(dr["ID"].ToString()), TrnUser = dr["TrnUser"].ToString(), TransactionType = short.Parse(dr["TransactionType"].ToString()), RecordType = short.Parse(dr["RecordType"].ToString()), LogType = short.Parse(dr["LogType"].ToString()), Description = dr["Description"].ToString(), ReferenceNo = dr["ReferenceNo"].ToString(), Remarks = dr["Remarks"].ToString() });
                        }
                    }

                    return lstRecentActivity;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


   
        public UserU GetUserByUsername(string Username)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[0].Value = Username;

                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spSelectUserFromUsername", p))
                {
                    UserU user = new UserU();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.Id = int.Parse(dr["ID"].ToString());
                            user.EmployeeCode = dr["EmployeeCode"].ToString();
                            user.EmployeeCategory = dr["EmployeeCategoryName"].ToString();
                            user.Username = dr["Username"].ToString();
                            user.NickName = dr["NickName"].ToString();
                            user.Password = dr["Password"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.TrnDate = DateTime.Parse(dr["TrnDate"].ToString());
                            user.TrnUser = dr["TrnUser"].ToString();
                            user.Active = bool.Parse(dr["Active"].ToString());
                        }
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[] GetUserRolesByUsername(string Username)
        {
            p = new SqlParameter[1];
            List<string> lstRoles = new List<string>();

            try
            {
                p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[0].Value = Username;
                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spSelectRolesFromUsername", p))
                {

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstRoles.Add(dr["RoleName"].ToString());
                        }

                    }
                    string[] arrRoles = lstRoles.ToArray();
                    return arrRoles;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<UserU> GetUserActiveAll(string username)
        {
            p = new SqlParameter[1];
            p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
            p[0].Value = username;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spUserSelectActiveAll",p))
            {
                List<UserU> userList = new List<UserU>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userList.Add(new UserU
                        {

                            Id = int.Parse(dr["Id"].ToString()),                     
                            Email = dr["Email"].ToString(),
                            EmployeeCode = dr["EmployeeCode"].ToString(),
                            Username = dr["Username"].ToString(),
                            FirstName = dr["FirstName"].ToString(),
                            NickName = dr["NickName"].ToString(),           
                            LastName = dr["LastName"].ToString(),
                            Password = dr["Password"].ToString(),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString(),
                            Active = bool.Parse(dr["Active"].ToString())
                        });
                    }
                }
                return userList;
            }
        }

        public string createUser(UserU emsUser)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[6];

            try
            {
                p[0] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar, 50);
                p[0].Value = emsUser.EmployeeCode;
                p[1] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[1].Value = emsUser.Username;
                p[2] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
                p[2].Value = emsUser.Password;        
                p[3] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[3].Value = idendity.User.Username;
                p[4] = new SqlParameter("@Active", SqlDbType.Bit, 0);
                p[4].Value = emsUser.Active;
                p[5] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,"spUserCrate", p);
                return p[5].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }


        public string changeUserPassword(int ID, UserU emsUser)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[4];

            try
            {
                p[0] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
                p[0].Value = emsUser.Password;
                p[1] = new SqlParameter("@ID", SqlDbType.Int, 0);
                p[1].Value = ID;
                p[2] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[2].Value = idendity.User.Username;
                p[3] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,"spUserChangePassword", p);
                return p[8].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public List<UserRoleTUview> getUserRoleByUserID(int UserID)
        {
            p=new SqlParameter[1];

            p[0]= new SqlParameter("@UserID",SqlDbType.Int,0);
            p[0].Value = UserID;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spUserSelectRolesByUserId", p))
            {
                List<UserRoleTUview> userRoleList = new List<UserRoleTUview>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userRoleList.Add(new UserRoleTUview
                        {
                            ID = int.Parse(dr["ID"].ToString()),
                            RoleID = dr["RoleID"].ToString(),
                            RoleName = dr["RoleName"].ToString(),
                            Username = dr["Username"].ToString(),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString(),
                        });
                    }
                }
                return userRoleList;
            }
        } 

        public UserU GetUserById(int ID)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@ID", SqlDbType.VarChar, 0);
                p[0].Value = ID;

                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spUserSelectActiveByID", p))
                {

                    UserU user = new UserU();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.Id = int.Parse(dr["ID"].ToString());
                            user.EmployeeCode = dr["EmployeeCode"].ToString();
                            user.EmployeeCategory = dr["EmployeeCategoryName"].ToString();
                            user.NickName = dr["NickName"].ToString();
                            user.Username = dr["Username"].ToString();
                            user.Password = dr["Password"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.TrnDate = DateTime.Parse(dr["TrnDate"].ToString());
                            user.TrnUser = dr["TrnUser"].ToString();
                            user.Active = bool.Parse(dr["Active"].ToString());
                        }
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserRoleU> getRolesNotAssignedForUser( string username)
        {
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
            p[0].Value = username;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spRolesSelectAllRolesNotAssignedForUser", p))
            {

                List<UserRoleU> userRoles = new List<UserRoleU>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userRoles.Add(new UserRoleU
                        {
                            ID = int.Parse(dr["ID"].ToString()),
                            RoleID = dr["RoleID"].ToString(),
                            RoleName = dr["RoleName"].ToString(),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString()
                        });
                    }
                }
                return userRoles;
            }
        }

        public string addUserRoleToUser(string RoleID,string username)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[4];

            try
            {
                p[0] = new SqlParameter("@RoleID", SqlDbType.VarChar, 50);
                p[0].Value = RoleID;
                p[1] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[1].Value = username;
                p[2] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[2].Value = idendity.User.Username;
                p[3] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,"spUserAddRole", p);
                return p[3].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string deleteUserRoleToUser(string RoleID, string username)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[4];

            try
            {
                p[0] = new SqlParameter("@RoleID", SqlDbType.VarChar, 50);
                p[0].Value = RoleID;
                p[1] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[1].Value = username;
                p[2] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[2].Value = idendity.User.Username;
                p[3] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,"spUserDeleteRole", p);
                return p[3].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }

}