using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class RMAReturn_D
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

        //定保計畫單號
        public string RMAReturnType { get; set; }

        public string RMAReturnNo { get; set; }

        public string ItemId { get; set; }

        //單據日期
        public string RMAType { get; set; }

        //客戶代號
        public string RMANo { get; set; }

        public string RMAItemId { get; set; }

        public string ProductNo { get; set; }

        public string ProductName { get; set; }

        public string SerialNo { get; set; }

        public string Remark { get; set; }

        public string Returned { get; set; }

        public string RepairDetail { get; set; }
        public string MalfunctionReason { get; set; }

        public string RGAType { get; set; }

        public string RGANo { get; set; }


    }
}