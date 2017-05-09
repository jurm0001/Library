using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace Utilities.Security
{
    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("person")]
    public class Person : UserPrincipal
    {
        // Inplement the constructor using the base class constructor. 
        public Person(PrincipalContext context)
            : base(context)
        {

        }

        // Implement the constructor with initialization parameters.    
        public Person(PrincipalContext context,
                             string samAccountName,
                             string password,
                             bool enabled)
            : base(context,
                   samAccountName,
                   password,
                   enabled)
        {
        }


        private Person _Manager;
        public Person Manager
        {
            get { return _Manager; }
            set { _Manager = value; }
        }

        public UserPrincipal GetManager()
        {
            {
                // get the DirectoryEntry behind the UserPrincipal object
                DirectoryEntry dirEntryForUser = this.GetUnderlyingObject() as DirectoryEntry;

                if (dirEntryForUser != null)
                {
                    // check to see if we have a manager name - if so, grab it
                    if (dirEntryForUser.Properties["manager"] != null)
                    {
                        string managerDN = dirEntryForUser.Properties["manager"][0].ToString();

                        // find the manager UserPrincipal via the managerDN 
                        _Manager = Person.FindByIdentity(this.Context, managerDN);
                    }
                }
            }

            return _Manager;
        }


        [DirectoryProperty("extensionAttribute1")]
        public string ExtensionAttribute1
        {
            get
            {
                if (ExtensionGet("extensionAttribute1").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute1")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute1", value);
            }
        }

        [DirectoryProperty("extensionAttribute2")]
        public string ExtensionAttribute2
        {
            get
            {
                if (ExtensionGet("extensionAttribute2").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute2")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute2", value);
            }
        }

        [DirectoryProperty("extensionAttribute3")]
        public string ExtensionAttribute3
        {
            get
            {
                if (ExtensionGet("extensionAttribute3").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute3")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute3", value);
            }
        }

        [DirectoryProperty("extensionAttribute4")]
        public string ExtensionAttribute4
        {
            get
            {
                if (ExtensionGet("extensionAttribute4").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute4")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute4", value);
            }
        }

        [DirectoryProperty("extensionAttribute5")]
        public string ExtensionAttribute5
        {
            get
            {
                if (ExtensionGet("extensionAttribute5").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute5")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute5", value);
            }
        }

        [DirectoryProperty("extensionAttribute6")]
        public string ExtensionAttribute6
        {
            get
            {
                if (ExtensionGet("extensionAttribute6").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute6")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute6", value);
            }
        }

        [DirectoryProperty("extensionAttribute7")]
        public string ExtensionAttribute7
        {
            get
            {
                if (ExtensionGet("extensionAttribute7").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute7")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute7", value);
            }
        }

        [DirectoryProperty("extensionAttribute8")]
        public string ExtensionAttribute8
        {
            get
            {
                if (ExtensionGet("extensionAttribute8").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute8")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute8", value);
            }
        }

        [DirectoryProperty("extensionAttribute9")]
        public string ExtensionAttribute9
        {
            get
            {
                if (ExtensionGet("extensionAttribute9").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute9")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute9", value);
            }
        }

        [DirectoryProperty("extensionAttribute1")]
        public string ExtensionAttribute10
        {
            get
            {
                if (ExtensionGet("extensionAttribute10").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute10")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute10", value);
            }
        }

        [DirectoryProperty("extensionAttribute11")]
        public string ExtensionAttribute11
        {
            get
            {
                if (ExtensionGet("extensionAttribute11").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute11")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute11", value);
            }
        }

        [DirectoryProperty("extensionAttribute12")]
        public string ExtensionAttribute12
        {
            get
            {
                if (ExtensionGet("extensionAttribute12").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute12")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute12", value);
            }
        }

        [DirectoryProperty("extensionAttribute13")]
        public string ExtensionAttribute13
        {
            get
            {
                if (ExtensionGet("extensionAttribute13").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute13")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute13", value);
            }
        }

        [DirectoryProperty("extensionAttribute14")]
        public string ExtensionAttribute14
        {
            get
            {
                if (ExtensionGet("extensionAttribute14").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute14")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute14", value);
            }
        }

        [DirectoryProperty("extensionAttribute15")]
        public string ExtensionAttribute15
        {
            get
            {
                if (ExtensionGet("extensionAttribute15").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute15")[0];
            }

            set
            {
                ExtensionSet("extensionAttribute15", value);
            }
        }


        // Implement the overloaded search method FindByIdentity.
        public static new Person FindByIdentity(PrincipalContext context,
                                                       string identityValue)
        {
            return (Person)FindByIdentityWithType(context,
                                                         typeof(Person),
                                                         identityValue);
        }

        // Implement the overloaded search method FindByIdentity. 
        public static new Person FindByIdentity(PrincipalContext context,
                                                       IdentityType identityType,
                                                       string identityValue)
        {
            return (Person)FindByIdentityWithType(context,
                                                         typeof(Person),
                                                         identityType,
                                                         identityValue);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("DistinguishedName:{0}", DistinguishedName);
            sb.AppendLine();
            sb.AppendFormat("DisplayName:{0}", DisplayName);
            sb.AppendLine();
            sb.AppendFormat("EmailAddress:{0}", EmailAddress);
            sb.AppendLine();
            sb.AppendFormat("UserPrincipalName:{0}", UserPrincipalName);
            sb.AppendLine();
            sb.AppendFormat("Name:{0}", Name);
            sb.AppendLine();
            sb.AppendFormat("GivenName:{0}", GivenName);
            sb.AppendLine();
            sb.AppendFormat("SamAccountName:{0}", SamAccountName);
            sb.AppendLine();

            sb.AppendFormat("Manager:{0}", _Manager.SamAccountName);
            sb.AppendLine();

            sb.AppendFormat("ManagerName:{0}", _Manager.DisplayName);
            sb.AppendLine();

            sb.AppendFormat("ExtensionAttribute1:{0}", ExtensionAttribute1);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute2:{0}", ExtensionAttribute2);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute3:{0}", ExtensionAttribute3);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute4:{0}", ExtensionAttribute4);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute5:{0}", ExtensionAttribute5);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute6:{0}", ExtensionAttribute6);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute7:{0}", ExtensionAttribute7);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute8:{0}", ExtensionAttribute8);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute9:{0}", ExtensionAttribute9);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute10:{0}", ExtensionAttribute10);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute11:{0}", ExtensionAttribute11);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute12:{0}", ExtensionAttribute12);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute13:{0}", ExtensionAttribute13);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute14:{0}", ExtensionAttribute14);
            sb.AppendLine();
            sb.AppendFormat("ExtensionAttribute15:{0}", ExtensionAttribute15);
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
