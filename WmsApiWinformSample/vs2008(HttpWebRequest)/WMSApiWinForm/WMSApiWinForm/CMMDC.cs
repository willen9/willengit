using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMSApiWinForm
{
    public class CMMDC
    {
        public virtual string SYSID { get; set; }

        public virtual string MDC001 { get; set; }

        public virtual string MDC002 { get; set; }

        public virtual string MDC003 { get; set; }

        public virtual string MDC004 { get; set; }

        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}