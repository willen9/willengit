using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class ServiceArrange_H
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
        public string ContractType { get; set; }

        //定保計畫單別
        public string SerArrangeOrderType { get; set; }

        //定保計畫單號
        public string SerArrangeOrderNo { get; set; }

        public string ItemId { get; set; }

        //單據日期
        public string OrderDate { get; set; }

        //計畫日期
        public string ConfirmedDate { get; set; }

        //客戶代號
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerType { get; set; }

        //版次
        public string Version { get; set; }

        //來源單號
        public string SourceOrderType { get; set; }

        public string SourceOrderNo { get; set; }

        public string SourceOrderItemId { get; set; }

        //品號
        public string ProductNo { get; set; }

        //品名
        public string ProductName { get; set; }

        //序號
        public string SerialNo { get; set; }

        //銷貨日期
        public string SaleDate { get; set; }

        //定保次數
        public string RoutineServiceFreq { get; set; }

        //保固期限
        public string WarrantyPeriod { get; set; }

        //保固起日
        public string WarrantySDate { get; set; }

        //保固迄日
        public string WarrantyEDate { get; set; }

        //確認碼
        public string Confirmed { get; set; }

        //確認人員
        public string Confirmor { get; set; }

        //備註
        public string Remark { get; set; }

        //內部備註
        public string InternalRemark { get; set; }

        public string ArrangeMonth { get; set; }

    }
}