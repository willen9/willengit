using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRGAReturn_DDAL
    {
        List<RGAReturn_D> SearchRGAReturn_D(RGAReturn_D rGAReturn_D);

    }
}