using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace BTCD_System.BTCD_DL.Master
{
    /// <summary>
    /// </summary>
    public class clsM_Location
    {
        #region Fields

        private SqlParameter[] p;
        private List<LocationM> lstLocation;
        private SqlDataReader _reader;

        #endregion



        #region Methods
        public bool SaveLocation(LocationM emsLocationM)
        {
            p = new SqlParameter[4];

            //p[0] = new SqlParameter("@LocationCode", SqlDbType.VarChar) { Value = emsLocationM.LocationCode };
            //p[1] = new SqlParameter("@Description", SqlDbType.VarChar) { Value = emsLocationM.Description };
            //p[2] = new SqlParameter("@TrnUser", SqlDbType.VarChar) { Value = emsLocationM.TrnUser };
            //p[3] = new SqlParameter("@TrnType", SqlDbType.VarChar) { Value = 1 };

            return SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,
                "spLocationsM", p) > 0;
        }


        public bool UpdateLocation(LocationM emsLocationM)
        {
            p = new SqlParameter[4];

            //p[0] = new SqlParameter("@LocationCode", SqlDbType.VarChar) { Value = emsLocationM.LocationCode };
            //p[1] = new SqlParameter("@Description", SqlDbType.VarChar) { Value = emsLocationM.Description };
            //p[2] = new SqlParameter("@TrnUser", SqlDbType.VarChar) { Value = emsLocationM.TrnUser};
            //p[3] = new SqlParameter("@TrnType", SqlDbType.VarChar) { Value = 2 };

            return SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,
                "spLocationsM", p) > 0;
        }


        public List<LocationM> GetAllLocations()
        {
            lstLocation = new List<LocationM>();
            using (_reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,"spSelectAllFromLocationsM"))
            {
                while (_reader.Read())
                {
                    lstLocation.Add(new LocationM
                    {
                        LocationId = int.Parse(_reader["LocationId"].ToString()),
                        LocationCode = _reader["LocationCode"].ToString(),
                        LocationName = _reader["LocationName"].ToString()
                    });
                }

                return lstLocation;
            }
        }



        public bool DeleteLocation(LocationM Location)
        {
            p = new SqlParameter[2];

            p[0] = new SqlParameter("@LocationCode", SqlDbType.VarChar) { Value = Location.LocationCode };
            //p[1] = new SqlParameter("@TrnUser", SqlDbType.VarChar) { Value = emsLocation.TrnUser };

            return SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,
                "spDeleteLocationsM", p) > 0;
        }

        #endregion
    }
}