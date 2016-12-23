using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SupportApl_ProcessDLogic
    {
        ISupportApl_ProcessDDAL objDAL = new SupportApl_ProcessDDAL();

        public List<SupportApl_ProcessD> GetSupportApl_ProductD(string SupportAplOrderType, string SupportAplOrderNo)
        {
            return objDAL.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);
        }
    }
}