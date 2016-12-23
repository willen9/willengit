using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class ProductLifetimeRecord
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

        //修改日期
        public string Confirmor { get; set; }

      
        public string ConfirmedDate { get; set; }

        public string closed { get; set; }
        //定保計畫單別
        public string RGAType { get; set; }
        //定保計畫單號
        public string RGAName { get; set; }

        public string RGANo { get; set; }

        //單據日期
        public string OrderDate { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerType { get; set; }

        public string AddressSName { get; set; }

        public string ProductNo { get; set; }

        public string ProductName { get; set; }

        public string SerialNo { get; set; }

        public string SaleDate { get; set; }

        public string PurchaseDate { get; set; }

        public string SaleMonth { get; set; }

        public string PurchaseMonth { get; set; }
    }
}