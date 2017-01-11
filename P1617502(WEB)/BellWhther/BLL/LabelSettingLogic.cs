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
    public class LabelSettingLogic
    {
        ILabelSetting obj = new LabelSettingDAL();

        public List<LabelSetting> GetAllBtw()
        {
            DataTable dtLabelSetting= obj.GetAllBtw();
            DataRow drNew = dtLabelSetting.NewRow();
            drNew["Type"] = "==請選擇==";
            dtLabelSetting.Rows.InsertAt(drNew,0);
            List<LabelSetting> lstReturn = new List<LabelSetting>();
            LabelSetting labelSetting = null;
            foreach (DataRow dr in dtLabelSetting.Rows)
            {
                labelSetting = new LabelSetting();
                labelSetting.Type = dr["Type"].ToString();
                lstReturn.Add(labelSetting);
            }
            return lstReturn;
        }

        public List<LabelSetting> GetInfo(LabelSetting model)
        {
            DataTable dtLabelSetting = obj.GetInfo(model);
            List<LabelSetting> lstReturn = new List<LabelSetting>();
            LabelSetting labelSetting = null;
            foreach (DataRow dr in dtLabelSetting.Rows)
            {
                labelSetting = new LabelSetting();
                labelSetting.Type = dr["Type"].ToString();
                labelSetting.CustomerCode = dr["CustomerCode"].ToString();
                labelSetting.VKORG = dr["VKORG"].ToString();
                labelSetting.VTWEG = dr["VTWEG"].ToString();
                labelSetting.SPART = dr["SPART"].ToString();
                labelSetting.Ary = dr["Ary"].ToString();
                labelSetting.AryInitialCycle = dr["AryInitialCycle"].ToString();
                labelSetting.NextLabel = dr["NextLabel"].ToString();
                lstReturn.Add(labelSetting);
            }
            return lstReturn;
        }

        public bool DelLabelMaintains(string chk)
        {
            List<LabelSetting> lstDel = new List<LabelSetting>();
            string[] pks = chk.Split(',');
            LabelSetting labelSetting = null;
            foreach (string pk in pks)
            {
                labelSetting = new LabelSetting();
                labelSetting.Type = pk.Split('-')[0];
                labelSetting.CustomerCode = pk.Split('-')[1];
                labelSetting.VKORG = pk.Split('-')[2];
                labelSetting.VTWEG = pk.Split('-')[3];
                labelSetting.SPART = pk.Split('-')[4];
                lstDel.Add(labelSetting);
            }
            return obj.DelLabelSettings(lstDel);
        }

        public bool SaveLabelMaintain(LabelSetting modelOld,LabelSetting model, string mode, out string msg)
        {
            return obj.SaveLabelSetting(modelOld,model, mode, out msg);
        }
    }
}