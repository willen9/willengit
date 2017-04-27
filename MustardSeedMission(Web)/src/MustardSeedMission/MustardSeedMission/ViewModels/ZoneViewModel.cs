using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MustardSeedMission.Models;

namespace MustardSeedMission.ViewModels
{
    public class ZoneViewModel
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public int? RegionCode { get; set; }

        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Updater { get; set; }

        public DateTime? updateTime { get; set; }

        public string RName { get; set; }
    }
}