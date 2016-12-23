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
    public class ContractChange_ProductSerialDAL : IContractChange_ProductSerialDAL
    {
        public List<ContractChange_ProductSerial> GetContractChange_ProductSerial(string ContractType, string ContractNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<ContractChange_ProductSerial> objContractChange_ProductSerial = new List<ContractChange_ProductSerial>();

            DataTable dtContractChange_ProductSerial = dbMySQL.ExecuteDataTable(@"
                select p.ContractType,p.ContractNo,p.ItemId,p.ProductNo,
                p.ProductName,p.qty,p.Unit,p.WarrantyPeriod,p.WarrantySDate,
                p.WarrantyEDate,p.RoutineServiceFreq,p.Remark,
                GROUP_CONCAT(co.SerialNo) as SerialNo,GROUP_CONCAT(co.NewIsClosed) as NewIsClosed  
                from contract_productd as p 
                left join contractchange_productserial as s on 
                p.ContractType=s.ContractType and 
                p.ContractNo=s.ContractNo and p.ItemId=s.ItemId
                left join ContractChange_ProductSerial as co on co.ContractNo=p.ContractNo and co.ContractNo=p.ContractNo and co.ItemId=p.ItemId
                where p.ContractType=@ContractType and p.ContractNo=@ContractNo
                group by p.ContractType,p.ContractNo,p.ItemId,p.ProductNo,
                p.ProductName,p.qty,p.Unit,p.WarrantyPeriod,p.WarrantySDate,
                p.WarrantyEDate,p.RoutineServiceFreq,p.Remark",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo)
                });
            if (dtContractChange_ProductSerial.Rows.Count > 0)
            {
                foreach (DataRow dr in dtContractChange_ProductSerial.Rows)
                {
                    ContractChange_ProductSerial obj = new ContractChange_ProductSerial();
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
                    obj.SerialNo = dr["SerialNo"].ToString().Replace(",", "#");
                    obj.NewIsClosed = dr["NewIsClosed"].ToString().Replace(",", "#");
                    objContractChange_ProductSerial.Add(obj);
                }
            }

            return objContractChange_ProductSerial;
        }
    }
}