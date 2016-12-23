using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class RepairRecord
    {
        //公司別
        public string ProductNo { get; set; }

        //群組
        public string SerialNo { get; set; }

        //建立人員
        public string RGAType { get; set; }

        //建立日期
        public string RGANo { get; set; }

        //修改人員
        public string ChangeDate { get; set; }

        //修改日期
        public string ChangeOrderType { get; set; }

        public string ChangeOrderNo { get; set; }

        public string ChangeMode { get; set; }

        public string Remark { get; set; }

        public string ProductName { get; set; }

    }
}