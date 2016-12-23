using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ServiceArrangeModi_DLogic
    {
        IServiceArrangeModi_DDAL objDAL = new ServiceArrangeModi_DDAL();

        public List<ServiceArrangeModi_D> SearchServiceArrangeModi_D(string SerArrangeOrderType, string SerArrangeOrderNo, string Version)
        {
            return objDAL.SearchServiceArrangeModi_D(SerArrangeOrderType, SerArrangeOrderNo, Version);
        }
    }
}