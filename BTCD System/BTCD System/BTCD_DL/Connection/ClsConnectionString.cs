using System.Configuration;


/// <summary>
/// Summary description for ClsConnectionstring
/// </summary>
/// 


namespace BTCD_System.BTCD_DL.Connection
{
    public class clsConnectionString
    {
        public clsConnectionString()
        {

        }
        public static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        }
    }
}