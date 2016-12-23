using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RMA_DLogic
    {
        IRMA_DDAL objDAL = new RMA_DDAL();

        public List<RMA_D> SearchRMA_D(RMA_D rMA_D)
        {
            return objDAL.SearchRMA_D(rMA_D);
        }
        
        public List<RMA_D> SearchRMA(string col, string condition, string value)
        {
            return objDAL.SearchRMA( col,  condition,  value);
        }
    }
}