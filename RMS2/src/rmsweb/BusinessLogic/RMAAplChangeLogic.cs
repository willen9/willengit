using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RMAAplChangeLogic
    {
        IRMAAplChangeDAL objDAL = new RMAAplChangeDAL();

        public bool AddRMAAplChange(RMAAplChange rMAAplChange, out string msg)
        {
            return objDAL.AddRMAAplChange(rMAAplChange, out msg);
        }

        public bool UpdateRMAAplChange(RMAAplChange rMAAplChange, out string msg)
        {
            return objDAL.UpdateRMAAplChange(rMAAplChange, out msg);
        }

        public bool DelRMAAplChange(RMAAplChange rMAAplChange, out string msg)
        {
            return objDAL.DelRMAAplChange(rMAAplChange, out msg);
        }

        public List<RMAAplChange> SearchRMAAplChange(string col, string condition, string value)
        {
            return objDAL.SearchRMAAplChange(col, condition, value);
        }

        public RMAAplChange GetRMAAplChangeInfo(RMAAplChange rMAAplChange)
        {
            return objDAL.GetRMAAplChangeInfo(rMAAplChange);
        }
    }
}