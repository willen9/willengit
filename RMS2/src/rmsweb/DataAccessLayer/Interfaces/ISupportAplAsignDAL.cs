using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISupportAplAsignDAL
    {
        bool AddSupportAplAsign(SupportAplAsign supportAplAsign, out string msg);

        List<SupportAplAsign> GetSupportAplAsign(string col, string condition, string value);

        void DeleteSupportAplAsign(string AsignOrderType, string AsignOrderNo);

        bool UpdateSupportAplAsign(SupportAplAsign supportAplAsign);

        SupportAplAsign GetSupportAplAsignInfo(string AsignOrderType, string AsignOrderNo);

        List<SupportAplAsign> SearchSupportAplAsign(SupportAplAsign supportAplAsign, int Page);

        void PrintSupportAplAsign(string AsignOrderType, string AsignOrderNo);

        SupportAplAsign GetInfo(string SupportAplOrderType, string SupportAplOrderNo);
    }
}