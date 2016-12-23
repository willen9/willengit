using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRMA_HDAL
    {
        bool AddRMA_H(RMA_H rMA_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo,
            string strProductNo,string strProductName,string strSerialNo,
            string strRemark,string strTestResult,string strReturned,
            string strClosed,string strRGAType,string strRGANo, out string msg);

        bool UpdateRMA_H(RMA_H rMA_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo,
            string strProductNo, string strProductName, string strSerialNo,
            string strRemark, string strTestResult, string strReturned,
            string strClosed, string strRGAType, string strRGANo, out string msg);

        bool DelRMA_H(RMA_H rMA_H, out string msg);

        List<RMA_H> SearchRMA_H(string col, string condition, string value);

        RMA_H GetRMA_HInfo(RMA_H rMA_H);
    }
}