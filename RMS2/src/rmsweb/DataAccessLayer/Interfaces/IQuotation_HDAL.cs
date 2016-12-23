using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IQuotation_HDAL
    {
        bool AddQuotation_H(Quotation_H quotation_H, string strItemId,
            string strPartNo, string strPartName, string strQTY,
            string strUnit,string strUnitPrice,string strPreferentialPrice,
            string strRemark, out string msg);

        bool UpdateQuotation_H(Quotation_H quotation_H, string strItemId,
            string strPartNo, string strPartName, string strQTY,
            string strUnit, string strUnitPrice, string strPreferentialPrice,
            string strRemark, out string msg);

        bool DelQuotation_H(Quotation_H quotation_H, out string msg);

        List<Quotation_H> SearchQuotation_H(string col, string condition, string value);

        Quotation_H GetQuotation_HInfo(Quotation_H quotation_H);

        bool ConfirmedQuotation_H(Quotation_H quotation_H, out string msg);


    }
}