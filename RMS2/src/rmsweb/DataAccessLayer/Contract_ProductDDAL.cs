using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class Contract_ProductDDAL : IContract_ProductDDAL
    {
        public List<Contract_ProductD> GetContract_ProductD(string ContractType, string ContractNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Contract_ProductD> objContract_ProductD = new List<Contract_ProductD>();

            DataTable dtContract_ProductD = dbMySQL.ExecuteDataTable(@"
                select p.ContractType,p.ContractNo,p.ItemId,p.ProductNo,
                p.ProductName,p.qty,p.Unit,p.WarrantyPeriod,p.WarrantySDate,
                p.WarrantyEDate,p.RoutineServiceFreq,p.Remark,
                GROUP_CONCAT(SerialNo) as SerialNo,GROUP_CONCAT(IsClosed) as IsClosed from contract_productd as p 
                left join contract_productserial as s on 
                p.ContractType=s.ContractType and 
                p.ContractNo=s.ContractNo and p.ItemId=s.ItemId
                where p.ContractType=@ContractType and p.ContractNo=@ContractNo
                group by p.ContractType,p.ContractNo,p.ItemId,p.ProductNo,
                p.ProductName,p.qty,p.Unit,p.WarrantyPeriod,p.WarrantySDate,
                p.WarrantyEDate,p.RoutineServiceFreq,p.Remark",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo)
                });
            if (dtContract_ProductD.Rows.Count > 0)
            {
                foreach (DataRow dr in dtContract_ProductD.Rows)
                {
                    Contract_ProductD obj = new Contract_ProductD();
                    obj.ContractType = dr["ContractType"].ToString();
                    obj.ContractNo = dr["ContractNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.QTY = dr["QTY"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.WarrantyPeriod = dr["WarrantyPeriod"].ToString();
                    obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.RoutineServiceFreq = dr["RoutineServiceFreq"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString().Replace(",","#");
                    obj.IsClosed = dr["IsClosed"].ToString().Replace(",", "#");
                    objContract_ProductD.Add(obj);
                }
            }

            return objContract_ProductD;
        }

        public List<Contract_ProductD> GetContractSerial(Contract_ProductD contract_ProductD, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Contract_ProductD> objContract_ProductD = new List<Contract_ProductD>();

            string sql = @"select d.ContractType,d.ContractNo,d.ItemId,d.ProductNo,
                d.ProductName,d.WarrantyPeriod,d.WarrantySDate,d.WarrantyEDate,
                d.RoutineServiceFreq,s.SerialNo from contract_productd as d 
                right join contract_productserial as s on s.ContractType=d.ContractType 
                and s.ContractNo=d.ContractNo and s.ItemId=d.ItemId and 
                s.ProductNo=d.ProductNo where 1=1";

            if (!string.IsNullOrEmpty(contract_ProductD.ContractType))
            {
                sql += " and s.ContractType=@ContractType";
            }

            if (!string.IsNullOrEmpty(contract_ProductD.ContractNo))
            {
                sql += " and s.ContractNo=@ContractNo";
            }

            if (!string.IsNullOrEmpty(contract_ProductD.ProductNo))
            {
                sql += " and s.ProductNo=@ProductNo";
            }

            if (!string.IsNullOrEmpty(contract_ProductD.SerialNo))
            {
                sql += " and s.SerialNo=@SerialNo";
            }

            if(!string .IsNullOrEmpty(contract_ProductD.IsClosed))
            {
                sql += " and IsClosed=@IsClosed";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtContract_ProductD = dbMySQL.ExecuteDataTable(sql, new DbParameter[]{
                DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_ProductD.ContractType),
                DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_ProductD.ContractNo),
                DataAccessMySQL.CreateParameter("ItemId",DbType.String,contract_ProductD.ItemId),
                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,"%"+contract_ProductD.ProductNo+"%"),
                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,"%"+contract_ProductD.SerialNo+"%"),
                DataAccessMySQL.CreateParameter("IsClosed",DbType.String,contract_ProductD.IsClosed),
                DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
            });  

            if (dtContract_ProductD.Rows.Count > 0)
            {
                foreach (DataRow dr in dtContract_ProductD.Rows)
                {
                    Contract_ProductD obj = new Contract_ProductD();
                    obj.ContractType = dr["ContractType"].ToString();
                    obj.ContractNo = dr["ContractNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.WarrantyPeriod = dr["WarrantyPeriod"].ToString();
                    obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.RoutineServiceFreq = dr["RoutineServiceFreq"].ToString();
                    objContract_ProductD.Add(obj);
                }
            }

            return objContract_ProductD;
        }

        public List<Contract_ProductD> GetContractProduct(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Contract_ProductD> objContract_ProductD = new List<Contract_ProductD>();

            string sql = @"select s.*,o.OrderSName,d.*,h.CustomerId,c.CustomerName,c.CustomerType from 
                Contract_ProductSerial as s left join Contract_ProductD as d on 
                s.ContractType=d.ContractType and s.ContractNo=d.ContractNo and s.ItemId=d.ItemId left join Contract_H as h 
                on h.ContractType=d.ContractType and h.ContractNo=d.ContractNo
                left join Customer as c on c.CustomerId=h.CustomerId
                left join OrderCategory as o on o.OrderType=s.ContractType where Confirmed<>'V'";

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

            string strCondition = "";

            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len + 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";
                if (conditionArray[i] == "like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i] + "%");
                }
                else if (conditionArray[i] == "%like")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i]);
                }
                else if (conditionArray[i] == "%like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i] + "%");
                }
                else
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
                }
                dblen++;
            }

            if (Page != 0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

            DataTable dtContract_ProductD = null;
            if (strCondition != "")
            {

                dtContract_ProductD = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtContract_ProductD = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }

            if (dtContract_ProductD.Rows.Count > 0)
            {
                foreach (DataRow dr in dtContract_ProductD.Rows)
                {
                    Contract_ProductD obj = new Contract_ProductD();
                    obj.ContractType = dr["ContractType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.ContractNo = dr["ContractNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.CustomerType = "";
                    if(dr["CustomerType"].ToString()!="")
                    {
                        obj.CustomerType = dr["CustomerType"].ToString()=="A"? "A:一般客戶" : "B:經銷商";
                    }
                    obj.WarrantyPeriod = dr["WarrantyPeriod"].ToString();
                    obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.RoutineServiceFreq = dr["RoutineServiceFreq"].ToString();
                    objContract_ProductD.Add(obj);
                }
            }

            return objContract_ProductD;
        }
    }
}