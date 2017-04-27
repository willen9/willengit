namespace MustardSeedMission.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Groups
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string GroupId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SysFunctionsID { get; set; }

        public bool? AD { get; set; }

        public bool? De { get; set; }

        public bool? Up { get; set; }

        public bool? Query { get; set; }

        public bool? Ex { get; set; }

        public bool? Im { get; set; }

        public virtual SysFunctions SysFunction { get; set; }
    }
}
