using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //維護合約單頭資料檔
    public class Contract_H
    {
        //公司別
        public string Company { get; set; }

        //群組
        public string UserGroup { get; set; }

        //建立人員
        public string Creator { get; set; }

        //建立日期
        public string CreateDate { get; set; }

        //修改人員
        public string Modifier { get; set; }

        //修改日期
        public string ModiDate { get; set; }

        //合約單別
        public string ContractType { get; set; }

        //合約單別名稱
        public string OrderSName { get; set; }

        //合約單號
        public string ContractNo { get; set; }

        //單據日期
        public string OrderDate { get; set; }

        //合約日期
        public string ConfirmedDate { get; set; }

        //版次
        public string Version { get; set; }

        //定保次數
        public double RoutineServiceFreq { get; set; }

        //外出服務
        public double D2DServiceFreq { get; set; }

        //零件折扣
        public double Discount { get; set; }

        //合約費用
        public double ContractPrice { get; set; }

        //保固期限
        public double WarrantyPeriod { get; set; }

        //合約起日
        public string ContractSDate { get; set; }

        //合約迄日
        public string ContractEDate { get; set; }

        //續約否
        public string Renewal { get; set; }

        //確認碼
        public string Confirmed { get; set; }

        //確認人員
        public string ConfirmedMan { get; set; }

        //確認人員名稱
        public string ConfirmedManName { get; set; }

        //備註
        public string Remark { get; set; }

        //客戶代號
        public string CustomerId { get; set; }

        //客戶簡稱
        public string CustomerName { get; set; }

        //客戶型態
        public string CustomerType { get; set; }

        //地址簡稱
        public string AddressSName { get; set; }

        //地址
        public string Address { get; set; }

        //聯絡人
        public string Contact { get; set; }

        //聯絡人名稱
        public string ContactName { get; set; }

        //電話
        public string Tel { get; set; }

        //傳真
        public string Fax { get; set; }

        //E-mail
        public string Email { get; set; }
    }
}