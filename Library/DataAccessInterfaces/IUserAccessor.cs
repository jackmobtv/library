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
        public UserVM selectUserByEmail(string email);
        public List<string> selectRolesByUserId(int userId);
        public UserVM selectUserByEmailAndPasswordHash(string email, string passwordHash);
        public void updateUser(string firstName, string lastName, string old_email, string new_email, string old_passwordHash, string new_passwordHash);
        public void insertUser(string firstName, string lastName, string email, string passwordHash);
    }
}
