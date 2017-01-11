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
    public class LabelMaintainLogic
    {
        ILabelMaintain obj = new LabelMaintainDAL();

        public List<LabelMaintain> GetInfo(LabelMaintain model)
        {
            DataTable dtLabelMaintain=obj.GetInfo(model);
            List<LabelMaintain> lstReturn = new List<LabelMaintain>();
            LabelMaintain labelMaintain = null;
            foreach (DataRow dr in dtLabelMaintain.Rows)
            {
                labelMaintain = new LabelMaintain();
                labelMaintain.Type = dr["Type"].ToString();
                labelMaintain.CustomerCode = dr["CustomerCode"].ToString();
                labelMaintain.VKORG = dr["VKORG"].ToString();
                labelMaintain.VTWEG = dr["VTWEG"].ToString();
                labelMaintain.SPART = dr["SPART"].ToString();
                labelMaintain.SPEC = dr["SPEC"].ToString();
                labelMaintain.LCP = dr["LCP"].ToString();
                labelMaintain.MSLLevel = dr["MSLLevel"].ToString();
                labelMaintain.CustomerVer = dr["CustomerVer"].ToString();
                labelMaintain.Rev = dr["Rev"].ToString();
                labelMaintain.Other1 = dr["Other1"].ToString();
                labelMaintain.Other2 = dr["Other2"].ToString();
                labelMaintain.Other3 = dr["Other3"].ToString();
                labelMaintain.Other4 = dr["Other4"].ToString();
                labelMaintain.Other5 = dr["Other5"].ToString();
                lstReturn.Add(labelMaintain);
            }
            return lstReturn;
        }

        public bool DelLabelMaintains(string chk)
        {
            List<LabelMaintain> lstDel = new List<LabelMaintain>();
            string[] pks = chk.Split(',');
            LabelMaintain labelMaintain = null;
            foreach (string pk in pks)
            {
                labelMaintain = new LabelMaintain();
                labelMaintain.Type = pk.Split('-')[0];
                labelMaintain.CustomerCode = pk.Split('-')[1];
                labelMaintain.VKORG = pk.Split('-')[2];
                labelMaintain.VTWEG = pk.Split('-')[3];
                labelMaintain.SPART = pk.Split('-')[4];
                labelMaintain.SPEC = pk.Split('-')[5];
                lstDel.Add(labelMaintain);
            }
            return obj.DelLabelMaintains(lstDel);
        }

        public bool SaveLabelMaintain(LabelMaintain model, string mode, out string msg)
        {
            return obj.SaveLabelMaintain(model, mode,out msg);
        }
    }
}