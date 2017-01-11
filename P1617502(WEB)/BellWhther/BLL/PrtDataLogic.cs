using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DAL;
using IDAL;
using Model;
using Newtonsoft.Json;

namespace BLL
{
    public class PrtDataLogic
    {
        IPrtData obj =new PrtDataDAL();

        public List<LabelSetting> GetInfo(PrtData model)
        {
            DataTable dtReturn = obj.GetInfo(model);
          
            List<LabelSetting> lstReturn = new List<LabelSetting>();
            LabelSetting labelSetting = null;
            foreach (DataRow dr in dtReturn.Rows)
            {
                labelSetting = JsonConvert.DeserializeObject<LabelSetting>(dr["LabelData"].ToString());
                labelSetting.DocEntry = dr["DocEntry"].ToString();
                lstReturn.Add(labelSetting);
            }
            return lstReturn;
        }

        public bool PintRepeat(string chk)
        {
            List<PrtData> lstPrint = new List<PrtData>();
            string[] pks = chk.Split(',');
            PrtData prtData = null;
            foreach (string pk in pks)
            {
                prtData = new PrtData();
                prtData.DocEntry = pk;
                lstPrint.Add(prtData);
            }
            return obj.PintRepeat(lstPrint);
        }
    }
}