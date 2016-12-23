using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Quotation_DLogic
    {
        IQuotation_DDAL objDAL = new Quotation_DDAL();

        public List<Quotation_D> SearchQuotation_D(Quotation_D quotation_D)
        {
            return objDAL.SearchQuotation_D(quotation_D);
        }   
        
        public List<Quotation_D> SearchQuotation_DInfo(string col, string condition, string value)
        {
            return objDAL.SearchQuotation_DInfo(col, condition, value);
        }
    }
}