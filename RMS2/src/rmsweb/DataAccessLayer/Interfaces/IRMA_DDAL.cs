using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRMA_DDAL
    {
        List<RMA_D> SearchRMA_D(RMA_D rMA_D);

        List<RMA_D> SearchRMA(string col, string condition, string value);

    }
}