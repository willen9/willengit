using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDLA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DLA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DLA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DLA003 { get; set; }

        [MaxLength(8)]
        public virtual string DLA004 { get; set; }

        [MaxLength(16)]
        public virtual string DLA005 { get; set; }

        [MaxLength(16)]
        public virtual string DLA006 { get; set; }

        [MaxLength(12)]
        public virtual string DLA007 { get; set; }

        [MaxLength(60)]
        public virtual string DLA008 { get; set; }

        [MaxLength(30)]
        public virtual string DLA009 { get; set; }

        [MaxLength(30)]
        public virtual string DLA010 { get; set; }

        [MaxLength(8)]
        public virtual string DLA011 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DLA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DLA016 { get; set; }

        [MaxLength(255)]
        public virtual string DLA020 { get; set; }

        [MaxLength(8)]
        public virtual string DLA030 { get; set; }

        public virtual int? DLA041 { get; set; }

        public virtual int? DLA042 { get; set; }

        public virtual Guid? DLA800 { get; set; }

        public virtual Guid? DLA801 { get; set; }

        [MaxLength(3)]
        public virtual string DLA851 { get; set; }

        [MaxLength(15)]
        public virtual string DLA852 { get; set; }

        [MaxLength(3)]
        public virtual string DLA854 { get; set; }

        [MaxLength(15)]
        public virtual string DLA855 { get; set; }

        [MaxLength(3)]
        public virtual string DLA857 { get; set; }

        [MaxLength(15)]
        public virtual string DLA858 { get; set; }

        [MaxLength(3)]
        public virtual string DLA992 { get; set; }

        public virtual int? DLA993 { get; set; }

        public virtual int? DLA994 { get; set; }

        [MaxLength(5)]
        public virtual string DLA995 { get; set; }

        [MaxLength(3)]
        public virtual string DLA996 { get; set; }

        [MaxLength(8)]
        public virtual string DLA997 { get; set; }

        [MaxLength(16)]
        public virtual string DLA928 { get; set; }

        [MaxLength(1)]
        public virtual string DLA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}