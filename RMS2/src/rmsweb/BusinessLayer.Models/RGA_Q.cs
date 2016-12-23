using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class RGA_Q
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
        public string RGAType { get; set; }

        //定保計畫單別
        public string RGANo { get; set; }

        //定保計畫單號
        public string ItemId { get; set; }

        public string QuestionNo { get; set; }

        //單據日期
        public string Question { get; set; }

        public string Description { get; set; }
    }
}