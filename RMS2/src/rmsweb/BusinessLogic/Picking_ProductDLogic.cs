using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Picking_ProductDLogic
    {
        IPicking_ProductDDAL objDAL = new Picking_ProductDDAL();

        public List<Picking_ProductD> GetPicking_ProductD(string PickingOrderType, string PickingOrderNo)
        {
            return objDAL.GetPicking_ProductD(PickingOrderType, PickingOrderNo);
        }
    }
}