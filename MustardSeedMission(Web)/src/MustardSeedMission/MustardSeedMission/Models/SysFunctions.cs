namespace MustardSeedMission.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SysFunctions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysFunctions()
        {
            Groups = new HashSet<Groups>();
        }

        [Key]
        [StringLength(50)]
        public string SysFunctionsID { get; set; }

        [StringLength(50)]
        public string SysFunctionsName { get; set; }

        [StringLength(50)]
        public string Creator { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Groups> Groups { get; set; }
    }
}
