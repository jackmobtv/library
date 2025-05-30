﻿using DataDomain;
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
        public bool authenticateUser(string email, string password);
        public UserVM loginUser(string email, string password);
        public void editUser(string firstName, string lastName, string old_email, string new_email, string old_password, string new_password);
        public void addUser(string firstName, string lastName, string email, string password);
        public List<UserVM> getAllUsers();
        public void deactivateUser(int userId);
        public void activateUser(int userId);
        public void editEmail(string email, string old_email);
        public void editPassword(string password, string email);
        public void editName(string firstname, string lastname, string email);
    }
}
