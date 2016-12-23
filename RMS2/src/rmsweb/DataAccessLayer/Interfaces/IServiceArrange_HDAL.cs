using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IServiceArrange_HDAL
    {
        bool AddServiceArrange_H(ServiceArrange_H serviceArrange_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg);

        bool UpdateServiceArrange_H(ServiceArrange_H serviceArrange_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg);

        bool DelServiceArrange_H(ServiceArrange_H serviceArrange_H, out string msg);

        List<ServiceArrange_H> SearchServiceArrange_H(string col, string condition, string value);

        ServiceArrange_H GetServiceArrange_HInfo(ServiceArrange_H serviceArrange_H);

        List<ServiceArrange_H> GetServiceArrange_H(ServiceArrange_H serviceArrange_H, int Page);

        List<ServiceArrange_H> GetServiceArrange_HSerialNo(ServiceArrange_H serviceArrange_H, int Page);

        void UpdateConfirmed(ServiceArrange_H serviceArrange_H);
    }
}