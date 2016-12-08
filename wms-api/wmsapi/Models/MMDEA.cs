using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDEA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DEA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DEA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DEA003 { get; set; }

        [MaxLength(8)]
        public virtual string DEA004 { get; set; }

        [MaxLength(16)]
        public virtual string DEA005 { get; set; }

        [MaxLength(16)]
        public virtual string DEA006 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DEA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DEA016 { get; set; }

        [MaxLength(255)]
        public virtual string DEA020 { get; set; }

        [MaxLength(8)]
        public virtual string DEA030 { get; set; }

        public virtual int? DEA041 { get; set; }

        public virtual int? DEA042 { get; set; }

        public virtual Guid? DEA800 { get; set; }

        public virtual Guid? DEA801 { get; set; }

        [MaxLength(3)]
        public virtual string DEA851 { get; set; }

        [MaxLength(15)]
        public virtual string DEA852 { get; set; }

        [MaxLength(3)]
        public virtual string DEA854 { get; set; }

        [MaxLength(15)]
        public virtual string DEA855 { get; set; }

        [MaxLength(3)]
        public virtual string DEA857 { get; set; }

        [MaxLength(15)]
        public virtual string DEA858 { get; set; }

        [MaxLength(3)]
        public virtual string DEA992 { get; set; }

        public virtual int? DEA993 { get; set; }

        public virtual int? DEA994 { get; set; }

        [MaxLength(12)]
        public virtual string DEA995 { get; set; }

        [MaxLength(3)]
        public virtual string DEA996 { get; set; }

        [MaxLength(8)]
        public virtual string DEA997 { get; set; }

        [MaxLength(16)]
        public virtual string DEA998 { get; set; }

        [MaxLength(1)]
        public virtual string DEA999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}