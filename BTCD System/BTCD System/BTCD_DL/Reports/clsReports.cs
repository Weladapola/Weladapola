using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BTCD_System.BTCD_DL.Connection;



namespace BTCD_System.BTCD_DLL.Reports
{
    public class clsReports
    {
        public List<Models.Reports> SelectAllReportslist()
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                 CommandType.StoredProcedure, "spSelectAllFromReports"))
            {

                List<Models.Reports> locleListReports = new List<Models.Reports>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        locleListReports.Add(new Models.Reports
                        {
                            ID = int.Parse(dr["ID"].ToString()),
                            ReportCategory = dr["ReportCategory"].ToString(),
                            ReportName = dr["ReportName"].ToString(),
                            ReportNumber = int.Parse(dr["ReportNumber"].ToString()),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString(),
                            ReportDisplayName = dr["ReportDisplayName"].ToString(),
                            ReportFolder = dr["ReportFolder"].ToString(),
                            ServerPath = dr["ServerPath"].ToString()
                        });
                    }
                }
                return locleListReports;
            }
        }
    }
}