using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;

namespace Utilities.Security
{
    public class AccountChecker
    {
        public static bool IsInRole(string role, ref string error)
        {
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                try
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, IdentityType.SamAccountName, role);
                    UserPrincipal user = UserPrincipal.Current;
                    return IsInGroup(user, group);
                }
                catch (Exception ex)
                {
                    error = ex.Message.ToString();
                    return false;
                }
            }
        }

        public static bool IsInRole(string domain, string username, string password, string role, ref string error)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain, username, password))
            {
                try
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, IdentityType.SamAccountName, role);
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);
                    return IsInGroup(user, group);
                }
                catch (Exception ex)
                {
                    error = ex.Message.ToString();
                    return false;
                }
            }
        }

        public static bool IsInRole(string domain, string username, string role, ref string error)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                try
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, IdentityType.SamAccountName, role);
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);
                    return IsInGroup(user, group);
                }
                catch (Exception ex)
                {
                    error = ex.Message.ToString();
                    return false;
                }
            }
        }

        public static Person GetPerson(string domainName, string racf)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domainName);
            return Person.FindByIdentity(ctx, IdentityType.SamAccountName, racf);
        }


        public static IList<Person> GetMembers(string domainName, string groupName)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domainName);
            GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, groupName);

            if (grp == null)
            {
                throw new ApplicationException("We did not find that group in that domain, perhaps the group resides in a different domain?");
            }

            IList<Person> members = new List<Person>();


            foreach (Principal p in grp.GetMembers(true))
            {
                Person user = Person.FindByIdentity(ctx, IdentityType.SamAccountName, p.SamAccountName);
                user.GetManager();
                members.Add(user); //You can add more attributes, samaccountname, UPN, DN, object type, etc... 
            }
            grp.Dispose();
            ctx.Dispose();

            return members;
        }


        #region private 
        private static bool IsInGroup(Principal principal, GroupPrincipal group)
        {
            if (principal.IsMemberOf(group))
                return true;

            foreach (var g in principal.GetGroups())
            {
                if (IsInGroup(g, group))
                    return true;
            }

            return false;
        }
        #endregion private


    }
}
