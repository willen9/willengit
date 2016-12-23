using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RoutineService_ProcessDLogic
    {
        IRoutineService_ProcessDDAL objDAL = new RoutineService_ProcessDDAL();

        public List<RoutineService_ProcessD> SearchRoutineService_ProcessD(string RoutineSerOrderType, string RoutineSerOrderNo)
        {
            return objDAL.SearchRoutineService_ProcessD(RoutineSerOrderType, RoutineSerOrderNo);
        }
    }
}