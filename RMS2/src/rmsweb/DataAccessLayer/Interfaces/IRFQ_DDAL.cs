using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRFQ_DDAL
    {
        List<RFQ_D> SearchRFQ_D(RFQ_D rFQ_D);

        List<RFQ_D> SearchRFQ_DInfo(string col, string condition, string value);
    }
}