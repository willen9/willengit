using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class Picking_ProductSerial
    {
        public string Company { get; set; }

        public string UserGroup { get; set; }

        public string Creator { get; set; }

        public string CreateDate { get; set; }

        public string Modifier { get; set; }

        public string ModiDate { get; set; }

        public string PickingOrderType { get; set; }

        public string PickingOrderNo { get; set; }

        public string ItemId { get; set; }

        public string ProductNo { get; set; }

        public string SerialNo { get; set; }
    }
}