using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BTCD_System.BTCD_DL.Master
{
    public class clsM_UOM
    {
        #region Fields

        private SqlParameter[] p;
        private List<UnitofMeasurementM> lstUnitofMeasurement;
        private SqlDataReader reader;

        #endregion

        #region Methods


        public List<UnitofMeasurementM> GetAllUnitofMeasurement()
        {
            lstUnitofMeasurement = new List<UnitofMeasurementM>();

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectAllFromUnitofMeasurements"))
            {
                while (reader.Read())
                {
                    lstUnitofMeasurement.Add(new UnitofMeasurementM
                    {
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UOMCode = reader["UOMCode"].ToString(),
                        UOMName = reader["UOMName"].ToString(),
                    });
                }

                return lstUnitofMeasurement;
            }
        }


        public UnitofMeasurementM GetUnitofMeasurementFromId(int UOMId)
        {
            UnitofMeasurementM UnitofMeasurement = new UnitofMeasurementM();

            p = new SqlParameter[1];
            p[0] = new SqlParameter("@UOMId", SqlDbType.Int) { Value = UOMId };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectUnitofMeasurementsFromId",p))
            {
                while (reader.Read())
                {
                    UnitofMeasurement = new UnitofMeasurementM
                    {
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UOMCode = reader["UOMCode"].ToString(),
                        UOMName = reader["UOMName"].ToString(),
                    };
                }

                return UnitofMeasurement;
            }
        }
        #endregion
    }
}