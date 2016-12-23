using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SupportApl_ProductDLogic
    {
        ISupportApl_ProductDDAL objDAL = new SupportApl_ProductDDAL();

        public List<SupportApl_ProductD> GetSupportApl_ProductD(string SupportAplOrderType, string SupportAplOrderNo)
        {
            return objDAL.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);
        }
    }
}