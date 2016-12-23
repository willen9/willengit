using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IPicking_ProductSerialDAL
    {
        bool AddPicking_ProductSerial(Picking_ProductSerial picking_ProductSerial, string[] SerialNoValue, out string msg);

        List<Picking_ProductSerial> SeachPicking_ProductSerial(string PickingOrderType, string PickingOrderNo, string ItemId);
    }
}