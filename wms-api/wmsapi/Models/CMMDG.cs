using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class CMMDG
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(12)]
        public virtual string MDG001 { get; set; }

        [MaxLength(20)]
        public virtual string MDG002 { get; set; }

        [MaxLength(60)]
        public virtual string MDG003 { get; set; }

        [MaxLength(60)]
        public virtual string MDG004 { get; set; }

        [MaxLength(12)]
        public virtual string MDG005 { get; set; }

        [MaxLength(30)]
        public virtual string MDG006 { get; set; }

        [MaxLength(3)]
        public virtual string MDG007 { get; set; }

        [MaxLength(16)]
        public virtual string MDG008 { get; set; }

        [MaxLength(16)]
        public virtual string MDG009 { get; set; }

        [MaxLength(255)]
        public virtual string MDG010 { get; set; }

        [MaxLength(30)]
        public virtual string MDG011 { get; set; }

        [MaxLength(30)]
        public virtual string MDG012 { get; set; }

        [MaxLength(20)]
        public virtual string MDG013 { get; set; }

        [MaxLength(20)]
        public virtual string MDG014 { get; set; }

        [MaxLength(20)]
        public virtual string MDG015 { get; set; }

        [MaxLength(100)]
        public virtual string MDG016 { get; set; }

        [MaxLength(8)]
        public virtual string MDG017 { get; set; }

        [MaxLength(8)]
        public virtual string MDG018 { get; set; }

        [MaxLength(8)]
        public virtual string MDG021 { get; set; }

        [MaxLength(100)]
        public virtual string MDG022 { get; set; }

        [MaxLength(30)]
        public virtual string MDG023 { get; set; }

        [MaxLength(8)]
        public virtual string MDG024 { get; set; }

        [MaxLength(100)]
        public virtual string MDG025 { get; set; }

        [MaxLength(30)]
        public virtual string MDG026 { get; set; }

        [MaxLength(8)]
        public virtual string MDG027 { get; set; }

        [MaxLength(100)]
        public virtual string MDG028 { get; set; }

        [MaxLength(30)]
        public virtual string MDG029 { get; set; }

        [MaxLength(8)]
        public virtual string MDG030 { get; set; }

        [MaxLength(100)]
        public virtual string MDG031 { get; set; }

        [MaxLength(30)]
        public virtual string MDG032 { get; set; }

        [MaxLength(12)]
        public virtual string MDG033 { get; set; }

        [MaxLength(30)]
        public virtual string MDG034 { get; set; }

        [MaxLength(12)]
        public virtual string MDG035 { get; set; }

        [MaxLength(30)]
        public virtual string MDG036 { get; set; }

        [MaxLength(12)]
        public virtual string MDG037 { get; set; }

        [MaxLength(12)]
        public virtual string MDG038 { get; set; }

        [MaxLength(12)]
        public virtual string MDG039 { get; set; }

        [MaxLength(12)]
        public virtual string MDG040 { get; set; }

        [MaxLength(8)]
        public virtual string MDG051 { get; set; }

        [MaxLength(8)]
        public virtual string MDG052 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}