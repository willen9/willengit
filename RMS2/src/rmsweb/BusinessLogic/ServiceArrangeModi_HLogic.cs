using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ServiceArrangeModi_HLogic
    {
        IServiceArrangeModi_HDAL objDAL = new ServiceArrangeModi_HDAL();

        public bool AddServiceArrangeModi_H(ServiceArrangeModi_H ServiceArrangeModi_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg)
        {
            return objDAL.AddServiceArrangeModi_H(ServiceArrangeModi_H, strItemId,
             strArrangeMonth, strAddressSName, strAddress,
             strIsClosed, strRemark, out msg);
        }

        public bool UpdateServiceArrangeModi_H(ServiceArrangeModi_H ServiceArrangeModi_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg)
        {
            return objDAL.UpdateServiceArrangeModi_H(ServiceArrangeModi_H, strItemId,
             strArrangeMonth, strAddressSName, strAddress,
             strIsClosed, strRemark, out msg);
        }

        public bool DelServiceArrangeModi_H(ServiceArrangeModi_H ServiceArrangeModi_H, out string msg)
        {
            return objDAL.DelServiceArrangeModi_H(ServiceArrangeModi_H, out msg);
        }

        public List<ServiceArrangeModi_H> SearchServiceArrangeModi_H(string col, string condition, string value)
        {
            return objDAL.SearchServiceArrangeModi_H(col, condition, value);
        }

        public ServiceArrangeModi_H GetServiceArrangeModi_HInfo(ServiceArrangeModi_H ServiceArrangeModi_H)
        {
            return objDAL.GetServiceArrangeModi_HInfo(ServiceArrangeModi_H);
        }

    }
}