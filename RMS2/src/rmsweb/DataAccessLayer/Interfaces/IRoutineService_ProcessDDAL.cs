using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRoutineService_ProcessDDAL
    {
        List<RoutineService_ProcessD> SearchRoutineService_ProcessD(string RoutineSerOrderType, string RoutineSerOrderNo);
    }
}