using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SupportAplAsignLogic
    {
        ISupportAplAsignDAL objDAL = new SupportAplAsignDAL();

        public bool AddSupportAplAsign(SupportAplAsign supportAplAsign, out string msg)
        {
            return objDAL.AddSupportAplAsign(supportAplAsign, out msg);
        }

        public List<SupportAplAsign> GetSupportAplAsign(string col, string condition, string value)
        {
            return objDAL.GetSupportAplAsign(col, condition, value);
        }

        public void DeleteSupportAplAsign(string AsignOrderType, string AsignOrderNo)
        {
            objDAL.DeleteSupportAplAsign(AsignOrderType, AsignOrderNo);
        }

        public bool UpdateSupportAplAsign(SupportAplAsign supportAplAsign)
        {
            return objDAL.UpdateSupportAplAsign(supportAplAsign);
        }

        public SupportAplAsign GetSupportAplAsignInfo(string AsignOrderType, string AsignOrderNo)
        {
            return objDAL.GetSupportAplAsignInfo(AsignOrderType, AsignOrderNo);
        }

        public List<SupportAplAsign> SearchSupportAplAsign(SupportAplAsign supportAplAsign, int Page)
        {
            return objDAL.SearchSupportAplAsign(supportAplAsign, Page);
        }

        public void PrintSupportAplAsign(string AsignOrderType, string AsignOrderNo)
        {
            objDAL.PrintSupportAplAsign(AsignOrderType, AsignOrderNo);
        }

        public SupportAplAsign GetInfo(string SupportAplOrderType, string SupportAplOrderNo)
        {
            return objDAL.GetInfo(SupportAplOrderType, SupportAplOrderNo);
        }
    }
}