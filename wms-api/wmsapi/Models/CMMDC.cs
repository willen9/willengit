using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class CMMDC
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string MDC001 { get; set; }

        [MaxLength(20)]
        public virtual string MDC002 { get; set; }

        [MaxLength(16)]
        public virtual string MDC003 { get; set; }

        [MaxLength(255)]
        public virtual string MDC004 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}