using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }

    public class UserVM : User
    {
        public List<string> Roles { get; set; }

        public bool isLibrarian()
        {
            if(Roles is null)
            {
                return false;
            }

            return Roles.Contains("Librarian") || Roles.Contains("Admin");
        }

        public bool isAdmin()
        {
            if (Roles is null)
            {
                return false;
            }

            return Roles.Contains("Admin");
        }
    }
}
