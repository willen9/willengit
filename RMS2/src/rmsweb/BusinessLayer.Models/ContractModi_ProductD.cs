using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //維護合約變更單身資料檔
    public class ContractModi_ProductD
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

        //項次
        public string ItemId { get; set; }

        //品號
        public string ProductNo { get; set; }

        //品名
        public string ProductName { get; set; }

        //新數量
        public string NewQTY { get; set; }

        //新保固期限
        public string NewWarrantyPeriod { get; set; }

        //新保固起日
        public string NewWarrantySDate { get; set; }

        //新保固迄日
        public string NewWarrantyEDate { get; set; }

        //新定保次數
        public string NewRoutineServiceFreq { get; set; }

        //新單身備註
        public string NewRemark { get; set; }

        //原數量
        public string OldQTY { get; set; }

        //原保固期限
        public string OldWarrantyPeriod { get; set; }

        //原保固起日
        public string OldWarrantySDate { get; set; }

        //原保固迄日
        public string OldWarrantyEDate { get; set; }

        //原定保次數
        public string OldRoutineServiceFreq { get; set; }

        //原單身備註
        public string OldRemark { get; set; }
    }
}