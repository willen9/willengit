using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;

namespace BusinessLogic
{
    public class TableListLogic
    {
        public ITableList objDal = new TableListDAL();

        public DataTable SelectTableList(TableList tableList, string condition, string condition1)
        {
            return objDal.SelectTableList(tableList, condition, condition1);
        }
        public bool IsModuleId(string tableId)
        {
            return objDal.IsModuleId(tableId);
        }
        public string UpdateCurrency(TableList tableList,string AddressId, string ColumnNo, string ColumnName, string DataType, string ColLength, string Remark)
        {
            return objDal.UpdateCurrency(tableList, AddressId, ColumnNo, ColumnName, DataType, ColLength, Remark);
        }
        public bool IsProgramId(string moduleId)
        {
            return objDal.IsProgramId(moduleId);
        }
        public bool DeleteGroup(string groupId)
        {
            return objDal.DeleteGroup(groupId);
        }

        public string InsertCurrency(TableList tableList,string AddressId, string ColumnNo, string ColumnName, string DataType, string ColLength,string Remark)
        {
            return objDal.InsertCurrency(tableList, AddressId, ColumnNo, ColumnName, DataType, ColLength, Remark);
        }
        public DataTable GetAddressInfo(TableList tableList)
        {
            return objDal.GetAddressInfo(tableList);
        }
        public DataTable SearchDetailCurrency(string col, string condition, string value)
        {
            return objDal.SearchDetailCurrency(col, condition, value);
        }
    }
}