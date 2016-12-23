using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISupportApl_ProcessDDAL
    {
        List<SupportApl_ProcessD> GetSupportApl_ProductD(string SupportAplOrderType, string SupportAplOrderNo);
    }
}