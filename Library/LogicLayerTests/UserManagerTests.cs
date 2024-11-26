using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
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
        public void TestGetUserByEmailWithValidEmail()
        {
            const string email = "a@test.com";
            const string expectedValue = email;
            string actualValue;
            actualValue = _userManager.getUserByEmail(email).Email;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetUserByEmailWithInvalidEmail()
        {
            const string email = "faek@test.com";

            _userManager.getUserByEmail(email);
        }

        [TestMethod]
        public void TestGetRolesByUserIdWithRoles()
        {
            const int userId = 100000;
            List<string> expectedValues = new List<string> { "Role1", "ROLE2" };
            List<string> actualValues;

            actualValues = _userManager.getRolesByUserId(userId);

            Assert.AreEqual(expectedValues.Count, actualValues.Count);
        }

        [TestMethod]
        public void TestGetRolesByUserIdWithNoRoles()
        {
            const int userId = 100004;
            List<string> expectedValues = new List<string> { };
            List<string> actualValues;

            actualValues = _userManager.getRolesByUserId(userId);

            Assert.AreEqual(expectedValues.Count, actualValues.Count);
        }

        [TestMethod]
        public void TestAuthenticateUserWithCorrectInputs()
        {
            const string email = "a@test.com";
            const string password = "newuser";
            bool expectedValue = true;

            bool actualValue = _userManager.authenticateUser(email, password);
            
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void TestAuthenticateUserWithIncorrectInputs()
        {
            const string email = "jdfidsjif";
            const string password = "fdsfds";

            _userManager.authenticateUser(email, password);
        }

        [TestMethod]
        public void TestLoginUserWithCorrectInputs()
        {
            const string email = "a@test.com";
            const string password = "newuser";
            int expectedValue = 100000;

            int actualValue = _userManager.loginUser(email, password).UserId;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLoginUserWithIncorrectInputs()
        {
            const string email = "jdfidsjif";
            const string password = "fdsfds";

            _userManager.loginUser(email, password);
        }
    }
}
