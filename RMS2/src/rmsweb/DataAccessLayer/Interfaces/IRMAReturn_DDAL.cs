using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRMAReturn_DDAL
    {
        List<RMAReturn_D> SearchRMAReturn_D(RMAReturn_D rMAReturn_D);

    }
}