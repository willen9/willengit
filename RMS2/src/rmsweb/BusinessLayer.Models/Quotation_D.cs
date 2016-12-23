using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫明細資料檔
    public class Quotation_D
    {
        //公司別
        public string UserGroup { get; set; }

        //群組
        public string Creator { get; set; }

        //建立人員
        public string CreateDate { get; set; }

        //建立日期
        public string Modifier { get; set; }

        //修改人員
        public string ModiDate { get; set; }

        //修改日期
        public string QuotationType { get; set; }

        //定保計畫單別
        public string QuotationNo { get; set; }

        //定保計畫單號
        public string ItemId { get; set; }

        public string PartNo { get; set; }

        public string PartName { get; set; }

        public double QTY { get; set; }

        public string Unit { get; set; }

        public double UnitPrice { get; set; }

        public double PreferentialPrice { get; set; }

        public double Subtotal { get; set; }

        public string Remark { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string ConfirmedDate { get; set; }

        public string SourceOrderType { get; set; }

        public string SourceOrderNo { get; set; }

        public string ProductNo { get; set; }

    }
}