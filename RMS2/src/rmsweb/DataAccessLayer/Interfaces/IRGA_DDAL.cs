using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRGA_DDAL
    {
        List<RGA_D> SearchRGA_D(RGA_D rGA_D);
    }
}