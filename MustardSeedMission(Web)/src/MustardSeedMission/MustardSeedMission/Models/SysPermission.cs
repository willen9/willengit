namespace MustardSeedMission.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysPermission")]
    public partial class SysPermission
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string GroupID { get; set; }

        [StringLength(50)]
        public string Creator { get; set; }

        public virtual Users User { get; set; }
    }
}
