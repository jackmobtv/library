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

        private string HashSha256(string password)
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
            catch (ArgumentException ex)
            {
                throw new ArgumentException("User Not Found", ex);
            }

            return userVM;
        }

        public List<string> getRolesByUserId(int userId)
        {
            return _userAccessor.selectRolesByUserId(userId);
        }

        public bool authenticateUser(string email, string password)
        {
            try
            {
                bool result = false;
                string passwordhash = HashSha256(password);
                _userAccessor.selectUserByEmailAndPasswordHash(email, passwordhash);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("User Not Found", ex);
            }

            return true;
        }

        public UserVM loginUser(string email, string password)
        {
            UserVM user = null;

            try
            {
                if (authenticateUser(email, password))
                {
                    user = _userAccessor.selectUserByEmail(email);
                    user.Roles = _userAccessor.selectRolesByUserId(user.UserId);
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Bad Username or Password", ex);
            }

            return user;
        }

        public void editUser(string firstName, string lastName, string old_email, string new_email, string old_password, string new_password)
        {
            bool success = false;

            string old_passwordHash = HashSha256(old_password);
            string new_passwordHash = HashSha256(new_password);

            try
            {
                if(old_email != new_email)
                {
                    try
                    {
                        _userAccessor.selectUserByEmail(new_email);
                    }
                    catch (ArgumentException)
                    {
                        _userAccessor.updateUser(firstName, lastName, old_email, new_email, old_passwordHash, new_passwordHash);
                        success = true;
                    }
                    if (!success)
                    {
                        throw new ArgumentException("Email is Already in Use");
                    }
                }
                else
                {
                    _userAccessor.updateUser(firstName, lastName, old_email, new_email, old_passwordHash, new_passwordHash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addUser(string firstName, string lastName, string email, string password)
        {
            bool success = false;

            try
            {
                try
                {
                    _userAccessor.selectUserByEmail(email);
                }
                catch (ArgumentException)
                {
                    string passwordHash = HashSha256(password);
                    _userAccessor.insertUser(firstName, lastName, email, passwordHash);
                    success = true;
                }
                if (!success)
                {
                    throw new ArgumentException("Email is Already in Use");
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }
    }
}
