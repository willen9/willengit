using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RGAReturn_HLogic
    {
        IRGAReturn_HDAL objDAL = new RGAReturn_HDAL();

        public bool AddRGAReturn_H(RGAReturn_H rGAReturn_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo, string strRGAType,
            string strRGANo,string strProductNo,string strProductName,string strSerialNo,
            string strTestResult,string strInternalRemark,string strRemark, out string msg)
        {
            return objDAL.AddRGAReturn_H(rGAReturn_H, strItemId,
             strSourceOrderType,  strSourceOrderNo,  strRGAType,
             strRGANo,  strProductNo,  strProductName,  strSerialNo,
             strTestResult,  strInternalRemark,  strRemark, out msg);
        }

        public bool UpdateRGAReturn_H(RGAReturn_H rGAReturn_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo, string strRGAType,
            string strRGANo, string strProductNo, string strProductName, string strSerialNo,
            string strTestResult, string strInternalRemark, string strRemark, out string msg)
        {
            return objDAL.UpdateRGAReturn_H(rGAReturn_H, strItemId,
             strSourceOrderType, strSourceOrderNo, strRGAType,
             strRGANo, strProductNo, strProductName, strSerialNo,
             strTestResult, strInternalRemark, strRemark, out msg);
        }

        public bool DelRGAReturn_H(RGAReturn_H rGAReturn_H, out string msg)
        {
            return objDAL.DelRGAReturn_H(rGAReturn_H, out msg);
        }

        public List<RGAReturn_H> SearchRGAReturn_H(string col, string condition, string value)
        {
            return objDAL.SearchRGAReturn_H(col, condition, value);
        }

        public RGAReturn_H GetRGAReturn_HInfo(RGAReturn_H rGAReturn_H)
        {
            return objDAL.GetRGAReturn_HInfo(rGAReturn_H);
        }
        
    }
}