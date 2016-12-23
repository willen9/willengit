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
    public class GroupLogic
    {
        public IGroup objDal = new GroupDAL();

        public DataTable SelectGroup(Group group, string condition, string condition1)
        {
            return objDal.SelectGroup(group, condition, condition1);
        }
        public bool IsModuleId(string groupId)
        {
            return objDal.IsModuleId(groupId);
        }
        public string UpdateCurrency(Currency currency)
        {
            return objDal.UpdateCurrency(currency);
        }

        public bool AddGroup(Group group, out string msg)
        {
            return objDal.AddGroup(group, out msg);
        }

        public bool DeleteGroup(string groupId)
        {
            return objDal.DeleteGroup(groupId);
        }
        public DataTable GetAddressInfo(Group group)
        {
            return objDal.GetAddressInfo(group);
        }
        public string InsertCurrency(Currency currency)
        {
            return objDal.InsertCurrency(currency);
        }

        public DataTable SearchDetailCurrency(string col, string condition, string value)
        {
            return objDal.SearchDetailCurrency(col, condition, value);
        }
    }
}