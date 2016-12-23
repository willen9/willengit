using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫明細資料檔
    public class FileArchive
    {
        //公司別
        public string FileKey { get; set; }

        //群組
        public string FileContent { get; set; }

        //建立人員
        public string Company { get; set; }

        //建立日期
        public string Creator { get; set; }

        //修改人員
        public string CreateDate { get; set; }

    }
}