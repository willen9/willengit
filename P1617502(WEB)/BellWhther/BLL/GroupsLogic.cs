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
    public class GroupsLogic
    {
        IGroups obj = new GroupsDAL();

        public List<Groups> GetAllGroups()
        {
            DataTable dtGroups = obj.GetAllGroups();
            DataRow drNew = dtGroups.NewRow();
            drNew["GroupId"] = "==請選擇==";
            dtGroups.Rows.InsertAt(drNew, 0);
            List<Groups> lstReturn = new List<Groups>();
            Groups groups = null;
            foreach (DataRow dr in dtGroups.Rows)
            {
                groups = new Groups();
                groups.GroupId = dr["GroupId"].ToString();
                lstReturn.Add(groups);
            }
            return lstReturn;
        }

        public List<SysFunctions> GetAllFunctions()
        {
            DataTable dtSysFunctions = obj.GetAllFunctions();
            List<SysFunctions> lstReturn = new List<SysFunctions>();
            SysFunctions sysFunctions = null;
            foreach (DataRow dr in dtSysFunctions.Rows)
            {
                sysFunctions = new SysFunctions();
                sysFunctions.SysFunctionsID = dr["SysFunctionsID"].ToString();
                sysFunctions.SysFunctionsName = dr["SysFunctionsName"].ToString();
                lstReturn.Add(sysFunctions);
            }
            return lstReturn;
        }

        public bool DelGroups(string groupId,out string msg)
        {
            return obj.DelGroups(groupId,out msg);
        }

        public bool SaveGroups(string chk,string groupId)
        {
            List<Groups> lstGroups=new List<Groups>();
            Groups model = null;
            if (!string.IsNullOrEmpty(chk))
            {
                foreach (string fid in chk.Split(','))
                {
                    model = new Groups();
                    model.GroupId = groupId;
                    model.SysFunctionsID = fid;
                    lstGroups.Add(model);
                }
            }
            else
            {
                model = new Groups();
                model.GroupId = groupId;
                lstGroups.Add(model);
            }
            return obj.SaveGroups(lstGroups);
        }

        public string GetFunctionsByGroupId(string groupId)
        {
            DataTable dtReturn=obj.GetFunctionsByGroupId(groupId);
            string strReturn = "";
            foreach (DataRow dr in dtReturn.Rows)
            {
                strReturn += dr["SysFunctionsID"].ToString() + ",";
            }
            if (strReturn.Length > 0)
            {
                strReturn = strReturn.TrimEnd(',');
            }
            return strReturn;
        }
    }
}