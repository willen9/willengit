using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRGAChange_DDAL
    {
        List<RGAChange_D> SearchRGAChange_D(RGAChange_D rGAChange_D);
    }
}