using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDAL
{
    public interface IUsers
    {
        bool CheckLogIn(string uid, string pwd);
        DataTable GetInfo(Users model);
        bool DelUsers(List<Users> lstDel);
        bool SaveUsers(Users modelOld,Users model, string mode, out string msg);
        DataTable GetLimitByUid(string uid);
    }
}
