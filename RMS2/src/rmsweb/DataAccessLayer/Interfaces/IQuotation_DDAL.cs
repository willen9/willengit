using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IQuotation_DDAL
    {
        List<Quotation_D> SearchQuotation_D(Quotation_D quotation_D);

        List<Quotation_D> SearchQuotation_DInfo(string col, string condition, string value);
    }
}