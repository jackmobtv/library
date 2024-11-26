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
    }
}
