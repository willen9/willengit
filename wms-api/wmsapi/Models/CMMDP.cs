using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class CMMDP
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string MDP001 { get; set; }

        [MaxLength(40)]
        public virtual string MDP002 { get; set; }

        [MaxLength(20)]
        public virtual string MDP003 { get; set; }

        [MaxLength(20)]
        public virtual string MDP004 { get; set; }

        [MaxLength(255)]
        public virtual string MDP005 { get; set; }

        [MaxLength(255)]
        public virtual string MDP006 { get; set; }

        [MaxLength(255)]
        public virtual string MDP007 { get; set; }

        [MaxLength(255)]
        public virtual string MDP008 { get; set; }

        [MaxLength(255)]
        public virtual string MDP009 { get; set; }

        [MaxLength(100)]
        public virtual string MDP010 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}