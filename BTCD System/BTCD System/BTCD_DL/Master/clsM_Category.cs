using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BTCD_System.BTCD_DL.Master
{

    public class clsM_Category
    {

        #region Fields

        private SqlParameter[] p;
        private List<CategoryM> _lstCategory;
        private SqlDataReader _reader;
        private string ImageUrl;

        #endregion

        #region Methods

        public List<CategoryM> GetAllCategories()
        {
            _lstCategory = new List<CategoryM>();
            using (_reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectCategory"))
            {
                while (_reader.Read())
                {
                    _lstCategory.Add(new CategoryM
                    {
                        CategoryId = int.Parse(_reader["CategoryId"].ToString()),
                        CategoryCode = _reader["CategoryCode"].ToString(),
                        CategoryName = _reader["CategoryName"].ToString(),
                        ImageUrl = _reader["ImageUrl"].ToString()
                    });
                }

                return _lstCategory;
            }
        }
        #endregion
    }


}