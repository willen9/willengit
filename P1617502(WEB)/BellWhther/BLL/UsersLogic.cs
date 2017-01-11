using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DAL;
using IDAL;
using Model;

namespace BLL
{
    public class UsersLogic
    {
        IUsers obj=new UsersDAL();

        public bool CheckLogIn(string uid, string pwd)
        {
            return obj.CheckLogIn(uid, pwd);
        }

        public List<Users> GetInfo(Users model)
        {
            DataTable dtUsers = obj.GetInfo(model);
            List<Users> lstReturn = new List<Users>();
            Users user = null;
            foreach (DataRow dr in dtUsers.Rows)
            {
                user = new Users();
                user.UserId = dr["UserId"].ToString();
                user.UserName = dr["UserName"].ToString();
                user.Pwd = dr["Pwd"].ToString();
                user.GroupID = dr["GroupID"].ToString();
                lstReturn.Add(user);
            }
            return lstReturn;
        }

        public bool DelUsers(string chk)
        {
            List<Users> lstDel = new List<Users>();
            string[] pks = chk.Split(',');
            Users model = null;
            foreach (string pk in pks)
            {
                model = new Users();
                model.UserId = pk;
                lstDel.Add(model);
            }
            return obj.DelUsers(lstDel);
        }

        public bool SaveUsers(Users modelOld,Users model, string mode, out string msg)
        {
            return obj.SaveUsers(modelOld,model, mode, out msg);
        }

        public string GetLimitByUid(string uid)
        {
            DataTable dtLimit = obj.GetLimitByUid(uid);
            string strReturn = "";
            foreach (DataRow dr in dtLimit.Rows)
            {
                strReturn += dr["SysFunctionsID"].ToString() + ",";
            }
            return strReturn.TrimEnd(',');
        }
    }
}