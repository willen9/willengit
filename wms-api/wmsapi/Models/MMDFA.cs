using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDFA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DFA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DFA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DFA003 { get; set; }

        [MaxLength(8)]
        public virtual string DFA004 { get; set; }

        [MaxLength(16)]
        public virtual string DFA006 { get; set; }

        [MaxLength(255)]
        public virtual string DFA020 { get; set; }

        [MaxLength(8)]
        public virtual string DFA030 { get; set; }

        public virtual Guid? DFA800 { get; set; }

        public virtual Guid? DFA801 { get; set; }

        [MaxLength(3)]
        public virtual string DFA851 { get; set; }

        [MaxLength(15)]
        public virtual string DFA852 { get; set; }

        [MaxLength(3)]
        public virtual string DFA854 { get; set; }

        [MaxLength(15)]
        public virtual string DFA855 { get; set; }

        [MaxLength(3)]
        public virtual string DFA857 { get; set; }

        [MaxLength(15)]
        public virtual string DFA858 { get; set; }

        [MaxLength(3)]
        public virtual string DFA992 { get; set; }

        public virtual int? DFA993 { get; set; }

        public virtual int? DFA994 { get; set; }

        [MaxLength(12)]
        public virtual string DFA995 { get; set; }

        [MaxLength(3)]
        public virtual string DFA996 { get; set; }

        [MaxLength(8)]
        public virtual string DFA997 { get; set; }

        [MaxLength(16)]
        public virtual string DFA998 { get; set; }

        [MaxLength(1)]
        public virtual string DFA999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}