using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
