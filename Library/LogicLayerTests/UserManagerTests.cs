using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataDomain;
using LogicLayer;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        UserManager? _userManager;

        [TestInitialize]
        public void testStartup()
        {
            _userManager = new UserManager(new UserAccessorFakes());
        }

        [TestMethod]
        public void testGetUserByEmailWithValidEmail()
        {
            const string email = "a@test.com";
            const string expectedValue = email;
            string actualValue;
            actualValue = _userManager.getUserByEmail(email).Email;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testGetUserByEmailWithInvalidEmail()
        {
            const string email = "faek@test.com";

            _userManager.getUserByEmail(email);
        }

        [TestMethod]
        public void testGetRolesByUserIdWithRoles()
        {
            const int userId = 100000;
            List<string> expectedValues = new List<string> { "Role1", "ROLE2" };
            List<string> actualValues;

            actualValues = _userManager.getRolesByUserId(userId);

            Assert.AreEqual(expectedValues.Count, actualValues.Count);
        }

        [TestMethod]
        public void testGetRolesByUserIdWithNoRoles()
        {
            const int userId = 100004;
            List<string> expectedValues = new List<string> { };
            List<string> actualValues;

            actualValues = _userManager.getRolesByUserId(userId);

            Assert.AreEqual(expectedValues.Count, actualValues.Count);
        }

        [TestMethod]
        public void testAuthenticateUserWithCorrectInputs()
        {
            const string email = "a@test.com";
            const string password = "newuser";
            bool expectedValue = true;

            bool actualValue = _userManager.authenticateUser(email, password);
            
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void testAuthenticateUserWithIncorrectInputs()
        {
            const string email = "jdfidsjif";
            const string password = "fdsfds";

            _userManager.authenticateUser(email, password);
        }

        [TestMethod]
        public void testLoginUserWithCorrectInputs()
        {
            const string email = "a@test.com";
            const string password = "newuser";
            int expectedValue = 100000;

            int actualValue = _userManager.loginUser(email, password).UserId;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testLoginUserWithIncorrectInputs()
        {
            const string email = "jdfidsjif";
            const string password = "fdsfds";

            _userManager.loginUser(email, password);
        }

        [TestMethod]
        public void testAddUser()
        {
            const string firstName = "Joe";
            const string lastName = "Mama";
            const string email = "aa@bbb.ccc";
            const string password = "password@123";
            const string expectedValue = "aa@bbb.ccc";

            _userManager.addUser(firstName, lastName, email, password);
            var actualValue = _userManager.getUserByEmail(email);

            Assert.AreEqual(expectedValue, actualValue.Email);
        }

        [TestMethod]
        public void testEditUser()
        {
            const string firstName = "Joe";
            const string lastName = "Mama";
            const string old_email = "e@test.com";
            const string new_email = "aa@bbb.ccc";
            const string old_password = "newuser";
            const string new_password = "password@123";
            UserVM expectedUser = new UserVM()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = new_email,
            };

            _userManager.editUser(firstName, lastName, old_email, new_email, old_password, new_password);
            UserVM user = _userManager.getUserByEmail(new_email);

            Assert.AreEqual(expectedUser.FirstName, user.FirstName);
        }

        [TestMethod]
        public void testGetAllUsers()
        {
            const int expectedValue = 5;

            int actualValue = _userManager.getAllUsers().Count();

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
