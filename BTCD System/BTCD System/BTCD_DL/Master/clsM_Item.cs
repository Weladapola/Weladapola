﻿using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Common;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BTCD_System.BTCD_DL.Master
{
    public class clsM_Item
    {
        #region Fields

        private SqlParameter[] p;
        private List<ItemM> lstItem;
        private SqlDataReader reader;

        #endregion

        #region Methods

        public List<ItemM> GetItemsByCategories(int category)
        {
            lstItem = new List<ItemM>();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = category };
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectItemByCategory", p))
            {
                while (reader.Read())
                {
                    lstItem.Add(new ItemM
                    {
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        CategoryId = int.Parse(reader["CategoryId"].ToString()),
                        ItemCode = reader["ItemCode"].ToString(),
                        ItemName = reader["ItemName"].ToString(),
                        Description = reader["Description"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        SinghalaDescription = reader["SinghalaDescription"].ToString(),
                        TamilDescription = reader["TamilDescription"].ToString()
                    });
                }

                return lstItem;
            }
        }

        public ItemM GetItemByCategoryID(int CategoryId)
        {
            ItemM _item = new ItemM();
            p = new SqlParameter[1];
            p[0] = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = CategoryId };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectItemByCategory", p))
            {
                while (reader.Read())
                {
                    _item = new ItemM
                    {
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        CategoryId = int.Parse(reader["CategoryId"].ToString()),
                        ItemName = reader["ItemName"].ToString(),
                        Description = reader["Description"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        SinghalaDescription = reader["SinghalaDescription"].ToString(),
                        TamilDescription = reader["TamilDescription"].ToString()
                    };
                }
                return _item;
            }
        }

        public List<ItemM> GetItemsBySinghalaName(string singhalaName,int laLanguageId)
        {
            lstItem = new List<ItemM>();
            p = new SqlParameter[2];

            p[0] = new SqlParameter("@SinghalaName", SqlDbType.VarChar) { Value = singhalaName };
            p[1] = new SqlParameter("@laLanguageId", SqlDbType.Int) { Value = laLanguageId };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectItemByCategorySinhalaName", p))
            {
                while (reader.Read())
                {
                    lstItem.Add(new ItemM
                    {
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        CategoryId = int.Parse(reader["CategoryId"].ToString()),
                        ItemCode = reader["ItemCode"].ToString(),
                        ItemName = reader["ItemName"].ToString(),
                        Description = reader["Description"].ToString(),ImageUrl = reader["ImageUrl"].ToString()
                    });
                }

                return lstItem;
            }
        }

        public List<AutoComplete> GetItemsByCateroryId(int category)
        {
            List<AutoComplete> lstAutoComplete = new List<AutoComplete>();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = category };
            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectItemByCategory", p))
            {
                while (reader.Read())
                {
                    lstAutoComplete.Add(new AutoComplete
                    {
                        value = reader["ItemId"].ToString(),
                        text =  reader["ItemName"].ToString()
                    });
                }

                return lstAutoComplete;
            }
        }


        public ItemM GetItemByItemId(int ItemId)
        {

            ItemM ItemM = new ItemM();

            p = new SqlParameter[1];
            p[0] = new SqlParameter("@ItemId", SqlDbType.Int) { Value = ItemId };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectItemByItemId", p))
            {
                while (reader.Read())
                {
                    ItemM = new ItemM
                    {
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        CategoryId = int.Parse(reader["CategoryId"].ToString()),
                        ItemCode = reader["ItemCode"].ToString(),
                        ItemName = reader["ItemName"].ToString(),
                        Description = reader["Description"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        SinghalaDescription = reader["SinghalaDescription"].ToString(),
                        TamilDescription = reader["TamilDescription"].ToString()
                    };
                }

                return ItemM;
            }
        }

        public string CreateItem(int ItemCategory, string ItemCode, string ItemName, string ItemDescription, string ItemSinhalaDescription, string ItemTamilDescription,string imgUrl)
        {
            p = new SqlParameter[8];
            p[0] = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = ItemCategory };
            p[1] = new SqlParameter("@ItemCode", SqlDbType.VarChar) { Value = ItemCode };
            p[2] = new SqlParameter("@ItemName", SqlDbType.VarChar) { Value = ItemName };
            p[3] = new SqlParameter("@Description", SqlDbType.VarChar) { Value = ItemDescription };
            p[4] = new SqlParameter("@ImageUrl", SqlDbType.VarChar) { Value = imgUrl };
            p[5] = new SqlParameter("@SinghalaDescription", SqlDbType.VarChar) { Value = ItemSinhalaDescription };
            p[6] = new SqlParameter("@TamilDescription", SqlDbType.VarChar) { Value = ItemTamilDescription };
            p[7] = new SqlParameter("@msg", SqlDbType.VarChar,100);
            p[7].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spInsertItem", p);

            return p[7].Value.ToString();
        }

        public string DeleteItem(int ItemID)
        {
            p = new SqlParameter[2];
            p[0] = new SqlParameter("@ItemId", SqlDbType.Int) { Value = ItemID };
            p[1] = new SqlParameter("@msg", SqlDbType.VarChar, 100);
            p[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spItemDelete", p);

            return p[1].Value.ToString();
        }

        public string UpdateItemById(int ItemID,string ItemName, string ItemDescription, string ItemSinhalaDescription, string ItemTamilDescription, string imgUrl)
        {
            p = new SqlParameter[7];
            p[0] = new SqlParameter("@ItemID", SqlDbType.Int) { Value = ItemID };
            p[1] = new SqlParameter("@ItemName", SqlDbType.VarChar) { Value = ItemName };
            p[2] = new SqlParameter("@Description", SqlDbType.VarChar) { Value = ItemDescription };
            p[3] = new SqlParameter("@ImageUrl", SqlDbType.VarChar) { Value = imgUrl };
            p[4] = new SqlParameter("@SinghalaDescription", SqlDbType.VarChar) { Value = ItemSinhalaDescription };
            p[5] = new SqlParameter("@TamilDescription", SqlDbType.VarChar) { Value = ItemTamilDescription };
            p[6] = new SqlParameter("@msg", SqlDbType.VarChar, 100);
            p[6].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spUpdateItem", p);

            return p[6].Value.ToString();
        }

        public string UpdateItemImageUrlId(int ItemID, string imgUrl)
        {
            p = new SqlParameter[3];
            p[0] = new SqlParameter("@ItemID", SqlDbType.Int) { Value = ItemID };            
            p[1] = new SqlParameter("@ImageUrl", SqlDbType.VarChar) { Value = imgUrl };            
            p[2] = new SqlParameter("@msg", SqlDbType.VarChar, 100);
            p[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spUpdateItemImageUrl", p);

            return p[2].Value.ToString();
        }

        #endregion
    }
}
