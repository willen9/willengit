using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IServiceArrangeModi_DDAL
    {
        List<ServiceArrangeModi_D> SearchServiceArrangeModi_D(string SerArrangeOrderType, string SerArrangeOrderNo, string Version);
    }
}