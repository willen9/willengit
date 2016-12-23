using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IGroup
    {
        DataTable SelectGroup(Group group, string condition,string condition1);
        bool IsModuleId(string groupId);
        string UpdateCurrency(Currency currency);
        DataTable GetAddressInfo(Group group);
        bool DeleteGroup(string group);

        bool AddGroup(Group group, out string msg);

        string InsertCurrency(Currency currency);

        DataTable SearchDetailCurrency(string col, string condition, string value);
    }
}
