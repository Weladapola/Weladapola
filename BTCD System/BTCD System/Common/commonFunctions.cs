using BTCD_System.BTCD_DL.AutoIncrement;
using BTCD_System.BTCD_DL.User;
using BTCD_System.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Web;

namespace BTCD_System.Common
{
    public class commonFunctions
    {
        #region Fields
        private string strquery = "";
        #endregion

        #region Public Methods
        public static void IncrementSerial(string ScreenNo)
        {
            AutoIncrementU AutoIncrementU = new AutoIncrementU();
            clsAutoNumber AutoIncrement_DL = new clsAutoNumber();

            AutoIncrementU.TrnID = ScreenNo;
            //AutoIncrementU = AutoIncrement_DL.GetAutoNumber(AutoIncrementU);
             AutoIncrementU.SerialNo += 1;
            //AutoIncrement_DL.Save_AutoNumber(AutoIncrementU);
        }



        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;

        }

        public static void WriteRecentActivity(string TransUser,TransactionType TransactionType,RecordType RecordType, LogType LogType, string Description,string ReferenceNo = "",string Remarks = "")
        {
            clsU_User.WriteUserActivity(new RecentActivityU { TrnUser = TransUser, LogType = (short)LogType, TransactionType = (short)(TransactionType),RecordType = (short)RecordType, Description = Description, ReferenceNo = ReferenceNo, Remarks = Remarks });
        }

        public static List<RecentActivityU> ReadRecentActivity()
        {
            return clsU_User.GetUserActivity(GetTransactionUser());

        }

        public static string GetTransactionUser()
        {
            try
            {
                var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
                return idendity.User.Username;
            }
            catch (Exception ex)
            {
                return "anonymous_user";
            }
        }

        public static string GetTransactionEmployeeCode()
        {
            try
            {
                var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
                return idendity.User.EmployeeCode;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetNickName()
        {
            try
            {
                var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
                return idendity.User.NickName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetRecentActivityDescription(short TransactionType,short RecordType)
        {
            string Transvalue = string.Empty;
            string Recordvalue = string.Empty;

            switch (TransactionType)
            {

                case 0: Transvalue = "Master Data ";break;
                case 1: Transvalue = "GRN "; break;
                case 2: Transvalue = "GRN Reverse "; break;
                case 3: Transvalue = "Inspection "; break;
                case 4: Transvalue = "Inspection Reverse "; break;
                case 5: Transvalue = "Rejection Transfer "; break;
                case 6: Transvalue = "Rejection Reverse "; break;
                case 7: Transvalue = "Issue "; break;
                case 8: Transvalue = "Issue Reverse "; break;
            }

            switch (RecordType)
            {

                case 0: Recordvalue = "Inserted"; break;
                case 1: Recordvalue = "Updated"; break;
                case 2: Recordvalue = "Deleted"; break;
            }


            return Transvalue + Recordvalue;
        }
       
        #endregion
    }

    #region public classes
    public class AutoComplete
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class MessageBox
    {
        public string CssClassName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
    #endregion

    #region public enums
    public enum TransactionType
    {
        MASTER_DATA,
        GRN,
        GRN_REVERSE,
        INSPECTION,
        INSPECTION_REVERSE,
        REJECTION,
        REJECTION_REVERSE,
        ISSUE,
        ISSUE_REVERSE
    }

    public enum LogType
    {
        ERROR,
        SUCESS
    }

    public enum RecordType
    {
        INSERT,
        UPDATE,
        DELETE
    }
    #endregion


}