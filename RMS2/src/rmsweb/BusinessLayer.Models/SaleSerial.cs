using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class SaleSerial
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

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string SaleDate { get; set; }

        public string SettingCode { get; set; }

        public string RoutineService { get; set; }

        public string SourceOrderType { get; set; }

        public string SourceOrderNo { get; set; }

        public string SourceOrderItemId { get; set; }

        public string Remark { get; set; }

        public string ProductTypeId1Name { get; set; }

        public string CustomerType { get; set; }
    }
}