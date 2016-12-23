using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RGAChange_HLogic
    {
        IRGAChange_HDAL objDAL = new RGAChange_HDAL();

        public bool AddRGAChange_H(RGAChange_H rGAChange_H, string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg)
        {
            return objDAL.AddRGAChange_H(rGAChange_H,
             strQQuestionNo,  strQQuestion,
             strQDescription,  strDItemId,
             strDPartNo,  strDPartName,
             strDQTY,  strDUnit,  strDRemark,
             strDRepaired,  strDSourceOrderType,
             strDSourceOrderNo,  strDSourceItemId, out msg);
        }

        public bool UpdateRGAChange_H(RGAChange_H rGAChange_H, 
            string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg)
        {
            return objDAL.UpdateRGAChange_H(rGAChange_H,
             strQQuestionNo, strQQuestion,
             strQDescription, strDItemId,
             strDPartNo, strDPartName,
             strDQTY, strDUnit, strDRemark,
             strDRepaired, strDSourceOrderType,
             strDSourceOrderNo, strDSourceItemId, out msg);
        }

        public bool DelRGAChange_H(RGAChange_H rGAChange_H, out string msg)
        {
            return objDAL.DelRGAChange_H(rGAChange_H, out msg);
        }

        public List<RGAChange_H> SearchRGAChange_H(string col, string condition, string value)
        {
            return objDAL.SearchRGAChange_H(col, condition, value);
        }

        public RGAChange_H GetRGAChange_HInfo(RGAChange_H rGAChange_H)
        {
            return objDAL.GetRGAChange_HInfo(rGAChange_H);
        }

    }
}