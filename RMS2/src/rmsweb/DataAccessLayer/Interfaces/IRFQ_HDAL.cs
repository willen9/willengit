using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRFQ_HDAL
    {
        bool AddRFQ_H(RFQ_H rFQ_H, string strItemId,
            string strPartNo, string strPartName,
            string strQTY, string strUnit, string strUnitPrice,
            string strPrice, string strListPrice, string strRemark,
            string strRepaired, out string msg);

        bool UpdateRFQ_H(RFQ_H rFQ_H, string strItemId,
            string strPartNo, string strPartName,
            string strQTY, string strUnit, string strUnitPrice,
            string strPrice, string strListPrice, string strRemark,
            string strRepaired, out string msg);

        bool DelRFQ_H(RFQ_H rFQ_H, out string msg);

        List<RFQ_H> SearchRFQ_H(string col, string condition, string value);

        RFQ_H GetRFQ_HInfo(RFQ_H rFQ_H);
    }
}