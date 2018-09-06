using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BTCD_System.BTCD_DL.Master
{
    public class clsM_Customer
    {
        #region Fields

        private SqlParameter[] p;
        private List<CustomerM> lstCustomer;
        private SqlDataReader _reader;

        #endregion

        #region Methods


        public List<CustomerM> GetAllCustomers()
        {
            lstCustomer = new List<CustomerM>();

            using (_reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectAllCustomer"))
            {
                while (_reader.Read())
                {
                    lstCustomer.Add(new CustomerM
                    {
                        CustomerId = int.Parse(_reader["CustomerId"].ToString()),
                        CustomerName = _reader["CustomerName"].ToString()
                    });
                }

                return lstCustomer;
            }
        }





        #endregion
    }
}