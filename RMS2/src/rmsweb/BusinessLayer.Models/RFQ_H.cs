using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class RFQ_H
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

        //定保計畫單別
        public string ConfirmDate { get; set; }

        //定保計畫單號
        public string RFQType { get; set; }

        public string OrderSName { get; set; }

        public string RFQNo { get; set; }

        //單據日期
        public string OrderDate { get; set; }

        public string StatusCode { get; set; }

        //客戶代號
        public int NoOfPrints { get; set; }

        public string SourceOrderType { get; set; }

        public string SourceOrderNo { get; set; }

        public string Remark { get; set; }

        public string VendorId { get; set; }

        public string VendorName { get; set; }

        public double TaxRate { get; set; }

        public string Contact { get; set; }

        public string TelNo { get; set; }

        public string FaxNo { get; set; }
        public string Address { get; set; }

        public string ProductNo { get; set; }

        public string ProductName { get; set; }

        public string SerialNo { get; set; }

        public string PurchaseDate { get; set; }

        public string WarrantyStartDate { get; set; }

        public string WarrantyExpiryDate { get; set; }

        public double TaxExcluded { get; set; }

        public double Tax { get; set; }

        public double TaxIncluded { get; set; }

        public string Confirmed { get; set; }

        public string Responser { get; set; }

        public string ResponseDate { get; set; }

        public string StatusOfResponse { get; set; }

    }
}