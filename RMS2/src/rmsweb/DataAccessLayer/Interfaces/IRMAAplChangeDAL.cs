using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRMAAplChangeDAL
    {
        bool AddRMAAplChange(RMAAplChange rMAAplChange, out string msg);

        bool UpdateRMAAplChange(RMAAplChange rMAAplChange, out string msg);

        bool DelRMAAplChange(RMAAplChange rMAAplChange, out string msg);

        List<RMAAplChange> SearchRMAAplChange(string col, string condition, string value);

        RMAAplChange GetRMAAplChangeInfo(RMAAplChange rMAAplChange);
    }
}