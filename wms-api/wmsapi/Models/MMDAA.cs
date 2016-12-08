using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDAA
    {
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(2)]
        public virtual string DAA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DAA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DAA003 { get; set; }

        [MaxLength(8)]
        public virtual string DAA004 { get; set; }

        [MaxLength(16)]
        public virtual string DAA005 { get; set; }

        [MaxLength(16)]
        public virtual string DAA006 { get; set; }

        [MaxLength(12)]
        public virtual string DAA007 { get; set; }

        [MaxLength(60)]
        public virtual string DAA008 { get; set; }

        [MaxLength(30)]
        public virtual string DAA009 { get; set; }

        [MaxLength(30)]
        public virtual string DAA010 { get; set; }

        [MaxLength(8)]
        public virtual string DAA011 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DAA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DAA016 { get; set; }

        [MaxLength(255)]
        public virtual string DAA020 { get; set; }

        [MaxLength(8)]
        public virtual string DAA030 { get; set; }

        public virtual int? DAA041 { get; set; }

        public virtual int? DAA042 { get; set; }

        public virtual Guid? DAA800 { get; set; }

        public virtual Guid? DAA801 { get; set; }

        [MaxLength(3)]
        public virtual string DAA851 { get; set; }

        [MaxLength(15)]
        public virtual string DAA852 { get; set; }

        [MaxLength(3)]
        public virtual string DAA854 { get; set; }

        [MaxLength(15)]
        public virtual string DAA855 { get; set; }

        [MaxLength(3)]
        public virtual string DAA857 { get; set; }

        [MaxLength(15)]
        public virtual string DAA858 { get; set; }

        [MaxLength(3)]
        public virtual string DAA992 { get; set; }

        public virtual int? DAA993 { get; set; }

        public virtual int? DAA994 { get; set; }

        [MaxLength(5)]
        public virtual string DAA995 { get; set; }

        [MaxLength(3)]
        public virtual string DAA996 { get; set; }

        [MaxLength(8)]
        public virtual string DAA997 { get; set; }

        [MaxLength(16)]
        public virtual string DAA998 { get; set; }

        [MaxLength(1)]
        public virtual string DAA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}