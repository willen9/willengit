using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SupportItemLogic
    {
        ISupportItemDAL objDAL = new SupportItemDAL();

        public bool AddSupportItem(SupportItem supportItem, out string msg)
        {
            return objDAL.AddSupportItem(supportItem, out msg);
        }

        public List<SupportItem> GetSupportItem(SupportItem supportItem, int Page)
        {
            return objDAL.GetSupportItem(supportItem,Page);
        }

        public bool DelSupportItem(string SupportItemId)
        {
            return objDAL.DelSupportItem(SupportItemId);
        }

        public SupportItem GetSupportItemInfo(string SupportItemId)
        {
            return objDAL.GetSupportItemInfo(SupportItemId);
        }

        public bool IsSupportItemId(string SupportItemId)
        {
            return objDAL.IsSupportItemId(SupportItemId);
        }

        public bool UpdataSupportItem(SupportItem supportItem)
        {
            return objDAL.UpdataSupportItem(supportItem);
        }

        public List<SupportItem> SearchSupportItem(string col, string condition, string value)
        {
            return objDAL.SearchSupportItem(col, condition, value);
        }
    }
}