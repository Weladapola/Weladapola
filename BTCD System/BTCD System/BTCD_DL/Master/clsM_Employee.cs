using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Common;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BTCD_System.BTCD_DL.Master
{
    public class clsM_Employee
    {
        #region Fields

        private SqlParameter[] p;
        private List<EmployeeM> lstCustomer;
        private SqlDataReader reader;

        #endregion

        #region Methods
        public string SaveEmployee(EmployeeM Employee,string username)
        {
            p = new SqlParameter[17];

            p[0] = new SqlParameter("@EmployeeCategory", SqlDbType.VarChar) { Value = Employee.EmployeeCategory };
            p[1] = new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = Employee.FirstName };
            p[2] = new SqlParameter("@LastName", SqlDbType.VarChar) { Value = Employee.LastName };
            p[3] = new SqlParameter("@NickName", SqlDbType.VarChar) { Value = Employee.NickName };
            p[4] = new SqlParameter("@Address1", SqlDbType.VarChar) { Value = Employee.Address1 };
            p[5] = new SqlParameter("@Address2", SqlDbType.VarChar) { Value = Employee.Address2 };
            p[6] = new SqlParameter("@Address3", SqlDbType.VarChar) { Value = Employee.Address3 };
            p[7] = new SqlParameter("@Telephone1", SqlDbType.VarChar) { Value = Employee.Telephone1 };
            p[8] = new SqlParameter("@Telephone2", SqlDbType.VarChar) { Value = Employee.Telephone2 };
            p[9] = new SqlParameter("@BankCode", SqlDbType.VarChar) { Value = Employee.BankCode };
            p[10] = new SqlParameter("@BranchCode", SqlDbType.VarChar) { Value = Employee.BranchCode };
            p[11] = new SqlParameter("@AccountNo", SqlDbType.VarChar) { Value = Employee.AccountNo };         
            p[12] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = Employee.Email };
            p[13] = new SqlParameter("@NICNo", SqlDbType.VarChar) { Value = Employee.NICNo };
            p[14] = new SqlParameter("@Dob", SqlDbType.Date) { Value = Employee.Dob };
            p[15] = new SqlParameter("@Username", SqlDbType.VarChar) { Value = username };
            p[16] = new SqlParameter("@ERRMSG", SqlDbType.VarChar, 400);
            p[16].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spInsertEmployee", p);

            return p[16].Value.ToString();
        }

        public List<EmployeeCategory> GetCategory()
        {
            List<EmployeeCategory> lstEmployeeCategory = new List<EmployeeCategory>();

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectEmployeeCategory"))
            {
                while (reader.Read())
                {
                    lstEmployeeCategory.Add(new EmployeeCategory
                    {
                        EmployeeCategoryCode = reader["EmployeeCategoryCode"].ToString(),
                        EmployeeCategoryName = reader["EmployeeCategoryName"].ToString()
                    });
                }

                return lstEmployeeCategory;
            }
        }

        public List<AutoComplete> GetEmployee(string username)
        {
            List<AutoComplete> lstAutoComplete = new List<AutoComplete>();
            p = new SqlParameter[1];
            p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
            p[0].Value = username;
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectAllEmployee",p))
            {
                while (reader.Read())
                {
                    lstAutoComplete.Add(new AutoComplete
                    {
                        value = reader["EmployeeCode"].ToString(),
                        text = reader["FirstName"].ToString() + " " + reader["LastName"].ToString()
                    });
                }

                return lstAutoComplete;
            }
        }


        #endregion
    }
}