using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class BOM_HLogic
    {
        IBOM_HDAL objDAL = new BOM_HDAL();

        public bool AddBOM_H(BOM_H bOM_H, string strItemId,
            string strComponentNo, string strQTY, string strRemark, out string msg)
        {
            return objDAL.AddBOM_H(bOM_H, strItemId,
             strComponentNo, strQTY, strRemark, out msg);
        }

        public bool UpdateBOM_H(BOM_H bOM_H, string strItemId,
            string strComponentNo, string strQTY, string strRemark, out string msg)
        {
            return objDAL.UpdateBOM_H(bOM_H, strItemId,
             strComponentNo, strQTY, strRemark, out msg);
        }

        public bool DelBOM_H(BOM_H bOM_H, out string msg)
        {
            return objDAL.DelBOM_H(bOM_H, out msg);
        }

        public List<BOM_H> SearchBOM_H(string col, string condition, string value)
        {
            return objDAL.SearchBOM_H(col, condition, value);
        }

        public BOM_H GetBOM_HInfo(BOM_H bOM_H)
        {
            return objDAL.GetBOM_HInfo(bOM_H);
        }

        public bool IsBOM_H(BOM_H bOM_H)
        {
            return objDAL.IsBOM_H(bOM_H);
        }

        public bool ConfirmedBOM_H(BOM_H bOM_H, out string msg)
        {
            return objDAL.ConfirmedBOM_H(bOM_H, out msg);
        }

        public bool ImportFile(string path)
        {
            return objDAL.ImportFile(path);
        }

        public List<BOM_H> SearchBOMH(BOM_H bOM_H, int Page)
        {
            return objDAL.SearchBOMH(bOM_H, Page);
        }
    }
}