using DataDomain;
using LogicLayer;
using System.Security.Claims;

namespace WebPresentation.Models
{
    public static class AccessToken
    {
        private static UserVM token;
        private static UserManager _userManager = new UserManager();

        public static void SetToken(string email)
        {
            token = _userManager.getUserByEmail(email);
        }

        public static void UnsetToken()
        {
            token = new UserVM() { UserID = 0, Email = "", FirstName = "", LastName = "", Active = false };
        }

        public static bool IsSet { get { return token.UserID != 0; } }
        public static int UserId { get { return token.UserID; } }
        public static string Email { get { return token.Email; } }
        public static string FirstName { get { return token.FirstName; } }
        public static string LastName { get { return token.LastName; } }
        public static List<string> Roles { get { return token.Roles; } }
        public static bool IsLibrarian { get { return token.isLibrarian(); } }
        public static bool IsAdmin { get { return token.isAdmin(); } }
    }
}
