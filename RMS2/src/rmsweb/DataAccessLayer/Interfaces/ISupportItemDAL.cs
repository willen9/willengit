using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISupportItemDAL
    {
        bool AddSupportItem(SupportItem supportItem, out string msg);

        List<SupportItem> GetSupportItem(SupportItem supportItem, int Page);

        bool DelSupportItem(string SupportItemId);

        SupportItem GetSupportItemInfo(string SupportItemId);

        bool IsSupportItemId(string SupportItemId);

        bool UpdataSupportItem(SupportItem supportItem);

        List<SupportItem> SearchSupportItem(string col, string condition, string value);
    }
}