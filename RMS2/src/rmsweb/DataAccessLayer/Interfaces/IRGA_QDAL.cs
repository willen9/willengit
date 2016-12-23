using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRGA_QDAL
    {
        List<RGA_Q> SearchRGA_Q(RGA_Q rGA_Q);
    }
}