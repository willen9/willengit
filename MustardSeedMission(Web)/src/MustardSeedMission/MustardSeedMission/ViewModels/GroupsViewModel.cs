using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MustardSeedMission.ViewModels
{
    public class GroupsViewModel
    {
        public string GroupId { get; set; }

        public string SysFunctionsID { get; set; }

        public string SysFunctionsName { get; set; }

        public bool? AD { get; set; }

        public bool? De { get; set; }

        public bool? Up { get; set; }

        public bool? Query { get; set; }

        public bool? Ex { get; set; }

        public bool? Im { get; set; }
    }
}