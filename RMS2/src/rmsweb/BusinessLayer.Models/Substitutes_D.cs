using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫明細資料檔
    public class Substitutes_D
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
        public string ComponentNo { get; set; }

        //定保計畫單別
        public string MajorComponentNo { get; set; }

        //定保計畫單號
        public string SubstitutesType { get; set; }

        //項次
        public string SubstitutesNo { get; set; }

        public string ProductName { get; set; }

        public string Specification { get; set; }

        public string Unit { get; set; }

        //保養月份
        public double QTY { get; set; }

        //保養月份
        public string Priority { get; set; }

        //保養月份
        public string Remark { get; set; }
    }
}