using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class CMMDR
    {
        [Required]
        [MaxLength(8)]
        public virtual string MDR001 { get; set; }

        [Required]
        [MaxLength(18)]
        public virtual string MDR002 { get; set; }

        [Required]
        [MaxLength(18)]
        public virtual string MDR003 { get; set; }

        [MaxLength(40)]
        public virtual string MDR004 { get; set; }

        [MaxLength(1)]
        public virtual string MDR010 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDR011 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDR012 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDR013 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDR014 { get; set; }

        [MaxLength(255)]
        public virtual string MDR015 { get; set; }

        [MaxLength(1)]
        public virtual string MDR020 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDR021 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDR022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDR023 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}