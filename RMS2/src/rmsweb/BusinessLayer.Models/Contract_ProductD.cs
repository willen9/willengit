using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //維護合約單身資料檔
    public class Contract_ProductD
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
        public string ContractType { get; set; }

        public string OrderSName { get; set; }

        //合約單別
        public string ContractNo { get; set; }

        //項次
        public string ItemId { get; set; }

        //品號
        public string ProductNo { get; set; }

        //品名
        public string ProductName { get; set; }

        //數量
        public string QTY { get; set; }

        //單位
        public string Unit { get; set; }

        //保固期限
        public string WarrantyPeriod { get; set; }

        //保固起日
        public string WarrantySDate { get; set; }

        //保固迄日
        public string WarrantyEDate { get; set; }

        //定保次數
        public string RoutineServiceFreq { get; set; }

        //備註
        public string Remark { get; set; }

        //序號
        public string SerialNo { get; set; }

        public string IsClosed { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerType { get; set; }

    }
}