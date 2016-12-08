using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class CMMDX
    {
        [Required]
        [MaxLength(5)]
        public virtual string MDX001 { get; set; }

        [MaxLength(12)]
        public virtual string MDX002 { get; set; }

        [MaxLength(20)]
        public virtual string MDX003 { get; set; }

        [MaxLength(5)]
        public virtual string MDX004 { get; set; }

        public virtual int? MDX005 { get; set; }

        [MaxLength(255)]
        public virtual string MDX010 { get; set; }

        public virtual bool? MDX011 { get; set; }

        public virtual bool? MDX012 { get; set; }

        public virtual bool? MDX013 { get; set; }

        public virtual bool? MDX014 { get; set; }

        public virtual bool? MDX015 { get; set; }

        public virtual bool? MDX016 { get; set; }

        [MaxLength(35)]
        public virtual string MDX035 { get; set; }

        [MaxLength(35)]
        public virtual string MDX036 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}