using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RFQ_DLogic
    {
        IRFQ_DDAL objDAL = new RFQ_DDAL();

        public List<RFQ_D> SearchRFQ_D(RFQ_D rFQ_D)
        {
            return objDAL.SearchRFQ_D(rFQ_D);
        }
     
        public List<RFQ_D> SearchRFQ_DInfo(string col, string condition, string value)
        {
            return objDAL.SearchRFQ_DInfo(col, condition, value);
        }
    }
}