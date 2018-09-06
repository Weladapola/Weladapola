using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Common;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BTCD_System.BTCD_DL.Transaction
{
    public class clsT_CustomerRequirment
    {
        private SqlParameter[] p;
        private List<CustomerRequirment> lstCustomerRequirment;
        private SqlDataReader reader;

        public string SaveCustomerRequirment(List<CustomerRequirment> lstCustomerRequirment, out string CustomerRequirmentNo)
        {
            p = new SqlParameter[3];

            try
            {
                p[0] = new SqlParameter("@CustomerReq_D", SqlDbType.Structured);
                p[0].Value = commonFunctions.ToDataTable<CustomerRequirment>(lstCustomerRequirment);
                p[1] = new SqlParameter("@Result", SqlDbType.VarChar, 200);
                p[1].Direction = ParameterDirection.Output;
                p[2] = new SqlParameter("@ERRMSG", SqlDbType.VarChar, 400);
                p[2].Direction = ParameterDirection.Output;


                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "_spInsertCustomerRequirment", p);

                CustomerRequirmentNo = p[1].Value.ToString();

                return p[2].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerRequirment> GetCustomerRequirmentByCreatedUser(string createdUserId)
        {
            lstCustomerRequirment = new List<CustomerRequirment>();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@CreatedUser", SqlDbType.VarChar) { Value = createdUserId };
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectCustomerRequirmentByCreatedUser", p))
            {
                while (reader.Read())
                {
                    lstCustomerRequirment.Add(new CustomerRequirment
                    {
                        CustomerId = int.Parse(reader["CustomerId"].ToString()),
                        CustomerName = reader["CustomerName"].ToString(),
                        RequirementId = int.Parse(reader["RequirementId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        GradeId = int.Parse(reader["GradeId"].ToString()),
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        RequiredDate = DateTime.Parse(reader["RequiredDate"].ToString()),
                        RequiredQty = decimal.Parse(reader["RequiredQty"].ToString()),
                        RequiredPrice = decimal.Parse(reader["RequiredPrice"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        GradeDesc = reader["Grade"].ToString(),
                        UOMDesc = reader["UOMName"].ToString()
                    });
                }

                return lstCustomerRequirment;
            }
        }
    }
}