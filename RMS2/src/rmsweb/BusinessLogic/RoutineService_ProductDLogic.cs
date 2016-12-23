using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RoutineService_ProductDLogic
    {
        IRoutineService_ProductDDAL objDAL = new RoutineService_ProductDDAL();

        public List<RoutineService_ProductD> SearchRoutineService_ProductD(string RoutineSerOrderType, string RoutineSerOrderNo)
        {
            return objDAL.SearchRoutineService_ProductD(RoutineSerOrderType, RoutineSerOrderNo);
        }
    }
}