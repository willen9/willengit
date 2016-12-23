using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class Warranty_D
    {
        //公司別
        public string Company { get; set; }

        //群組
        public string UserGroup { get; set; }

        //建立人員
        public string Creator { get; set; }

        //建立日期
        public string CreateDate { get; set; }

        //修改人員
        public string Modifier { get; set; }

        //修改日期
        public string ModiDate { get; set; }

        public string ProductNo { get; set; }

        public string ProductName { get; set; }

        public string SerialNo { get; set; }

        public string ChangeDate { get; set; }

        //銷貨日期
        public string SalesDate { get; set; }

        //進貨日期
        public string PurchasesDate { get; set; }

        public string ChangeOrderType { get; set; }

        public string ChangeOrderNo { get; set; }

        public string ChangeOrderItemId { get; set; }

        public string TargetId { get; set; }

        public string TargetName { get; set; }

        public string DataDate { get; set; }

        public string DataTime { get; set; }

        public string WarrantyPeriod { get; set; }

        public string WarrantySDate { get; set; }

        public string WarrantyEDate { get; set; }

        public string WarrantyType { get; set; }

        public string Remark { get; set; }

        public string CustomerName { get; set; }

    }
}