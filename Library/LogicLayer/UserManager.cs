using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessFakes;
using DataAccessLayer;
using DataDomain;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public string HashSha256(string password)
        {
            if (password == null || password == "")
            {
                throw new ArgumentException("MISSING INPUT");
            }

            string result = "";
            byte[] data;

            using (SHA256 sha256provider = SHA256.Create())
            {
                data = sha256provider.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString().ToLower();

            return result;
        }

        public UserVM getUserByEmail(string email)
        {
            UserVM userVM = null;

            try
            {
                userVM = _userAccessor.selectUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Employee Not Found", ex);
            }

            return userVM;
        }

        public List<string> getRolesByUserId(int userId)
        {
            return _userAccessor.selectRolesByUserId(userId);
        }
    }
}
