using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class BOM_H
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

        //定保計畫單別
        public string ConfirmDate { get; set; }

        //定保計畫單號
        public string MajorComponentNo { get; set; }

        public string MajorComponentName { get; set; }

        //單據日期
        public string Specification { get; set; }

        //計畫日期
        public string Unit { get; set; }

        //客戶代號
        public double StandardQTY { get; set; }

        public string Remark { get; set; }

        public string Confirmed { get; set; }
        
    }
}