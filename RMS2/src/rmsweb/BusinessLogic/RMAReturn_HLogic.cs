using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RMAReturn_HLogic
    {
        IRMAReturn_HDAL objDAL = new RMAReturn_HDAL();

        public bool AddRMAReturn_H(RMAReturn_H rMAReturn_H, string strItemId,
            string strRMAType, string strRMANo,
            string strRMAItemId, string strProductNo, string strProductName,
            string strSerialNo, string strRemark, string strReturned,
            string strRepairDetail, string strMalfunctionReason, string strRGAType, string strRGANo, out string msg)
        {
            return objDAL.AddRMAReturn_H(rMAReturn_H,  strItemId,
             strRMAType,  strRMANo,
             strRMAItemId,  strProductNo,  strProductName,
             strSerialNo,  strRemark,  strReturned,
             strRepairDetail,  strMalfunctionReason,  strRGAType,  strRGANo, out msg);
        }

        public bool UpdateRMAReturn_H(RMAReturn_H rMAReturn_H, string strItemId,
            string strRMAType, string strRMANo,
            string strRMAItemId, string strProductNo, string strProductName,
            string strSerialNo, string strRemark, string strReturned,
            string strRepairDetail, string strMalfunctionReason, string strRGAType, string strRGANo, out string msg)
        {
            return objDAL.UpdateRMAReturn_H(rMAReturn_H, strItemId,
             strRMAType, strRMANo,
             strRMAItemId, strProductNo, strProductName,
             strSerialNo, strRemark, strReturned,
             strRepairDetail, strMalfunctionReason, strRGAType, strRGANo, out msg);
        }

        public bool DelRMAReturn_H(RMAReturn_H rMAReturn_H, out string msg)
        {
            return objDAL.DelRMAReturn_H(rMAReturn_H, out msg);
        }

        public List<RMAReturn_H> SearchRMAReturn_H(string col, string condition, string value)
        {
            return objDAL.SearchRMAReturn_H(col, condition, value);
        }

        public RMAReturn_H GetRMAReturn_HInfo(RMAReturn_H rMAReturn_H)
        {
            return objDAL.GetRMAReturn_HInfo(rMAReturn_H);
        }
        
    }
}