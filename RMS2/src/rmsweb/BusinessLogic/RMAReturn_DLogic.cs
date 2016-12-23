using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RMAReturn_DLogic
    {
        IRMAReturn_DDAL objDAL = new RMAReturn_DDAL();

        public List<RMAReturn_D> SearchRMAReturn_D(RMAReturn_D rMAReturn_D)
        {
            return objDAL.SearchRMAReturn_D(rMAReturn_D);
        }
    }
}