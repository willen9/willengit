using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IPicking_ProductDDAL
    {
        List<Picking_ProductD> GetPicking_ProductD(string PickingOrderType, string PickingOrderNo);
    }
}