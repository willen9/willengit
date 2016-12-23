using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //維護合約變更子單身序號檔
    public class ContractModi_ProductSerial
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

        //序號項次
        public string SerialNoItemId { get; set; }

        //序號
        public string SerialNo { get; set; }

        //新結案碼
        public string NewIsClosed { get; set; }

        //原結案碼
        public string OldIsClosed { get; set; }

    }
}