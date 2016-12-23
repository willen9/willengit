using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class RMA_D
    {
        //公司別
        public string Company { get; set; }

        public string UserGroup { get; set; }

        //群組
        public string Creator { get; set; }

        //建立人員
        public string CreateDate { get; set; }

        //建立日期
        public string Modifier { get; set; }

        //修改人員
        public string ModiDate { get; set; }

        public string Confirmed { get; set; }

        //定保計畫單號
        public string RMAType { get; set; }

        public string OrderSName { get; set; }

        public string RMANo { get; set; }

        public string ItemId { get; set; }

        public string VendorId { get; set;}

        public string VendorName { get; set; }

        public string ConfirmDate { get; set; }

        public string ConfirmorName { get; set; }

        //單據日期
        public string SourceOrderType { get; set; }

        //客戶代號
        public string SourceOrderNo { get; set; }

        public string ProductNo { get; set; }

        public string ProductName { get; set; }

        public string SerialNo { get; set; }

        public string Remark { get; set; }

        public string TestResult { get; set; }

        public string Returned { get; set; }

        public string Closed { get; set; }
        public string RGAType { get; set; }

        public string RGANo { get; set; }


    }
}