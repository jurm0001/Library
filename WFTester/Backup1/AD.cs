using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace AD
{
    public class ADirectory
    {


        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionstring">connection to AD</param>
        public ADirectory(string connectionstring)
        {
            this._connectionString = connectionstring;
            this._DE = new DirectoryEntry(this._connectionString);
            this._DE.AuthenticationType = AuthenticationTypes.Secure;
            this._err = "";
        }

        public ADirectory()
        {
            this._err = "";
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">user to find</param>
        /// <returns>datatable</returns>
        public DataTable FindUser(string user)
        {
            if (this._DE == null)
            {
                this._err = "DE NULL";
                return null;
            }

            DataTable table = new DataTable();

            try
            {

                DirectorySearcher ds = new DirectorySearcher();
                SearchResult src;
                ds.SearchRoot = this._DE;
                ds.Filter = "sAMAccountName=" + user;

                src = ds.FindOne();
                if (src == null)
                    return null;

                table.Columns.Add("Property");
                table.Columns.Add("Value");

                DirectoryEntry directoryEntry = src.GetDirectoryEntry();

                System.DirectoryServices.PropertyCollection pc = directoryEntry.Properties;
                foreach (string propName in pc.PropertyNames)
                {
                    string t = directoryEntry.Properties[propName].Value.ToString();
                    table.Rows.Add(propName, directoryEntry.Properties[propName].Value.ToString());
                }

            }
            catch (Exception ex)
            {
                this._err = ex.ToString();
                return null;
            }

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">get users groups</param>
        /// <returns>datatable</returns>
        public DataTable getMemberOf(string user)
        {
            if (this._DE == null)
            {
                this._err = "DE NULL";
                return null;
            }

            DataTable table = new DataTable();
            DirectorySearcher dirSearcher = new DirectorySearcher();

            dirSearcher.Filter = "sAMAccountName=" + user; ;
            dirSearcher.PropertiesToLoad.Add("memberOf");

            table.Columns.Add("Group");

            SearchResult result = dirSearcher.FindOne();
            if (result != null)
            {
                int groupCount = result.Properties["memberOf"].Count;
                for (int counter = 0; counter < groupCount; counter++)
                {

                    string dn = (string)result.Properties["memberOf"][counter]; // Assign dn to the record.
                    //dn = dn.Remove(dn.LastIndexOf("CN="));  // Since the record provides raw information remove the unneeded information to get the true group name.
                    //dn = dn.Replace("CN=", ""); // Same as above.
                    table.Rows.Add(dn);
                }
            }
            return table;
        }

        /// <summary>
        /// Method to add a user to a group
        /// </summary>
        /// <param name="de"></param>
        /// <param name="deUser"></param>
        /// <param name="GroupName"></param>
        /// 
        /*
        public static void AddUserToGroup(DirectoryEntry de, DirectoryEntry deUser, string GroupName)
        {
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = de;
            deSearch.Filter = "(&(objectClass=group) (cn=" + GroupName +"))"; 
            SearchResultCollection results = deSearch.FindAll();

            bool isGroupMember = false;

            if (results.Count>0)
            {
                DirectoryEntry group = results[0].GetDirectoryEntry();
                    
                object members = group.Invoke("Members",null);
                foreach ( object member in IEnumerable<object>members){
                    DirectoryEntry x = new DirectoryEntry(member);
                    if (x.Name != deUser.Name)
                    {
                        isGroupMember = false;
                    }
                    else
                    {
                        isGroupMember = true;
                        break;
                    }
                }
                
                if (!isGroupMember){
                    group.Invoke("Add", new object[] {deUser.Path.ToString()});
                }
                
                group.Close();
            }
            return;
        } */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public Boolean userInGroup(string group, string userName)
        {
            DataTable dt = this.getMemberOf(userName);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                string current = dt.Rows[i][0].ToString().Trim();
                if (current.Contains(group))
                    return true;
                //if (group.Trim().Contains(dt.Rows[i][0].ToString().Trim()))
                //    return true;
            }
            return false;

        }

        public List<CN> GetCNList() 
        {
            List<CN> cnList = new List<CN>();

            if (this._DE == null)
            {
                this._err = "DE NULL";
                return null;
            }

            DataTable table = new DataTable();

            try
            {

                DirectorySearcher ds = new DirectorySearcher();
                ds.PageSize = 1000;
                ds.Filter = "(&(objectCategory=person)(objectClass=user))";
                //ds.ServerPageTimeLimit = new TimeSpan(9999);
                //ds.ServerTimeLimit = new TimeSpan(999999);
                ds.PropertiesToLoad.Add("displayName");                             // name
                ds.PropertiesToLoad.Add("sAMAccountName");      // location
                ds.PropertiesToLoad.Add("mail"); 

                SearchResultCollection src;
                ds.SearchRoot = this._DE;

                src = ds.FindAll();
                if (src == null)
                    return null;


                foreach (SearchResult sr in src)
                {
                    CN cnl = new CN();
                    Boolean found1 = false;
                    Boolean found2 = false;
                    Boolean found3 = false;
                    if (sr.Properties["mail"] != null && sr.Properties["mail"].Count > 0)
                    {
                        cnl.Email = sr.Properties["mail"][0].ToString();
                        found1 = true;
                    }
                    if (sr.Properties["displayName"] != null && sr.Properties["displayName"].Count > 0)
                    {
                        cnl.name = sr.Properties["displayName"][0].ToString();
                        found2 = true;
                    }
                    if (sr.Properties["sAMAccountName"] != null && sr.Properties["sAMAccountName"].Count > 0)
                    {
                        cnl.racf = sr.Properties["sAMAccountName"][0].ToString();
                        found3 = true;
                    }
                    
                    if(found1 && found2 && found3)
                        cnList.Add(cnl);
                }               

            }
            catch (Exception ex)
            {
                this._err = ex.ToString();
                return null;
            }
            return cnList;
        }

        public SearchResultCollection GetSearchResultCollectionList() 
        {
            List<CN> cnList = new List<CN>();

            if (this._DE == null)
            {
                this._err = "DE NULL";
                return null;
            }

            DataTable table = new DataTable();

            try
            {

                DirectorySearcher ds = new DirectorySearcher();
                ds.PageSize = 1000;
                ds.Filter = "(&(objectCategory=person)(objectClass=user))";
                //ds.ServerPageTimeLimit = new TimeSpan(9999);
                //ds.ServerTimeLimit = new TimeSpan(999999);
                ds.PropertiesToLoad.Add("displayName"); // name
                ds.PropertiesToLoad.Add("sAMAccountName"); // location
                ds.PropertiesToLoad.Add("mail"); 

                SearchResultCollection src;
                ds.SearchRoot = this._DE;

                src = ds.FindAll();
                if (src == null)
                    return null;

                return src;
                     

            }
            catch (Exception ex)
            {
                this._err = ex.ToString();
                return null;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group">group to find</param>
        /// <returns>datatable</returns>
        public DataTable getGroupUsers(string group)
        {
            if (this._DE == null)
            {
                this._err = "DE NULL";
                return null;
            }
            DirectorySearcher ds = new DirectorySearcher(this._DE);
            ds.Filter = "(&(objectCategory=group)(sAMAccountName=" + group + "))";
            /*
            ds.PropertiesToLoad.Add("givenname");    
            ds.PropertiesToLoad.Add("samaccountname");    
            ds.PropertiesToLoad.Add("sn");    
            ds.PropertiesToLoad.Add("useraccountcontrol");
            */
            SearchResult result = ds.FindOne();
            if (result == null)
                return null;
            DataTable dt = new DataTable();
            dt.Columns.Add("User");
            dt.Columns.Add("sAMAccountName");

            int groupCount = result.Properties["member"].Count;
            for (int counter = 0; counter < groupCount; counter++)
            {

                string dn = (string)result.Properties["member"][counter]; // Assign dn to the record.

                dn = dn.Substring(dn.IndexOf(",CN=") + 4, dn.Length - (dn.IndexOf(",CN=") + 4));  // Since the record provides raw information remove the unneeded information to get the true group name.

                string temp = "";
                int commacount = 0;
                char[] letter = dn.ToCharArray();
                for (int i = 0; i < letter.Length; ++i)
                {
                    if (letter[i] == ',')
                        commacount++;
                    if (commacount == 2)
                        break;
                    if(letter[i] != '\\')
                        temp += letter[i].ToString();
                    
                }


                dt.Rows.Add(temp, getUserProperty(temp, "sAMAccountName"));
            }
            return dt;
        }

         

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">user cn name</param>
        /// <param name="property">property of user</param>
        /// <returns>string</returns>
        public string getUserProperty(string user, string property)
        {
            if (this._DE == null)
            {
                this._err = "DE NULL";
                return null;
            }
            DirectorySearcher ds = new DirectorySearcher(this._DE);
            ds.Filter = "(&(objectCategory=user)(sAMAccountName=" + user + "))";

            ds.PropertiesToLoad.Add(property);
            SearchResult result = ds.FindOne();

            if (result == null)
                return "";

            if (result.Properties[property] == null)
                return "";

            if (result.Properties[property].Count > 0)
            {
                try
                {
                    return (string)result.Properties[property][0];
                }
                catch (Exception ex)
                {
                    return property + " " + ex.ToString();
                }
            }
            return "";
        }


        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum AdsUserFlags
        {
            Script = 1,                          // 0x1
            AccountDisabled = 2,                 // 0x2
            HomeDirectoryRequired = 8,           // 0x8 
            AccountLockedOut = 16,               // 0x10
            PasswordNotRequired = 32,            // 0x20
            PasswordCannotChange = 64,           // 0x40
            EncryptedTextPasswordAllowed = 128,  // 0x80
            TempDuplicateAccount = 256,          // 0x100
            NormalAccount = 512,                 // 0x200
            InterDomainTrustAccount = 2048,      // 0x800
            WorkstationTrustAccount = 4096,      // 0x1000
            ServerTrustAccount = 8192,           // 0x2000
            PasswordDoesNotExpire = 65536,       // 0x10000
            MnsLogonAccount = 131072,            // 0x20000
            SmartCardRequired = 262144,          // 0x40000
            TrustedForDelegation = 524288,       // 0x80000
            AccountNotDelegated = 1048576,       // 0x100000
            UseDesKeyOnly = 2097152,              // 0x200000
            DontRequirePreauth = 4194304,         // 0x400000
            PasswordExpired = 8388608,           // 0x800000
            TrustedToAuthenticateForDelegation = 16777216, // 0x1000000
            NoAuthDataRequired = 33554432        // 0x2000000
        }

        DirectoryEntry _DE;
        string _connectionString;
        string _err;

    }
}
