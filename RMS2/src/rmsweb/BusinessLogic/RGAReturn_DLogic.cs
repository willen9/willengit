using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RGAReturn_DLogic
    {
        IRGAReturn_DDAL objDAL = new RGAReturn_DDAL();

        public List<RGAReturn_D> SearchRGAReturn_D(RGAReturn_D rGAReturn_D)
        {
            return objDAL.SearchRGAReturn_D(rGAReturn_D);
        }
    }
}