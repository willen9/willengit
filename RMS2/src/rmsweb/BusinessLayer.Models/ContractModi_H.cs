using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //維護合約變更單頭資料檔
    public class ContractModi_H
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

        //合約單別
        public string ContractNo { get; set; }

        //變更版次
        public string Version { get; set; }

        //單據日期
        public string OrderDate { get; set; }

        //終止合約
        public string TerminationOfService { get; set; }

        //變更原因
        public string ModiReason { get; set; }

        //確認碼
        public string Confirmed { get; set; }
    }
}