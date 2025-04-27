using DataDomain;
using LogicLayer;
using System.Security.Claims;

namespace WebPresentation.Models
{
    public class AccessToken
    {
        private UserVM _token = new UserVM() { UserID = 0, Email = "", FirstName = "", LastName = "", Active = false };
        private UserManager _userManager = new UserManager();

        public AccessToken(string email)
        {
            try
            {
                _token = _userManager.getUserByEmail(email);
            }
            catch {}
        }

        public bool IsSet { get { return _token.UserID != 0; } }
        public int UserId { get { return _token.UserID; } }
        public string Email { get { return _token.Email; } }
        public string FirstName { get { return _token.FirstName; } }
        public string LastName { get { return _token.LastName; } }
        public List<string> Roles { get { return _token.Roles; } }
        public bool IsLibrarian { get { return _token.isLibrarian(); } }
        public bool IsAdmin { get { return _token.isAdmin(); } }
    }
}
