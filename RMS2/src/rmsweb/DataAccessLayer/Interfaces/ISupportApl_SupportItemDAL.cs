using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISupportApl_SupportItemDAL
    {
        List<SupportApl_SupportItem> GetSupportApl_SupportItem(string SupportAplOrderType, string SupportAplOrderNo);
    }
}