using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Modal
{
    public class MMMDB
    {
        [DisplayName("料號")]
        public  string MDB001 { get; set; }

        [DisplayName("中文品名")]
        public  string MDB002 { get; set; }

        [DisplayName("英文品名")]
        public  string MDB003 { get; set; }

        [DisplayName("規格")]
        public  string MDB004 { get; set; }

        [DisplayName("料品類別")]
        public  string MDB005 { get; set; }

        [DisplayName("料品屬性")]
        public  string MDB006 { get; set; }

        [DisplayName("條碼編號")]
        public  string MDB007 { get; set; }

        [DisplayName("條碼類型")]
        public  string MDB008 { get; set; }

        [DisplayName("Base Unit")]
        public  string MDB009 { get; set; }

        [DisplayName("Sales Unit")]
        public  string MDB010 { get; set; }

        [DisplayName("重量單位")]
        public  string MDB011 { get; set; }

        [DisplayName("單位淨重")]
        public  decimal? MDB012 { get; set; }

        [DisplayName("主要庫別")]
        public  string MDB013 { get; set; }

        [DisplayName("主供應商")]
        public  string MDB014 { get; set; }

        [DisplayName("庫存管理")]
        public  string MDB015 { get; set; }

        [DisplayName("批號管理")]
        public  string MDB016 { get; set; }

        [DisplayName("DATECODE管理")]
        public  string MDB017 { get; set; }

        [DisplayName("序號管理")]
        public  string MDB018 { get; set; }

        [DisplayName("先進先出")]
        public  string MDB019 { get; set; }

        [DisplayName("生效日期")]
        public  string MDB020 { get; set; }

        [DisplayName("失效日期")]
        public  string MDB021 { get; set; }

        [DisplayName("備註")]
        public  string MDB022 { get; set; }

        [DisplayName("參考料號")]
        public  string MDB023 { get; set; }

        [DisplayName("材積規格(長)")]
        public  decimal? MDB024 { get; set; }

        [DisplayName("材積規格(寬)")]
        public  decimal? MDB025 { get; set; }

        [DisplayName("材積規格(高)")]
        public  decimal? MDB026 { get; set; }

        [DisplayName("材積單位")]
        public  string MDB027 { get; set; }

        [DisplayName("產品圖號")]
        public  string MDB028 { get; set; }

        [DisplayName("圖號版次")]
        public  string MDB029 { get; set; }

        [DisplayName("單位毛重")]
        public  decimal? MDB030 { get; set; }

        [DisplayName("產品等級")]
        public  string MDB031 { get; set; }

        [DisplayName("庫存數量")]
        public  decimal? MDB035 { get; set; }

        [DisplayName("安全存量")]
        public  decimal? MDB036 { get; set; }

        [DisplayName("超期管理")]
        public  string MDB040 { get; set; }

        [DisplayName("保存期限")]
        public  int? MDB041 { get; set; }

        [DisplayName("有效天數")]
        public  int? MDB042 { get; set; }

        [DisplayName("複檢天數")]
        public  int? MDB043 { get; set; }

        [DisplayName("警示天數")]
        public  int? MDB044 { get; set; }

        [DisplayName("最後盤點日期")]
        public  string MDB051 { get; set; }

        [DisplayName("最後入庫日期")]
        public  string MDB052 { get; set; }

        [DisplayName("最後出庫日期")]
        public  string MDB053 { get; set; }

        [DisplayName("最後檢驗日期")]
        public  string MDB054 { get; set; }

        [DisplayName("最後收料日期")]
        public  string MDB055 { get; set; }

        [DisplayName("最後借出日期")]
        public  string MDB056 { get; set; }

        [DisplayName("最後進貨批號")]
        public  string MDB057 { get; set; }

        [DisplayName("最後進貨D/C")]
        public  string MDB058 { get; set; }

        [DisplayName("最後進貨序號")]
        public  string MDB059 { get; set; }

        [DisplayName("最後進貨箱號")]
        public  string MDB060 { get; set; }

        [DisplayName("類型-採購物料")]
        public  string MDB061 { get; set; }

        [DisplayName("類型-倉庫物料")]
        public  string MDB062 { get; set; }

        [DisplayName("類型-銷售物料")]
        public  string MDB063 { get; set; }

        [DisplayName("類型-固定資產")]
        public  string MDB064 { get; set; }

        [DisplayName("創建日期")]
        public  DateTime? CreatedDate { get; set; }

        [DisplayName("創建人")]
        public  string Creator { get; set; }

        [DisplayName("修改日期")]
        public  DateTime? ModifiedDate { get; set; }

        [DisplayName("修改人")]
        public  string Modifier { get; set; }
    }
}
