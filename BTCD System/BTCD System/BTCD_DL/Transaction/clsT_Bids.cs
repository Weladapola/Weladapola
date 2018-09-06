using BTCD_System.BTCD_DL.Connection;
using BTCD_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BTCD_System.BTCD_DL.Transaction
{
    public class clsT_Bids
    {
        #region Fields

        private SqlParameter[] p;
        private List<BidsM> lstBid;
        private List<RequestT> lstRequest;
        private SqlDataReader reader;


        #endregion

        #region Methods

        public List<RequestT> ViewRequest(int StockId)
        {
            lstRequest = new List<RequestT>();
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@StockId", SqlDbType.Int) { Value = StockId };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectRequestFromStockId", p))
            {
                while (reader.Read())
                {
                    lstRequest.Add(new RequestT
                    {
                      RequirementId = int.Parse(reader["RequirementId"].ToString()),
                      StockId = int.Parse(reader["StockId"].ToString()),
                      ItemId = int.Parse(reader["ItemId"].ToString()),
                      ItemCode = reader["ItemCode"].ToString(),
                      ItemName = reader["Description"].ToString(),
                      RequiredDate = Convert.ToDateTime(reader["RequiredDate"].ToString()),
                      RequiredPrice = Convert.ToDecimal(reader["RequiredPrice"].ToString()),
                      RequiredQty   = Convert.ToDecimal(reader["RequiredQty"].ToString()),
                      CreatedBy  = reader["CreatedBy"].ToString(),
                    });
                }

                return lstRequest;
            }
        }

        public List<RequestT> GetPendingPaymentAll()
        {
            lstRequest = new List<RequestT>();
            //p = new SqlParameter[1];

            //p[0] = new SqlParameter("@StockId", SqlDbType.Int) { Value = StockId };

            using (reader = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spSelectRequestPendingPaymentAll"))
            {
                while (reader.Read())
                {
                    lstRequest.Add(new RequestT
                    {
                        RequirementId = int.Parse(reader["RequirementId"].ToString()),
                        StockId = int.Parse(reader["StockId"].ToString()),
                        ItemId = int.Parse(reader["ItemId"].ToString()),
                        ItemCode = reader["ItemCode"].ToString(),
                        ItemName = reader["Description"].ToString(),
                        RequiredDate = Convert.ToDateTime(reader["RequiredDate"].ToString()),
                        RequiredPrice = Convert.ToDecimal(reader["RequiredPrice"].ToString()),
                        RequiredQty = Convert.ToDecimal(reader["RequiredQty"].ToString()),
                        CreatedBy = reader["CreatedBy"].ToString(),
                        Payment = Convert.ToDecimal(reader["Payment"].ToString()),
                        RequestedBy = reader["RequestedBy"].ToString(),
                        StockCreatedBy = reader["StockCreatedBy"].ToString(),
                    });
                }

                return lstRequest;
            }
        }
        

        public string SaveRequest(BidsM Bid, out string RequestNo)
        {
            p = new SqlParameter[10];

            try
            {
                p[0] = new SqlParameter("@StockId", SqlDbType.Int);
                p[0].Value = Bid.StockId;
                p[1] = new SqlParameter("@ItemId", SqlDbType.Int);
                p[1].Value = Bid.ItemId;
                p[2] = new SqlParameter("@GradeId", SqlDbType.Int);
                p[2].Value = Bid.GradeId;
                p[3] = new SqlParameter("@RequiredDate", SqlDbType.Date);
                p[3].Value = Bid.RequiredDate;
                p[4] = new SqlParameter("@RequiredQty", SqlDbType.Decimal);
                p[4].Value = Bid.RequestedQty;
                p[5] = new SqlParameter("@RequiredPrice", SqlDbType.Decimal);
                p[5].Value = Bid.RequestedPrice;
                p[6] = new SqlParameter("@LocationId", SqlDbType.Int);
                p[6].Value = Bid.LocationId;
                p[7] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                p[7].Value = Bid.RequestedBy;
                p[8] = new SqlParameter("@Result", SqlDbType.VarChar, 400);
                p[8].Direction = ParameterDirection.Output;
                p[9] = new SqlParameter("@ERRMSG", SqlDbType.VarChar, 400);
                p[9].Direction = ParameterDirection.Output;


                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spInsertRequirmentD", p);

                RequestNo = p[8].Value.ToString();

                return p[9].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ApproveRequest(int RequirementId)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@RequirementId", SqlDbType.Int);
                p[0].Value = RequirementId;
               
                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spAccept", p);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RejectRequest(int RequirementId)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@RequirementId", SqlDbType.Int);
                p[0].Value = RequirementId;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spReject", p);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConfirmPayment(int RequirementId)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@RequirementId", SqlDbType.Int);
                p[0].Value = RequirementId;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spConfirmPayment", p);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}