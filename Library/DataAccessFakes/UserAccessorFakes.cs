﻿using DataAccessInterfaces;
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
    }
}