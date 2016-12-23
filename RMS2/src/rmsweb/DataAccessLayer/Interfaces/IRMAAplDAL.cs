using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRMAAplDAL
    {
        bool AddRMAApl(RMAApl rMAApl, out string msg);

        bool UpdateRMAApl(RMAApl rMAApl, out string msg);

        bool DelRMAApl(RMAApl rMAApl, out string msg);

        List<RMAApl> SearchRMAApl(string col, string condition, string value);

        RMAApl GetRMAAplInfo(RMAApl rMAApl);

        List<RMAApl> SearchRMAAplInfo(RMAApl rMAApl, int Page);
    }
}