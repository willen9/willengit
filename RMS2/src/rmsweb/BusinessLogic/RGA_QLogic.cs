using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RGA_QLogic
    {
        IRGA_QDAL objDAL = new RGA_QDAL();

        public List<RGA_Q> SearchRGA_Q(RGA_Q rGA_Q)
        {
            return objDAL.SearchRGA_Q(rGA_Q);
        }
    }
}