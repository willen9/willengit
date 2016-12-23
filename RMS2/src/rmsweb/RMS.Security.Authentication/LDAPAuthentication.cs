using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Security.Authentication
{
    public class LDAPAuthentication : IDisposable
    {        
        private DirectoryEntry directoryEntry;

        public string BaseDN { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public LDAPAuthentication()
        {
            directoryEntry = new DirectoryEntry();            
        }

        public LDAPUserObject GetUser(string username)
        {
            directoryEntry.AuthenticationType = AuthenticationTypes.None;
            directoryEntry.Path = String.Format("LDAP://{0}", this.BaseDN);
            directoryEntry.Username = this.Username;
            directoryEntry.Password = this.Password;

            DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry);
            directorySearcher.Filter = String.Format("(&(objectClass=user)(sAMAccountName={0}))", username);
            directorySearcher.SearchScope = SearchScope.Subtree;

            SearchResult result = directorySearcher.FindOne();
            if (result != null)
            {
                DirectoryEntry resultEntity = result.GetDirectoryEntry();

                LDAPUserObject UserObject = new LDAPUserObject();
                UserObject.sAMAccountName = resultEntity.Properties["sAMAccountName"].Value.ToString();
                UserObject.ObjectGuid = resultEntity.Guid.ToString();
                UserObject.description = resultEntity.Properties["description"].Value.ToString();
                UserObject.CN = resultEntity.Properties["CN"].Value.ToString();
                UserObject.DN = resultEntity.Properties["DN"].Value.ToString();
                UserObject.givenName = resultEntity.Properties["givenName"].Value.ToString();
                UserObject.sn = resultEntity.Properties["sn"].Value.ToString();
                UserObject.displayName = resultEntity.Properties["displayName"].Value.ToString();
                UserObject.userAccountControl = resultEntity.Properties["userAccountControl"].Value.ToString();
                UserObject.userPrincipleName = resultEntity.Properties["userPrincipleName"].Value.ToString();
                UserObject.Mail = resultEntity.Properties["mail"].Value.ToString();

                return UserObject;
            }

            return null;
        }

        public LDAPUserObject GetUser(string username, string password)
        {
            return null;
        }

        public bool VerifyUser(string username, string password)
        {
            try
            {

            }
            catch(DirectoryServicesCOMException COMException)
            {
                //-2147023570 means unknown user name or bad passowrd.
                if (COMException.ErrorCode == -2147023570)
                {

                }

                return false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            if (directoryEntry != null)
            {
                directoryEntry.Close();
                directoryEntry.Dispose();
            }                
        }
    }
}
