using BTCD_System.BTCD_DL.Connection;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BTCD_System.BTCD_DL.Dashboard
{
    public class clsU_Dashboard
    {

        #region Fields
        private string strquery = "";
        private SqlParameter[] p;

        private SqlDataReader reader;
        private DataTable dtDashBoard;
        #endregion

        #region Methods

        
        public int GetPendingGRNCount()
        {
            try
            {
                dtDashBoard = SqlHelper.ExecuteDataset(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spDashboardPendingGRN").Tables[0];

                if (dtDashBoard != null && dtDashBoard.Rows.Count > 0)
                {
                    return dtDashBoard.Rows.Count;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
