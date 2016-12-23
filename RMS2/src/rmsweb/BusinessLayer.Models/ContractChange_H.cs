using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //維護合約變更單頭資料檔
    public class ContractChange_H
    {
        //公司別
        public string Company { get; set; }

        //群組
        public string UserGroup { get; set; }

        //建立人員
        public string Creator { get; set; }

        //建立日期
        public string CreateDate { get; set; }

        //修改人員
        public string Modifier { get; set; }

        //修改日期
        public string ModiDate { get; set; }

        //變更人員
        public string Confirmor { get; set; }

        public string ConfirmorName { get; set; }

        //變更日期
        public string ConfirmedDate { get; set; }

        //合約單別
        public string ContractType { get; set; }

        public string OrderSName { get; set; }

        //合約單號
        public string ContractNo { get; set; }

        //變更版次
        public string Version { get; set; }

        //單據日期
        public string OrderDate { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string ContractSDate { get; set; }

        public string ContractEDate { get; set; }

        //終止合約
        public string TerminationOfService { get; set; }

        //變更原因
        public string ModiReason { get; set; }

        //確認碼
        public string Confirmed { get; set; }
    }
}