using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IServiceArrangeModi_HDAL
    {
        bool AddServiceArrangeModi_H(ServiceArrangeModi_H ServiceArrangeModi_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg);

        bool UpdateServiceArrangeModi_H(ServiceArrangeModi_H ServiceArrangeModi_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg);

        bool DelServiceArrangeModi_H(ServiceArrangeModi_H ServiceArrangeModi_H, out string msg);

        List<ServiceArrangeModi_H> SearchServiceArrangeModi_H(string col, string condition, string value);

        ServiceArrangeModi_H GetServiceArrangeModi_HInfo(ServiceArrangeModi_H ServiceArrangeModi_H);

    }
}