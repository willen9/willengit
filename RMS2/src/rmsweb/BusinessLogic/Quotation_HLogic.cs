using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Quotation_HLogic
    {
        IQuotation_HDAL objDAL = new Quotation_HDAL();

        public bool AddQuotation_H(Quotation_H quotation_H, string strItemId,
            string strPartNo, string strPartName, string strQTY,
            string strUnit, string strUnitPrice, string strPreferentialPrice,
            string strRemark, out string msg)
        {
            return objDAL.AddQuotation_H(quotation_H, strItemId,
             strPartNo,  strPartName,  strQTY,
             strUnit,  strUnitPrice,  strPreferentialPrice,
             strRemark, out msg);
        }

        public bool UpdateQuotation_H(Quotation_H quotation_H, string strItemId,
            string strPartNo, string strPartName, string strQTY,
            string strUnit, string strUnitPrice, string strPreferentialPrice,
             string strRemark, out string msg)
        {
            return objDAL.UpdateQuotation_H(quotation_H, strItemId,
             strPartNo, strPartName, strQTY,
             strUnit, strUnitPrice, strPreferentialPrice,
              strRemark, out msg);
        }

        public bool DelQuotation_H(Quotation_H quotation_H, out string msg)
        {
            return objDAL.DelQuotation_H(quotation_H, out msg);
        }

        public List<Quotation_H> SearchQuotation_H(string col, string condition, string value)
        {
            return objDAL.SearchQuotation_H(col, condition, value);
        }

        public Quotation_H GetQuotation_HInfo(Quotation_H quotation_H)
        {
            return objDAL.GetQuotation_HInfo(quotation_H);
        }

        public bool ConfirmedQuotation_H(Quotation_H quotation_H, out string msg)
        {
            return objDAL.ConfirmedQuotation_H(quotation_H, out msg);
        }


    }
}