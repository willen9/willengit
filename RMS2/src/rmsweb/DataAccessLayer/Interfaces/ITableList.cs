using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ITableList
    {
        DataTable SelectTableList(TableList tableList, string condition,string condition1);
        bool IsModuleId(string groupId);
        string UpdateCurrency(TableList tableList,string AddressId,string ColumnNo,string ColumnName,string DataType,string ColLength,string Remark);
        bool IsProgramId(string moduleId);
        bool DeleteGroup(string groupId);
        DataTable GetAddressInfo(TableList tableList);
        string InsertCurrency(TableList tableList,string AddressId, string ColumnNo, string ColumnName, string DataType, string ColLength, string Remark);

        DataTable SearchDetailCurrency(string col, string condition, string value);
    }
}
