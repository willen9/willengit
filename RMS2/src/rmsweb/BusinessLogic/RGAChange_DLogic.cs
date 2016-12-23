using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RGAChange_DLogic
    {
        IRGAChange_DDAL objDAL = new RGAChange_DDAL();

        public List<RGAChange_D> SearchRGAChange_D(RGAChange_D rGAChange_D)
        {
            return objDAL.SearchRGAChange_D(rGAChange_D);
        }
    }
}