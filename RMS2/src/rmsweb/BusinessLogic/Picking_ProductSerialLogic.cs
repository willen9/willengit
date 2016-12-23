using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Picking_ProductSerialLogic
    {
        IPicking_ProductSerialDAL objDAL = new Picking_ProductSerialDAL();

        public bool AddPicking_ProductSerial(Picking_ProductSerial picking_ProductSerial, string[] SerialNoValue,out string msg)
        {
            return objDAL.AddPicking_ProductSerial(picking_ProductSerial,SerialNoValue,out msg);
        }

        public List<Picking_ProductSerial> SeachPicking_ProductSerial(string PickingOrderType, string PickingOrderNo, string ItemId)
        {
            return objDAL.SeachPicking_ProductSerial(PickingOrderType, PickingOrderNo, ItemId);
        }
    }
}