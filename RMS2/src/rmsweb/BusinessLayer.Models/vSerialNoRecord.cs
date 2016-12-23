using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫明細資料檔
    public class vSerialNoRecord
    {
        //公司別
        public string ProductNo { get; set; }

        //群組
        public string ProductName { get; set; }

        //建立人員
        public string SerialNo { get; set; }

        //建立日期
        public string ChangeDate { get; set; }

        //修改人員
        public string TargetId { get; set; }

        //修改日期
        public string TargetName { get; set; }

        //定保計畫單別
        public string WarrantySDate { get; set; }

        //定保計畫單號
        public string WarrantyEDate { get; set; }

        //項次
        public int WarrantyType { get; set; }
    }
}