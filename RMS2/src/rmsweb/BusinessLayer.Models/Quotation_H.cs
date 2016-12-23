using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class Quotation_H
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
        public string ConfirmedDate { get; set; }

        //定保計畫單號
        public string QuotationType { get; set; }

        public string OrderSName { get; set; }

        public string QuotationNo { get; set; }

        //單據日期
        public string OrderDate { get; set; }

        public string StatusCode { get; set; }

        //客戶代號
        public int NoOfPrints { get; set; }

        public string Repairman { get; set; }

        public string SourceOrderType { get; set; }

        public string SourceOrderNo { get; set; }

        public string Responser { get; set; }

        public string StatusOfResponse { get; set; }

        public string ResponseDate { get; set; }

        public string Response { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string TaxRate { get; set; }

        public string AddressSName { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string TelNo { get; set; }

        public string FaxNo { get; set; }

        public string ProductNo { get; set; }

        public string ProductName { get; set; }

        public string SerialNo { get; set; }

        public string AssetNo { get; set; }

        public string UnderWarranty { get; set; }

        public string SaleDate { get; set; }

        public string PurchaseDate { get; set; }

        public string WarrantyStartDate { get; set; }

        public string WarrantyExpiryDate { get; set; }

        public string Remark { get; set; }

        public string TestResult { get; set; }

        public string InternalRemark { get; set; }


        public string TaxExcluded { get; set; }

        public string Tax { get; set; }


        public string TaxIncluded { get; set; }
        public string Confirmed { get; set; }

    }
}