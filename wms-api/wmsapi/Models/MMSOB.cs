using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMSOB
    {
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [MaxLength(45)]
        public virtual string SOB001 { get; set; }

        [MaxLength(8)]
        public virtual string SOB002 { get; set; }

        [MaxLength(18)]
        public virtual string SOB003 { get; set; }

        [MaxLength(18)]
        public virtual string SOB004 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? SOB021 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? SOB022 { get; set; }

        public virtual DateTime? SOB031 { get; set; }

        public virtual DateTime? SOB032 { get; set; }

        public virtual DateTime? SOB033 { get; set; }

        public virtual DateTime? SOB034 { get; set; }

        public virtual DateTime? SOB035 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}