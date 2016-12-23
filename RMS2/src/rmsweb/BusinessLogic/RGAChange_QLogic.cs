using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RGAChange_QLogic
    {
        IRGAChange_QDAL objDAL = new RGAChange_QDAL();

        public List<RGAChange_Q> SearchRGAChange_Q(RGAChange_Q rGAChange_Q)
        {
            return objDAL.SearchRGAChange_Q(rGAChange_Q);
        }
    }
}