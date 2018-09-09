using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BTCD_System.BTCD_DL.Transaction
{
    public class clsT_Stock
    {
        #region Fields

        private SqlParameter[] p;
        private List<StockM> lstStock;        
        private SqlDataReader reader;

        #endregion

        #region Methods

        //public string SaveStock(StockM stockM, out string stockNo)
        //{
        //    p = new SqlParameter[9];
        //    try
        //    {

        //        p[0] = new SqlParameter("@ItemId", SqlDbType.Int);
        //        p[0].Value = stockM.ItemId;
        //        p[1] = new SqlParameter("@UserId", SqlDbType.Int);
        //        p[1].Value = stockM.UserId;
        //        p[2] = new SqlParameter("@LocationId", SqlDbType.Int);
        //        p[2].Value = stockM.LocationId;
        //        p[3] = new SqlParameter("@GradeId", SqlDbType.Int);
        //        p[3].Value = stockM.GradeId;
        //        p[4] = new SqlParameter("@Quantity", SqlDbType.Decimal);
        //        p[4].Value = stockM.Quantity;
        //        p[5] = new SqlParameter("@UOMId", SqlDbType.Int);
        //        p[5].Value = stockM.UOMId;
        //        p[6] = new SqlParameter("@UnitPrice", SqlDbType.Decimal);
        //        p[6].Value = stockM.UnitPrice;
        //        p[7] = new SqlParameter("@Result", SqlDbType.VarChar, 200);
        //        p[7].Direction = ParameterDirection.Output;
        //        p[8] = new SqlParameter("@ERRMSG", SqlDbType.VarChar, 400);
        //        p[8].Direction = ParameterDirection.Output;

        //        SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spInsertStockD", p);
        //        stockNo = p[7].Value.ToString();

        //        return p[8].Value.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<StockM> GetDealerStockByDealerId(int dealerId)
        {
            lstStock = new List<StockM>();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@DealerId", SqlDbType.Int) { Value = dealerId };
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectStockByDealerId", p))
            {
                while (reader.Read())
                {
                    lstStock.Add(new StockM
                    {
                        StockId = int.Parse(reader["StockId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        LocationId = int.Parse(reader["LocationId"].ToString()),
                        GradeId = int.Parse(reader["GradeId"].ToString()),
                        Grade = reader["Grade"].ToString(),
                        Quantity = decimal.Parse(reader["Quantity"].ToString()),
                        RemainQuantity = decimal.Parse(reader["RemainQuantity"].ToString()),
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString())
                    });
                }

                return lstStock;
            }
        }

        public List<StockM> GetStockByItemId(int itemId)
        {
            lstStock = new List<StockM>();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@ItemId", SqlDbType.Int) { Value = itemId };
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectStockByItemId", p))
            {
                while (reader.Read())
                {
                    lstStock.Add(new StockM
                    {
                        StockId = int.Parse(reader["StockId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        LocationId = int.Parse(reader["LocationId"].ToString()),
                        GradeId = int.Parse(reader["GradeId"].ToString()),
                        Grade = reader["Grade"].ToString(),
                        Quantity = decimal.Parse(reader["Quantity"].ToString()),
                        RemainQuantity = decimal.Parse(reader["RemainQuantity"].ToString()),
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString())
                    });
                }

                return lstStock;
            }
        }

        public List<StockM> GetStockByItemIdAndGradeId(int itemId, int gradeId)
        {
            lstStock = new List<StockM>();
            p = new SqlParameter[2];

            p[0] = new SqlParameter("@ItemId", SqlDbType.Int) { Value = itemId };
            p[1] = new SqlParameter("@GradeId", SqlDbType.Int) { Value = gradeId };
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectStockByItemIdAndGradeId", p))
            {
                while (reader.Read())
                {
                    lstStock.Add(new StockM
                    {
                        StockId = int.Parse(reader["StockId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        LocationId = int.Parse(reader["LocationId"].ToString()),
                        GradeId = int.Parse(reader["GradeId"].ToString()),
                        Grade = reader["Grade"].ToString(),
                        Quantity = decimal.Parse(reader["Quantity"].ToString()),
                        RemainQuantity = decimal.Parse(reader["RemainQuantity"].ToString()),
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString())
                    });
                }

                return lstStock;
            }
        }

        public List<StockM> GetTotalStockByCategoryId(int categoryId)
        {
            lstStock = new List<StockM>();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = categoryId };
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectTotalStockByCategoryId", p))
            {
                while (reader.Read())
                {
                    lstStock.Add(new StockM
                    {
                        StockId = int.Parse(reader["StockId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        LocationId = int.Parse(reader["LocationId"].ToString()),
                        GradeId = int.Parse(reader["GradeId"].ToString()),
                        Grade = reader["Grade"].ToString(),
                        Quantity = decimal.Parse(reader["Quantity"].ToString()),
                        RemainQuantity = decimal.Parse(reader["RemainQuantity"].ToString()),
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString())
                    });
                }
                return lstStock;
            }
        }



        public string SaveStock(StockM StockM, out string StockNo)
        {
            p = new SqlParameter[10];

            try
            {
                p[0] = new SqlParameter("@ItemId", SqlDbType.Int);
                p[0].Value = StockM.ItemId;
                p[1] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar,50);
                p[1].Value = StockM.EmployeeCode;
                p[2] = new SqlParameter("@LocationId", SqlDbType.Int);
                p[2].Value = StockM.LocationId;
                p[3] = new SqlParameter("@GradeId", SqlDbType.Int);
                p[3].Value = StockM.GradeId;
                p[4] = new SqlParameter("@Quantity", SqlDbType.Decimal);
                p[4].Value = StockM.Quantity;
                p[5] = new SqlParameter("@UOMId", SqlDbType.Int);
                p[5].Value = StockM.UOMId;
                p[6] = new SqlParameter("@UnitPrice", SqlDbType.Decimal);
                p[6].Value = StockM.UnitPrice;
                p[7] = new SqlParameter("@Result", SqlDbType.VarChar, 400);
                p[7].Direction = ParameterDirection.Output;
                p[8] = new SqlParameter("@ERRMSG", SqlDbType.VarChar, 400);
                p[8].Direction = ParameterDirection.Output;
                p[9] = new SqlParameter("@Description", SqlDbType.VarChar);
                p[9].Value = StockM.Description;


                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spInsertStockD", p);

                StockNo = p[7].Value.ToString();

                return p[8].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateStock(StockM StockM, out string StockNo)
        {
            p = new SqlParameter[11];

            try
            {
                p[0] = new SqlParameter("@ItemId", SqlDbType.Int);
                p[0].Value = StockM.ItemId;
                p[1] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar, 50);
                p[1].Value = StockM.EmployeeCode;
                p[2] = new SqlParameter("@LocationId", SqlDbType.Int);
                p[2].Value = StockM.LocationId;
                p[3] = new SqlParameter("@GradeId", SqlDbType.Int);
                p[3].Value = StockM.GradeId;
                p[4] = new SqlParameter("@Quantity", SqlDbType.Decimal);
                p[4].Value = StockM.Quantity;
                p[5] = new SqlParameter("@UOMId", SqlDbType.Int);
                p[5].Value = StockM.UOMId;
                p[6] = new SqlParameter("@UnitPrice", SqlDbType.Decimal);
                p[6].Value = StockM.UnitPrice;
                p[7] = new SqlParameter("@Result", SqlDbType.VarChar, 400);
                p[7].Direction = ParameterDirection.Output;
                p[8] = new SqlParameter("@ERRMSG", SqlDbType.VarChar, 400);
                p[8].Direction = ParameterDirection.Output;
                p[9] = new SqlParameter("@Description", SqlDbType.VarChar);
                p[9].Value = StockM.Description;
                p[10] = new SqlParameter("@StockId", SqlDbType.VarChar);
                p[10].Value = StockM.StockId;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spUpdateStockD", p);

                StockNo = p[7].Value.ToString();

                return p[8].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CopyStock(string StockNo)
        {
            p = new SqlParameter[2];

            try
            {
                p[0] = new SqlParameter("@StockId", SqlDbType.VarChar);
                p[0].Value = StockNo;
                p[1] = new SqlParameter("@Result", SqlDbType.VarChar, 400);
                p[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spCopyStockD", p);
                return p[1].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<StockM> GetStockByUserCode(string EmployeeCode)
        {
            lstStock = new List<StockM>();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar,50) { Value = EmployeeCode };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectTotalStockByUserCode", p))
            {
                while (reader.Read())
                {
                    lstStock.Add(new StockM
                    {
                        StockId = int.Parse(reader["StockId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        LocationId = int.Parse(reader["LocationId"].ToString()),
                        GradeId = int.Parse(reader["GradeId"].ToString()),
                        Grade = reader["Grade"].ToString(),
                        Quantity = decimal.Parse(reader["Quantity"].ToString()),
                        RemainQuantity = decimal.Parse(reader["RemainQuantity"].ToString()),
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UOMName = reader["UOMName"].ToString(),
                        UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                        Location = reader["LocationName"].ToString(),
                        StockOwner = reader["NickName"].ToString(),
                        Description = reader["Description"].ToString(),
                        NoOfBids = int.Parse(reader["NoOfBids"].ToString())
                    });
                }
                return lstStock;
            }
        }

        public StockM GetStockDetailByStockId(int StockId)
        {
            StockM Stock = new StockM();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@StockId", SqlDbType.Int) { Value = StockId };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectStockByStockId", p))
            {
                while (reader.Read())
                {
                    Stock = new StockM
                    {
                        StockId = int.Parse(reader["StockId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        LocationId = int.Parse(reader["LocationId"].ToString()),
                        GradeId = int.Parse(reader["GradeId"].ToString()),
                        Grade = reader["Grade"].ToString(),
                        Quantity = decimal.Parse(reader["Quantity"].ToString()),
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UOMName = reader["UOMName"].ToString(),
                        UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                        Location = reader["LocationName"].ToString(),
                        Description = reader["Description"].ToString(),
                        StockOwner = reader["NickName"].ToString()
                    };
                }
                return Stock;
            }
        }


        public List<StockM> GetStock()
        {
            lstStock = new List<StockM>();


            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectTotalStock"))
            {
                while (reader.Read())
                {
                    lstStock.Add(new StockM
                    {
                        StockId = int.Parse(reader["StockId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        LocationId = int.Parse(reader["LocationId"].ToString()),
                        GradeId = int.Parse(reader["GradeId"].ToString()),
                        Grade = reader["Grade"].ToString(),
                        Quantity = decimal.Parse(reader["Quantity"].ToString()),
                        RemainQuantity = decimal.Parse(reader["RemainQuantity"].ToString()),
                        UOMId = int.Parse(reader["UOMId"].ToString()),
                        UOMName = reader["UOMName"].ToString(),
                        UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                        Location = reader["LocationName"].ToString(),
                        StockOwner = reader["NickName"].ToString(),
                        Description = reader["Description"].ToString()
                    });
                }
                return lstStock;
            }
        }

        
        #endregion
    }
}