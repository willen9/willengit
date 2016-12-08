using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class CMMDF
    {
        [Required]
        [MaxLength(10)]
        public string SYSID { get; set; }

        [Required]
        [MaxLength(12)]
        public string MDF001 { get; set; }

        [MaxLength(20)]
        public string MDF002 { get; set; }

        [MaxLength(255)]
        public string MDF003 { get; set; }

        public DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public string Creator { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [MaxLength(100)]
        public string Modifier { get; set; }
    }
}
