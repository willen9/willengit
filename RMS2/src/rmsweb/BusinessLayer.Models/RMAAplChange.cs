using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class RMAAplChange
    {
        //公司別
        public string Company { get; set; }

        public string UserGroup { get; set; }

        //群組
        public string Creator { get; set; }

        public string CreatorName { get; set; }

        //建立人員
        public string CreateDate { get; set; }

        //建立日期
        public string Modifier { get; set; }

        //修改人員
        public string ModiDate { get; set; }

        //修改日期
        public string Confirmor { get; set; }

        public string ConfirmorName { get; set; }

        //定保計畫單別
        public string ConfirmDate { get; set; }

        //定保計畫單號
        public string RMAAplType { get; set; }

        public string OrderSName { get; set; }

        public string RMAAplNo { get; set; }

        //單據日期
        public string Version { get; set; }

        //計畫日期
        public string OrderDate { get; set; }

        //客戶代號
        public string SourceOrderType { get; set; }

        public string SourceOrderNo { get; set; }

        public string IsClosed { get; set; }

        public string ChangeReason { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string ProductNo { get; set; }

        public string ProductName { get; set; }

        public string SerialNo { get; set; }
        public string VendorWStartDate { get; set; }

        public string VendorWExpiryDate { get; set; }

        public string WarrantyStartDate { get; set; }

        public string WarrantyExpiryDate { get; set; }

        public string Confirmed { get; set; }

        public int NoOfPrints { get; set; }

        public string OptionDetail { get; set; }

        public string Remark { get; set; }

        public string Returned { get; set; }

        public string TestResult { get; set; }

        public string OldVersion { get; set; }

        public string OldOptionDetail { get; set; }

        public string OldRemark { get; set; }

        public string OldTestResult { get; set; }

    }
}