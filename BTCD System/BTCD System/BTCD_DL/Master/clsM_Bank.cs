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
    public class clsM_Bank
    {
        #region Fields

        private SqlParameter[] p;
        private List<BankM>  lstBank;
        private List<BankBranchM> lstBankBranch;
        private SqlDataReader reader;

        #endregion

        #region Methods

        public List<BankM> GetAllBanks()
        {
            lstBank = new List<BankM>();
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectBank"))
            {
                while (reader.Read())
                {
                    lstBank.Add(new BankM
                    {
                        BankCode = reader["BankCode"].ToString(),
                        BankName = reader["BankName"].ToString()
                    });
                }

                return lstBank;
            }
        }

        public List<AutoComplete> GetBranchInBank(string BankCode)
        {
            List<AutoComplete> lstAutoComplete = new List<AutoComplete>();

            p = new SqlParameter[1];

            p[0] = new SqlParameter("@BankCode", SqlDbType.VarChar) { Value = BankCode };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectBankBranchFromCode",p))
            {
                while (reader.Read())
                {
                    lstAutoComplete.Add(new AutoComplete
                    {
                        value = reader["BranchCode"].ToString(),
                        text = reader["BranchName"].ToString()
                    });
                }

                return lstAutoComplete;
            }
        }

        #endregion


    }
}
