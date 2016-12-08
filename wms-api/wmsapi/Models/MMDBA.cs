using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDBA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DBA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DBA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DBA003 { get; set; }

        [MaxLength(8)]
        public virtual string DBA004 { get; set; }

        [MaxLength(16)]
        public virtual string DBA005 { get; set; }

        [MaxLength(16)]
        public virtual string DBA006 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DBA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DBA016 { get; set; }

        [MaxLength(255)]
        public virtual string DBA020 { get; set; }

        [MaxLength(8)]
        public virtual string DBA030 { get; set; }

        public virtual int? DBA041 { get; set; }

        public virtual int? DBA042 { get; set; }

        public virtual Guid? DBA800 { get; set; }

        public virtual Guid? DBA801 { get; set; }

        [MaxLength(3)]
        public virtual string DBA851 { get; set; }

        [MaxLength(15)]
        public virtual string DBA852 { get; set; }

        [MaxLength(3)]
        public virtual string DBA854 { get; set; }

        [MaxLength(15)]
        public virtual string DBA855 { get; set; }

        [MaxLength(3)]
        public virtual string DBA857 { get; set; }

        [MaxLength(15)]
        public virtual string DBA858 { get; set; }

        [MaxLength(3)]
        public virtual string DBA992 { get; set; }

        public virtual int? DBA993 { get; set; }

        public virtual int? DBA994 { get; set; }

        [MaxLength(12)]
        public virtual string DBA995 { get; set; }

        [MaxLength(3)]
        public virtual string DBA996 { get; set; }

        [MaxLength(8)]
        public virtual string DBA997 { get; set; }

        [MaxLength(16)]
        public virtual string DBA998 { get; set; }

        [MaxLength(1)]
        public virtual string DBA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}