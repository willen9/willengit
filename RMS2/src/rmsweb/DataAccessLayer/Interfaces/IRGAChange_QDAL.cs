using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRGAChange_QDAL
    {
        List<RGAChange_Q> SearchRGAChange_Q(RGAChange_Q rGAChange_Q);
    }
}