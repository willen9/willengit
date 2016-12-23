using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RGA_DLogic
    {
        IRGA_DDAL objDAL = new RGA_DDAL();

        public List<RGA_D> SearchRGA_D(RGA_D rGA_D)
        {
            return objDAL.SearchRGA_D(rGA_D);
        }
    }
}