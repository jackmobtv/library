using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class UserAccessorFakes : IUserAccessor
    {
        private List<UserVM> _users;
        private List<string> _passwordhashes;
        public UserAccessorFakes()
        {
            _users = new List<UserVM>();

            _users.Add(new UserVM()
            {
                UserId = 100000,
                FirstName = "A",
                LastName = "AA",
                Email = "a@test.com",
                Active = true,
                Roles = new List<string> { "Role1", "ROLE2" }
            });
            _users.Add(new UserVM()
            {
                UserId = 100001,
                FirstName = "B",
                LastName = "BB",
                Email = "b@test.com",
                Active = true,
                Roles = new List<string> { "Role1" }
            });
            _users.Add(new UserVM()
            {
                UserId = 100002,
                FirstName = "C",
                LastName = "CC",
                Email = "c@test.com",
                Active = false,
                Roles = new List<string> { "Role1", "ROLE2" }
            });
            _users.Add(new UserVM()
            {
                UserId = 100003,
                FirstName = "D",
                LastName = "DD",
                Email = "d@test.com",
                Active = true,
                Roles = new List<string> {  }
            });
            _users.Add(new UserVM()
            {
                UserId = 100004,
                FirstName = "E",
                LastName = "EE",
                Email = "e@test.com",
                Active = false,
                Roles = new List<string> {  }
            });

            _passwordhashes = new List<string>();
            _passwordhashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            _passwordhashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            _passwordhashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            _passwordhashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            _passwordhashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
        }

        public void insertUser(string firstName, string lastName, string email, string passwordHash)
        {
            _users.Add(new UserVM()
            {
                UserId = 100005,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Active = true,
                Roles = new List<string> { "User" }
            });

            _passwordhashes.Add(passwordHash);
        }

        public void updateUser(string firstName, string lastName, string old_email, string new_email, string old_passwordHash, string new_passwordHash)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Email == old_email || _passwordhashes[i] == old_passwordHash)
                {
                    _users[i].FirstName = firstName;
                    _users[i].LastName = lastName;
                    _users[i].Email = new_email;
                    _passwordhashes[i] = new_passwordHash;
                }
            }
        }

        public List<string> selectRolesByUserId(int userId)
        {
            List<string> roles = null;

            foreach (var user in _users)
            {
                if(user.UserId == userId) 
                { 
                    roles = user.Roles; 
                    break; 
                }
            }
            if (roles == null)
            {
                roles = new List<string>();
            }

            return roles;
        }

        public UserVM selectUserByEmail(string email)
        {
            foreach (var user in _users)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            throw new ArgumentException("User Not Found");
        }

        public UserVM selectUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            UserVM user = null;
            for(int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Email == email && _passwordhashes[i] == passwordHash && _users[i].Active)
                {
                    user = _users[i];
                }
            }
            if (user == null)
            {
                throw new ArgumentException("User Not Found");
            }
            return user;
        }
    }
}
