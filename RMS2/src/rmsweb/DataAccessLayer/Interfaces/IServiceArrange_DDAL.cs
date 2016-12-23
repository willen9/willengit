using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IServiceArrange_DDAL
    {
        List<ServiceArrange_D> SearchServiceArrange_D(string SerArrangeOrderType, string SerArrangeOrderNo);

        List<ServiceArrange_D> SearchServiceArrange_DInfo(string col, string condition, string value);

        bool ChangeRoutineSdervice(RoutineService_H RoutineService_H, string ServiceArrangeId, string chkRemark);
    }
}