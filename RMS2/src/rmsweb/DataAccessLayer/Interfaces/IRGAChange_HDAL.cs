using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRGAChange_HDAL
    {
        bool AddRGAChange_H(RGAChange_H rGAChange_H, string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg);

        bool UpdateRGAChange_H(RGAChange_H rGAChange_H, string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg);

        bool DelRGAChange_H(RGAChange_H rGAChange_H, out string msg);

        List<RGAChange_H> SearchRGAChange_H(string col, string condition, string value);

        RGAChange_H GetRGAChange_HInfo(RGAChange_H rGAChange_H);

    }
}