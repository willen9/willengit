using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class Picking_ProductSerialDAL : IPicking_ProductSerialDAL
    {
        public bool AddPicking_ProductSerial(Picking_ProductSerial picking_ProductSerial,string[] SerialNoValue,out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;

            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dt = dbMySQL.ExecuteDataTable("select * from Picking_ProductD where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo and ItemId=@ItemId",tran,
                    new DbParameter[] {
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_ProductSerial.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_ProductSerial.PickingOrderNo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,picking_ProductSerial.ItemId)
                        });
                if(dt==null||dt.Rows.Count==0)
                {
                    msg = "品項明細不存在！";
                    tran.Commit();
                    return false;
                }
                dbMySQL.ExecuteNonQuery("delete from picking_productserial where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo and ItemId=@ItemId", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_ProductSerial.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_ProductSerial.PickingOrderNo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,picking_ProductSerial.ItemId)
                        });
                for (int i = 0; i < SerialNoValue.Length; i++)
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from Warranty_H where ProductNo=@ProductNo and SerialNo=@SerialNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dt.Rows[0]["ProductNo"].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo", DbType.String, SerialNoValue[i])
                        }).ToString()) == 0)
                    {
                        msg = "序號：" + SerialNoValue[i] + "不存在！";
                        tran.Rollback();
                        return false;
                    }

                    dbMySQL.ExecuteScalar(@"insert into picking_productserial (Company,UserGroup,Creator,CreateDate,
                            PickingOrderType,PickingOrderNo,ItemId,ProductNo,SerialNo) values (@Company,@UserGroup,@Creator,@CreateDate,
                            @PickingOrderType,@PickingOrderNo,@ItemId,@ProductNo,@SerialNo)", tran,
                            new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company", DbType.String, picking_ProductSerial.Company),
                            DataAccessMySQL.CreateParameter("UserGroup", DbType.String, picking_ProductSerial.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator", DbType.String, picking_ProductSerial.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PickingOrderType", DbType.String, picking_ProductSerial.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo", DbType.String, picking_ProductSerial.PickingOrderNo),
                            DataAccessMySQL.CreateParameter("ItemId", DbType.String, picking_ProductSerial.ItemId),
                            DataAccessMySQL.CreateParameter("ProductNo", DbType.String, dt.Rows[0]["ProductNo"].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo", DbType.String, SerialNoValue[i])
                            });
                }

                dbMySQL.ExecuteNonQuery("update Picking_ProductD set PickingQTY=@PickingQTY where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo and ItemId=@ItemId", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("PickingQTY",DbType.Double,SerialNoValue.Length),
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_ProductSerial.PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_ProductSerial.PickingOrderNo),
                        DataAccessMySQL.CreateParameter("ItemId",DbType.String,picking_ProductSerial.ItemId)
                    });
                msg = "";
                tran.Commit();
                return true;
            }catch(Exception ex)
            {
                if(tran!=null)
                {
                    tran.Rollback();
                }
                msg = ex.Message;
                return false;
            }finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
           
        }

        public List<Picking_ProductSerial> SeachPicking_ProductSerial(string PickingOrderType, string PickingOrderNo,string ItemId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Picking_ProductSerial> objPicking_ProductSerial = new List<Picking_ProductSerial>();

            DataTable dtPicking_ProductSerial = dbMySQL.ExecuteDataTable(@"select * from picking_productserial where 
                PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo and ItemId=@ItemId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,PickingOrderType),
                    DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,PickingOrderNo),
                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,ItemId)
                });

            if (dtPicking_ProductSerial.Rows.Count > 0)
            {

                foreach (DataRow dr in dtPicking_ProductSerial.Rows)
                {
                    Picking_ProductSerial obj = new Picking_ProductSerial();
                    obj.PickingOrderType = dr["PickingOrderType"].ToString();
                    obj.PickingOrderNo = dr["PickingOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    objPicking_ProductSerial.Add(obj);
                }
            }

            return objPicking_ProductSerial;
        }
    }
}