using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RMA_HLogic
    {
        IRMA_HDAL objDAL = new RMA_HDAL();

        public bool AddRMA_H(RMA_H rMA_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo,
            string strProductNo, string strProductName, string strSerialNo,
            string strRemark, string strTestResult, string strReturned,
            string strClosed, string strRGAType, string strRGANo, out string msg)
        {
            return objDAL.AddRMA_H(rMA_H,  strItemId,
             strSourceOrderType,  strSourceOrderNo,
             strProductNo,  strProductName,  strSerialNo,
             strRemark,  strTestResult,  strReturned,
             strClosed,  strRGAType,  strRGANo, out msg);
        }

        public bool UpdateRMA_H(RMA_H rMA_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo,
            string strProductNo, string strProductName, string strSerialNo,
            string strRemark, string strTestResult, string strReturned,
            string strClosed, string strRGAType, string strRGANo, out string msg)
        {
            return objDAL.UpdateRMA_H(rMA_H,  strItemId,
             strSourceOrderType,  strSourceOrderNo,
             strProductNo,  strProductName,  strSerialNo,
             strRemark,  strTestResult,  strReturned,
             strClosed,  strRGAType,  strRGANo, out msg);
        }

        public bool DelRMA_H(RMA_H rMA_H, out string msg)
        {
            return objDAL.DelRMA_H(rMA_H, out msg);
        }

        public List<RMA_H> SearchRMA_H(string col, string condition, string value)
        {
            return objDAL.SearchRMA_H(col, condition, value);
        }

        public RMA_H GetRMA_HInfo(RMA_H rMA_H)
        {
            return objDAL.GetRMA_HInfo(rMA_H);
        }
        
    }
}