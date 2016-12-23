using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRoutineService_ProductDDAL
    {
        List<RoutineService_ProductD> SearchRoutineService_ProductD(string RoutineSerOrderType, string RoutineSerOrderNo);
    }
}