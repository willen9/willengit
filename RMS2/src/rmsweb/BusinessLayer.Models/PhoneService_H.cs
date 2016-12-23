using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class PhoneService_H
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

        public string Confirmor { get; set; }

        //修改人員
        public string ConfirmedDate { get; set; }

        //修改日期
        public string PhoneSerType { get; set; }

        //定保計畫單別
        public string PhoneSerNo { get; set; }

        //定保計畫單號
        public string OrderDate { get; set; }

        public int NoOfPrints { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        //修改日期
        public string AddressSName { get; set; }

        //定保計畫單別
        public string Address { get; set; }

        //定保計畫單號
        public string Contact { get; set; }

        public string TelNo { get; set; }

        //修改日期
        public string FaxNo { get; set; }

        //定保計畫單別
        public string ProductNo { get; set; }

        //定保計畫單號
        public string ProductName { get; set; }

        public string SerialNo { get; set; }

        //修改日期
        public string ContractType { get; set; }

        //定保計畫單別
        public string ContractNo { get; set; }

        //定保計畫單號
        public string UnderWarranty { get; set; }

        public string SaleDate { get; set; }

        //修改日期
        public string PurchaseDate { get; set; }

        //定保計畫單別
        public string WarrantyStartDate { get; set; }

        //定保計畫單號
        public string WarrantyExpiryDate { get; set; }

        public string Confirmed { get; set; }

        //修改日期
        public string Remark { get; set; }

        public string OrderSName { get; set; }

        public string CustomerType { get; set; }

        public string CreatorName { get; set; }

    }
}