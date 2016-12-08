using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMSOC
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(45)]
        public virtual string SOC001 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string SOC002 { get; set; }

        [Required]
        [MaxLength(18)]
        public virtual string SOC003 { get; set; }

        [Required]
        [MaxLength(18)]
        public virtual string SOC004 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string SOC005 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? SOC021 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? SOC022 { get; set; }

        public virtual DateTime? SOC031 { get; set; }

        public virtual DateTime? SOC032 { get; set; }

        public virtual DateTime? SOC033 { get; set; }

        public virtual DateTime? SOC034 { get; set; }

        public virtual DateTime? SOC035 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}