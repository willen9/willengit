using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDNA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DNA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DNA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DNA003 { get; set; }

        [MaxLength(8)]
        public virtual string DNA004 { get; set; }

        [MaxLength(16)]
        public virtual string DNA005 { get; set; }

        [MaxLength(16)]
        public virtual string DNA006 { get; set; }

        [MaxLength(12)]
        public virtual string DNA007 { get; set; }

        [MaxLength(30)]
        public virtual string DNA010 { get; set; }

        [MaxLength(8)]
        public virtual string DNA011 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DNA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DNA016 { get; set; }

        [MaxLength(255)]
        public virtual string DNA020 { get; set; }

        [MaxLength(8)]
        public virtual string DNA030 { get; set; }

        public virtual int? DNA041 { get; set; }

        public virtual int? DNA042 { get; set; }

        [MaxLength(10)]
        public virtual string DNA051 { get; set; }

        [MaxLength(60)]
        public virtual string DNA052 { get; set; }

        [MaxLength(8)]
        public virtual string DNA053 { get; set; }

        [MaxLength(8)]
        public virtual string DNA054 { get; set; }

        [MaxLength(12)]
        public virtual string DNA055 { get; set; }

        [MaxLength(35)]
        public virtual string DNA056 { get; set; }

        [MaxLength(100)]
        public virtual string DNA057 { get; set; }

        [MaxLength(3)]
        public virtual string DNA701 { get; set; }

        [MaxLength(15)]
        public virtual string DNA702 { get; set; }

        public virtual Guid? DNA800 { get; set; }

        public virtual Guid? DNA801 { get; set; }

        [MaxLength(3)]
        public virtual string DNA851 { get; set; }

        [MaxLength(15)]
        public virtual string DNA852 { get; set; }

        [MaxLength(3)]
        public virtual string DNA854 { get; set; }

        [MaxLength(15)]
        public virtual string DNA855 { get; set; }

        [MaxLength(3)]
        public virtual string DNA857 { get; set; }

        [MaxLength(15)]
        public virtual string DNA858 { get; set; }

        [MaxLength(3)]
        public virtual string DNA992 { get; set; }

        public virtual int? DNA993 { get; set; }

        public virtual int? DNA994 { get; set; }

        [MaxLength(12)]
        public virtual string DNA995 { get; set; }

        [MaxLength(3)]
        public virtual string DNA996 { get; set; }

        [MaxLength(8)]
        public virtual string DNA997 { get; set; }

        [MaxLength(16)]
        public virtual string DNA928 { get; set; }

        [MaxLength(1)]
        public virtual string DNA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}