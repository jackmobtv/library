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
            _userManager = new UserManager();
        }
    }
}
