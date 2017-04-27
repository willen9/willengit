using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MustardSeedMission.ViewModels
{
    public class DonationHeadViewModel
    {
        public string DocEntry { get; set; }

        public int? PlanCode { get; set; }

        public string Operator { get; set; }

        public DateTime? WorkDay { get; set; }

        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Updater { get; set; }

        public DateTime? updateTime { get; set; }

        public string PlanName { get; set; }

        public string OperatorName { get; set; }

        public string CreatorName { get; set; }

        public string UpdaterName { get; set; }
    }
}