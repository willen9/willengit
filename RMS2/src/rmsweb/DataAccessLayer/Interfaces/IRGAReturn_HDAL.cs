using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRGAReturn_HDAL
    {
        bool AddRGAReturn_H(RGAReturn_H rGAReturn_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo, string strRGAType,
            string strRGANo, string strProductNo, string strProductName, string strSerialNo,
            string strTestResult, string strInternalRemark, string strRemark, out string msg);

        bool UpdateRGAReturn_H(RGAReturn_H rGAReturn_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo, string strRGAType,
            string strRGANo, string strProductNo, string strProductName, string strSerialNo,
            string strTestResult, string strInternalRemark, string strRemark, out string msg);

        bool DelRGAReturn_H(RGAReturn_H rGAReturn_H, out string msg);

        List<RGAReturn_H> SearchRGAReturn_H(string col, string condition, string value);

        RGAReturn_H GetRGAReturn_HInfo(RGAReturn_H rGAReturn_H);

    }
}