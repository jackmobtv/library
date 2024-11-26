using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IUserManager
    {
        public UserVM getUserByEmail(string email);
        public List<string> getRolesByUserId(int userId);
    }
}
