using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDAL
{
    public interface IGroups
    {
        DataTable GetAllGroups();
        DataTable GetAllFunctions();
        bool DelGroups(string groupId,out string msg);
        bool SaveGroups(List<Groups> lstGroups);
        DataTable GetFunctionsByGroupId(string groupId);
    }
}
