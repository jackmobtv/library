using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        public UserVM getUserByEmail(string email);
        public List<string> getRolesByUserId(int userId);
        public UserVM getUserByEmailAndPasswordHash(string email, string passwordHash);
        public void editUser(string firstName, string lastName, string old_email, string new_email, string old_passwordHash, string new_passwordHash);
        public void addUser(string firstName, string lastName, string email, string passwordHash);
    }
}
