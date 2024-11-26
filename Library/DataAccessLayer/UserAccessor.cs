using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public List<string> selectRolesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public UserVM selectUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
