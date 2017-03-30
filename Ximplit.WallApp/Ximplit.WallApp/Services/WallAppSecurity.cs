using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WallApp.classes;
using WallApp.DataModel;

namespace Ximplit.WallApp.Services
{
    public class WallAppSecurity
    {
        public static bool Login(string username, string password)
        {
            using (var _context = new WallAppContext())
            {

                return _context.Users.Any(user =>
                 user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                 && user.Password == password
                 );

            }
        }
    }
}