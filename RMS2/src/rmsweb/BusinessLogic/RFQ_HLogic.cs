using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RFQ_HLogic
    {
        IRFQ_HDAL objDAL = new RFQ_HDAL();

        public bool AddRFQ_H(RFQ_H rFQ_H, string strItemId,
            string strPartNo, string strPartName,
            string strQTY, string strUnit, string strUnitPrice,
            string strPrice, string strListPrice, string strRemark,
            string strRepaired, out string msg)
        {
            return objDAL.AddRFQ_H(rFQ_H,  strItemId,
             strPartNo,  strPartName,
             strQTY,  strUnit,  strUnitPrice,
             strPrice,  strListPrice,  strRemark,
             strRepaired, out msg);
        }

        public bool UpdateRFQ_H(RFQ_H rFQ_H, string strItemId,
            string strPartNo, string strPartName,
            string strQTY, string strUnit, string strUnitPrice,
            string strPrice, string strListPrice, string strRemark,
            string strRepaired, out string msg)
        {
            return objDAL.UpdateRFQ_H(rFQ_H, strItemId,
             strPartNo, strPartName,
             strQTY, strUnit, strUnitPrice,
             strPrice, strListPrice, strRemark,
             strRepaired, out msg);
        }

        public bool DelRFQ_H(RFQ_H rFQ_H, out string msg)
        {
            return objDAL.DelRFQ_H(rFQ_H, out msg);
        }

        public List<RFQ_H> SearchRFQ_H(string col, string condition, string value)
        {
            return objDAL.SearchRFQ_H(col, condition, value);
        }

        public RFQ_H GetRFQ_HInfo(RFQ_H rFQ_H)
        {
            return objDAL.GetRFQ_HInfo(rFQ_H);
        }
        
    }
}