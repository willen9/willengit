using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class CMMDQ
    {
        [Required]
        [MaxLength(8)]
        public virtual string MDQ001 { get; set; }

        [Required]
        [MaxLength(18)]
        public virtual string MDQ002 { get; set; }

        [MaxLength(40)]
        public virtual string MDQ003 { get; set; }

        [MaxLength(255)]
        public virtual string MDQ015 { get; set; }

        [MaxLength(30)]
        public virtual string MDQ016 { get; set; }

        [MaxLength(20)]
        public virtual string MDQ017 { get; set; }

        public virtual bool? MDQ031 { get; set; }

        public virtual bool? MDQ032 { get; set; }

        public virtual int? MDQ033 { get; set; }

        public virtual int? MDQ034 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}