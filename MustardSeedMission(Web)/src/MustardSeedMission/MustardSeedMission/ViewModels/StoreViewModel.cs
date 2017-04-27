using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MustardSeedMission.ViewModels
{
    public class StoreViewModel
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public int? ZoneCode { get; set; }

        public int? SerialNo { get; set; }

        public int? BusinessCode { get; set; }

        public int? ModificationCode { get; set; }

        public DateTime? ModificationDate { get; set; }

        public DateTime? DevelopDate { get; set; }

        public string Developer { get; set; }

        public int? DBC { get; set; }

        public bool? Sunday { get; set; }

        public bool? Monday { get; set; }

        public bool? Tuesday { get; set; }

        public bool? Wednesday { get; set; }

        public bool? Thursday { get; set; }

        public bool? Friday { get; set; }

        public bool? Saturday { get; set; }

        public string ReceiptZIP { get; set; }

        public string ReceipAddress { get; set; }

        public string ZIP { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }

        public string Description { get; set; }

        public DateTime? LastTime { get; set; }

        public object FSTime { get; set; }

        public object FETime { get; set; }

        public object SSTime { get; set; }

        public object SETime { get; set; }

        public string Status { get; set; }

        public int? Level { get; set; }

        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Updater { get; set; }

        public DateTime? updateTime { get; set; }

        public int? RegionCode { get; set; }

        public string RegionName { get; set; }

        public string ZoneName { get; set; }

        public string BusinessName { get; set; }

        public string ModificationName { get; set; }
    }
}