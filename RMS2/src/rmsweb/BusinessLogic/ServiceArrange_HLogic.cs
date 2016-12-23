using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ServiceArrange_HLogic
    {
        IServiceArrange_HDAL objDAL = new ServiceArrange_HDAL();

        public bool AddServiceArrange_H(ServiceArrange_H serviceArrange_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg)
        {
            return objDAL.AddServiceArrange_H(serviceArrange_H, strItemId,
             strArrangeMonth, strAddressSName, strAddress,
             strIsClosed, strRemark, out msg);
        }

        public bool UpdateServiceArrange_H(ServiceArrange_H serviceArrange_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg)
        {
            return objDAL.UpdateServiceArrange_H(serviceArrange_H, strItemId,
             strArrangeMonth, strAddressSName, strAddress,
             strIsClosed, strRemark, out msg);
        }

        public bool DelServiceArrange_H(ServiceArrange_H serviceArrange_H, out string msg)
        {
            return objDAL.DelServiceArrange_H(serviceArrange_H, out msg);
        }

        public List<ServiceArrange_H> SearchServiceArrange_H(string col, string condition, string value)
        {
            return objDAL.SearchServiceArrange_H(col, condition, value);
        }

        public ServiceArrange_H GetServiceArrange_HInfo(ServiceArrange_H serviceArrange_H)
        {
            return objDAL.GetServiceArrange_HInfo(serviceArrange_H);
        }

        public List<ServiceArrange_H> GetServiceArrange_H(ServiceArrange_H serviceArrange_H, int Page)
        {
            return objDAL.GetServiceArrange_H(serviceArrange_H, Page);
        }

        public List<ServiceArrange_H> GetServiceArrange_HSerialNo(ServiceArrange_H serviceArrange_H, int Page)
        {
            return objDAL.GetServiceArrange_HSerialNo(serviceArrange_H, Page);
        }

        public void UpdateConfirmed(ServiceArrange_H serviceArrange_H)
        {
            objDAL.UpdateConfirmed(serviceArrange_H);
        }
    }
}