using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISupportAplAsignModiDAL
    {
        void AddSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi);

        List<SupportAplAsignModi> GetSupportAplAsignModi(string col, string condition, string value);

        void DeleteSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi);

        bool UpdateSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi);

        SupportAplAsignModi GetSupportAplAsignInfo(string AsignOrderType, string AsignOrderNo);

        void voidModi(string AsignOrderType, string AsignOrderNo);
    }
}