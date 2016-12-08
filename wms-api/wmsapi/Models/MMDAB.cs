using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDAB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DAB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DAB002 { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string DAB003 { get; set; }

        [MaxLength(35)]
        public virtual string DAB004 { get; set; }

        [MaxLength(100)]
        public virtual string DAB005 { get; set; }

        [MaxLength(100)]
        public virtual string DAB006 { get; set; }

        [MaxLength(8)]
        public virtual string DAB007 { get; set; }

        [MaxLength(12)]
        public virtual string DAB008 { get; set; }

        [MaxLength(12)]
        public virtual string DAB009 { get; set; }

        [MaxLength(15)]
        public virtual string DAB010 { get; set; }

        [MaxLength(15)]
        public virtual string DAB011 { get; set; }

        [MaxLength(255)]
        public virtual string DAB020 { get; set; }

        [MaxLength(5)]
        public virtual string DAB021 { get; set; }

        [MaxLength(5)]
        public virtual string DAB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DAB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DAB026 { get; set; }

        [MaxLength(8)]
        public virtual string DAB034 { get; set; }

        [MaxLength(8)]
        public virtual string DAB035 { get; set; }

        [MaxLength(8)]
        public virtual string DAB036 { get; set; }

        [MaxLength(8)]
        public virtual string DAB051 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DAB052 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DAB053 { get; set; }

        [MaxLength(8)]
        public virtual string DAB054 { get; set; }

        public virtual Guid? DAB800 { get; set; }

        public virtual Guid? DAB801 { get; set; }

        [MaxLength(3)]
        public virtual string DAB851 { get; set; }

        [MaxLength(15)]
        public virtual string DAB852 { get; set; }

        [MaxLength(4)]
        public virtual string DAB853 { get; set; }

        [MaxLength(3)]
        public virtual string DAB854 { get; set; }

        [MaxLength(15)]
        public virtual string DAB855 { get; set; }

        [MaxLength(4)]
        public virtual string DAB856 { get; set; }

        [MaxLength(3)]
        public virtual string DAB857 { get; set; }

        [MaxLength(15)]
        public virtual string DAB858 { get; set; }

        [MaxLength(4)]
        public virtual string DAB859 { get; set; }

        [MaxLength(3)]
        public virtual string DAB992 { get; set; }

        public virtual int? DAB993 { get; set; }

        public virtual int? DAB994 { get; set; }

        [MaxLength(5)]
        public virtual string DAB995 { get; set; }

        [MaxLength(3)]
        public virtual string DAB996 { get; set; }

        [MaxLength(8)]
        public virtual string DAB997 { get; set; }

        [MaxLength(16)]
        public virtual string DAB998 { get; set; }

        [MaxLength(1)]
        public virtual string DAB999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}