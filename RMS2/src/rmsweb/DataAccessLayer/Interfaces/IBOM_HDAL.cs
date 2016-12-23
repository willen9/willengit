using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IBOM_HDAL
    {
        bool AddBOM_H(BOM_H bOM_H, string strItemId,
            string strComponentNo, string strQTY, string strRemark, out string msg);

        bool UpdateBOM_H(BOM_H bOM_H, string strItemId,
            string strComponentNo, string strQTY, string strRemark, out string msg);

        bool DelBOM_H(BOM_H bOM_H, out string msg);

        List<BOM_H> SearchBOM_H(string col, string condition, string value);

        BOM_H GetBOM_HInfo(BOM_H bOM_H);

        bool IsBOM_H(BOM_H bOM_H);

        bool ConfirmedBOM_H(BOM_H bOM_H, out string msg);

        bool ImportFile(string path);

        List<BOM_H> SearchBOMH(BOM_H bOM_H, int Page);
    }
}