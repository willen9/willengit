using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ServiceArrange_DLogic
    {
        IServiceArrange_DDAL objDAL = new ServiceArrange_DDAL();

        public List<ServiceArrange_D> SearchServiceArrange_D(string SerArrangeOrderType, string SerArrangeOrderNo)
        {
            return objDAL.SearchServiceArrange_D(SerArrangeOrderType, SerArrangeOrderNo);
        }

        public List<ServiceArrange_D> SearchServiceArrange_DInfo(string col, string condition, string value)
        {
            return objDAL.SearchServiceArrange_DInfo( col,  condition,  value);
        }

        public bool ChangeRoutineSdervice(RoutineService_H RoutineService_H, string ServiceArrangeId, string chkRemark)
        {
            return objDAL.ChangeRoutineSdervice(RoutineService_H, ServiceArrangeId, chkRemark);
        }
    }
}