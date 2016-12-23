using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Security.Authentication
{
    public class LDAPUserObject
    {
        public string sAMAccountName { get; set; }
        public string sAMAccountType { get; set; }
        public string ObjectClass { get; set; }
        public string ObjectGuid { get; set; }
        public string givenName { get; set; }
        public string sn { get; set; }
        public string displayName { get; set; }
        public string CN { get; set; }
        public string DN { get; set; }
        public string description { get; set; }
        public string userAccountControl { get; set; }
        public string userPrincipleName { get; set; }
        public string Mail { get; set; }
    }
}
