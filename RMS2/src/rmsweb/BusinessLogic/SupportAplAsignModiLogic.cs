using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SupportAplAsignModiLogic
    {
        ISupportAplAsignModiDAL objDAL = new SupportAplAsignModiDAL();

        public void AddSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi)
        {
            objDAL.AddSupportAplAsignModi(supportAplAsignModi);
        }

        public List<SupportAplAsignModi> GetSupportAplAsignModi(string col, string condition, string value)
        {
            return objDAL.GetSupportAplAsignModi(col, condition, value);
        }

        public void DeleteSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi)
        {
            objDAL.DeleteSupportAplAsignModi(supportAplAsignModi);
        }

        public bool UpdateSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi)
        {
            return objDAL.UpdateSupportAplAsignModi(supportAplAsignModi);
        }

        public SupportAplAsignModi GetSupportAplAsignInfo(string AsignOrderType, string AsignOrderNo)
        {
            return objDAL.GetSupportAplAsignInfo(AsignOrderType, AsignOrderNo);
        }

        public void voidModi(string AsignOrderType, string AsignOrderNo)
        {
            objDAL.voidModi(AsignOrderType, AsignOrderNo);
        }
    }
}