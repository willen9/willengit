using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRMAReturn_HDAL
    {
        bool AddRMAReturn_H(RMAReturn_H rMAReturn_H, string strItemId,
            string strRMAType, string strRMANo,
            string strRMAItemId, string strProductNo, string strProductName,
            string strSerialNo, string strRemark, string strReturned,
            string strRepairDetail, string strMalfunctionReason, string strRGAType,string strRGANo, out string msg);

        bool UpdateRMAReturn_H(RMAReturn_H rMAReturn_H, string strItemId,
            string strRMAType, string strRMANo,
            string strRMAItemId, string strProductNo, string strProductName,
            string strSerialNo, string strRemark, string strReturned,
            string strRepairDetail, string strMalfunctionReason, string strRGAType, string strRGANo, out string msg);

        bool DelRMAReturn_H(RMAReturn_H rMAReturn_H, out string msg);

        List<RMAReturn_H> SearchRMAReturn_H(string col, string condition, string value);

        RMAReturn_H GetRMAReturn_HInfo(RMAReturn_H rMAReturn_H);
    }
}