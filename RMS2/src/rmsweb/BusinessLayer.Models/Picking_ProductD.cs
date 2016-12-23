using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class Picking_ProductD
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

        public string ProductName { get; set; }

        public string QTY { get; set; }

        public string PickingQTY { get; set; }

        public string Unit { get; set; }

        public string Remark { get; set; }

        public string SerialControl { get; set; }
    }
}