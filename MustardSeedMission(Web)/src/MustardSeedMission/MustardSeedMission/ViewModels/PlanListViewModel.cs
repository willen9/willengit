using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MustardSeedMission.ViewModels
{
    public class PlanListViewModel
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public int StoreCode { get; set; }

        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Updater { get; set; }

        public DateTime? updateTime { get; set; }

        public int? RegionCode { get; set; }

        public string RegionName { get; set; }

        public int? ZoneCode { get; set; }

        public string ZoneName { get; set; }

        public string StoreName  { get; set; }

        public int NumberOld { get; set; }

        public int StoreCodeOld { get; set; }
    }
}