using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RMAAplLogic
    {
        IRMAAplDAL objDAL = new RMAAplDAL();

        public bool AddRMAApl(RMAApl rMAApl, out string msg)
        {
            return objDAL.AddRMAApl(rMAApl, out msg);
        }

        public bool UpdateRMAApl(RMAApl rMAApl, out string msg)
        {
            return objDAL.UpdateRMAApl(rMAApl, out msg);
        }

        public bool DelRMAApl(RMAApl rMAApl, out string msg)
        {
            return objDAL.DelRMAApl(rMAApl, out msg);
        }

        public List<RMAApl> SearchRMAApl(string col, string condition, string value)
        {
            return objDAL.SearchRMAApl(col, condition, value);
        }

        public RMAApl GetRMAAplInfo(RMAApl rMAApl)
        {
            return objDAL.GetRMAAplInfo(rMAApl);
        }

        public List<RMAApl> SearchRMAAplInfo(RMAApl rMAApl, int Page)
        {
            return objDAL.SearchRMAAplInfo(rMAApl, Page);
        }
    }
}