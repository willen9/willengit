using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SupportApl_SupportItemLogic
    {
        ISupportApl_SupportItemDAL objDAL = new SupportApl_SupportItemDAL();

        public List<SupportApl_SupportItem> GetSupportApl_SupportItem(string SupportAplOrderType, string SupportAplOrderNo)
        {
            return objDAL.GetSupportApl_SupportItem (SupportAplOrderType,  SupportAplOrderNo);
        }
    }
}