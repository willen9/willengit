using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫明細資料檔
    public class Schedule
    {
        public string Company { get; set; }

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
        public string guID { get; set; }

        //定保計畫單別
        public string Title { get; set; }

        //定保計畫單號
        public string StartDate { get; set; }

        //項次
        public string EndDate { get; set; }

        //保養月份
        public string Holiday { get; set; }

        public string InCounting { get; set; }
        
    }
}