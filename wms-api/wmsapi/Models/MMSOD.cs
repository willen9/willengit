using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMSOD
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(45)]
        public virtual string SOD001 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string SOD002 { get; set; }

        [Required]
        [MaxLength(18)]
        public virtual string SOD003 { get; set; }

        [Required]
        [MaxLength(18)]
        public virtual string SOD004 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string SOD005 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? SOD021 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? SOD022 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}