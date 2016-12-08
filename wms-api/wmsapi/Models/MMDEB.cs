using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDEB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DEB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DEB002 { get; set; }

        [Required]
        [MaxLength(4)]
        public virtual string DEB003 { get; set; }

        [Required]
        [MaxLength(35)]
        public virtual string DEB004 { get; set; }

        [MaxLength(100)]
        public virtual string DEB005 { get; set; }

        [MaxLength(100)]
        public virtual string DEB006 { get; set; }

        [MaxLength(15)]
        public virtual string DEB007 { get; set; }

        [MaxLength(15)]
        public virtual string DEB008 { get; set; }

        [MaxLength(8)]
        public virtual string DEB011 { get; set; }

        [MaxLength(12)]
        public virtual string DEB012 { get; set; }

        [MaxLength(12)]
        public virtual string DEB013 { get; set; }

        [MaxLength(8)]
        public virtual string DEB016 { get; set; }

        [MaxLength(12)]
        public virtual string DEB017 { get; set; }

        [MaxLength(12)]
        public virtual string DEB018 { get; set; }

        [MaxLength(255)]
        public virtual string DEB020 { get; set; }

        [MaxLength(5)]
        public virtual string DEB021 { get; set; }

        [MaxLength(5)]
        public virtual string DEB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DEB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DEB026 { get; set; }

        [MaxLength(8)]
        public virtual string DEB034 { get; set; }

        [MaxLength(8)]
        public virtual string DEB035 { get; set; }

        public virtual Guid? DEB800 { get; set; }

        public virtual Guid? DEB801 { get; set; }

        [MaxLength(3)]
        public virtual string DEB851 { get; set; }

        [MaxLength(15)]
        public virtual string DEB852 { get; set; }

        [MaxLength(4)]
        public virtual string DEB853 { get; set; }

        [MaxLength(3)]
        public virtual string DEB854 { get; set; }

        [MaxLength(15)]
        public virtual string DEB855 { get; set; }

        [MaxLength(4)]
        public virtual string DEB856 { get; set; }

        [MaxLength(3)]
        public virtual string DEB857 { get; set; }

        [MaxLength(15)]
        public virtual string DEB858 { get; set; }

        [MaxLength(4)]
        public virtual string DEB859 { get; set; }

        [MaxLength(3)]
        public virtual string DEB992 { get; set; }

        public virtual int? DEB993 { get; set; }

        public virtual int? DEB994 { get; set; }

        [MaxLength(12)]
        public virtual string DEB995 { get; set; }

        [MaxLength(3)]
        public virtual string DEB996 { get; set; }

        [MaxLength(8)]
        public virtual string DEB997 { get; set; }

        [MaxLength(16)]
        public virtual string DEB998 { get; set; }

        [MaxLength(1)]
        public virtual string DEB999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}