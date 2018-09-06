using System;
using System.Data;
using System.Data.SqlClient;

namespace BTCD_System.BTCD_DL.AutoIncrement
{
    public class clsAutoNumber
    {

        #region Fields
        private string strquery = "";
        private SqlParameter[] p;
        private SqlDataReader reader;
        //private AutoIncrementU AutoIncrementU;
        #endregion



        #region Methods

        ///// <summary>
        ///// Saves a record to the AutoNumber table.
        ///// </summary>
        //public void Save_AutoNumber(AutoIncrementU AutoNumber)
        //{
        //    p = new SqlParameter[2];

        //    try
        //    {
        //        p[0] = new SqlParameter("@TrnID", SqlDbType.VarChar);
        //        p[0].Value = AutoNumber.TrnID;
        //        p[1] = new SqlParameter("@SerialNo", SqlDbType.Decimal);
        //        p[1].Value = AutoNumber.SerialNo;

        //        SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spUpdateAutoIncrement", p);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        //public AutoIncrementU GetAutoNumber(AutoIncrementU AutoNumber)
        //{
        //    p = new SqlParameter[1];
        //    AutoIncrementU = new AutoIncrementU();

        //    try
        //    {
        //        p[0] = new SqlParameter("@TrnID", SqlDbType.VarChar);
        //        p[0].Value = AutoNumber.TrnID;

        //        using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spGetAutoIncrement", p))
        //        {
        //            while (reader.Read())
        //            {
        //                AutoIncrementU.TrnID = reader["TrnID"].ToString();
        //                AutoIncrementU.TrnsactionName = reader["TrnsactionName"].ToString();
        //                AutoIncrementU.Prefix = reader["Prefix"].ToString();
        //                AutoIncrementU.SerialNo = Decimal.Parse(reader["SerialNo"].ToString());
        //                AutoIncrementU.SerialLength = int.Parse(reader["SerialLength"].ToString());
        //                AutoIncrementU.Mode = int.Parse(reader["Mode"].ToString());
        //            }

        //            return AutoIncrementU;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        #endregion
    }
}
