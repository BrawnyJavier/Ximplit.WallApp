using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WallApp.classes;
using WallApp.DataModel;
using Ximplit.WallApp.Services;

namespace Ximplit.WallApp.Controllers.API
{
    public class UsersController : ApiController
    {
        public bool Login(string credentials)
        {
            try
            {
                string[] Credentials = credentials.Split(':');
                string username = Credentials[0];
                string password = Credentials[1];
                using (var _context = new WallAppContext())
                {
                    return _context.Users.Any(user =>
                 user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                 && user.Password == password
                 );
                }                
            }
            catch (Exception) { return false; }
        }

        public IEnumerable<object> Get()
        {
            using (var _context = new WallAppContext())
            {
                // Return all but the password  x)
                return _context.Users.Select(s => new
                {
                    LastName = s.LastName,
                    Name = s.Name,
                    UserName = s.UserName
                }).ToList();
            }
        }
        public object Get(string userName)
        {
            using (var _context = new WallAppContext())
            {
                return _context.Users.Where(u => u.UserName == userName).Select(
                    s => new
                    {
                        LastName = s.LastName,
                        Name = s.Name,
                        UserName = s.UserName
                    }).FirstOrDefault();
            }
        }
        // POST: api/Users
        public object Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                // User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(val.ToString());
                using (var _context = new WallAppContext())
                {
                    _context.Users.Add(user);
                }
                return HttpStatusCode.OK;
            }
            else return HttpStatusCode.BadRequest;
        }
        // PUT: api/Users/5
        [BasicAuth]
        public object Put([FromBody]User value)
        {
            if (value.UserName == Thread.CurrentPrincipal.Identity.Name)
            {
                using (var contex = new WallAppContext())
                {
                    var userInDatabase = contex.Users.Where(u => u.UserName == value.UserName).FirstOrDefault();
                    userInDatabase.LastName = value.LastName;
                    userInDatabase.Name = value.Name;
                    userInDatabase.Password = value.Password;
                    userInDatabase.UserName = value.UserName;
                    userInDatabase.Email = value.Email;
                    contex.SaveChanges();
                    return HttpStatusCode.OK;
                }
            }
            return HttpStatusCode.Unauthorized;
        }
        // DELETE: api/Users/5
        [BasicAuth]
        public object Delete(string username)
        {
            using (var context = new WallAppContext())
            {
                // confirm if the current user is the owner of the account
                bool isUser = username == Thread.CurrentPrincipal.Identity.Name;
                if (isUser) // Only the owner of the account can delete it.
                {
                    var toDelete = context.Users.Where(u => u.UserName == username).FirstOrDefault();
                    context.Users.Remove(toDelete);
                    return HttpStatusCode.OK;
                }
                else return HttpStatusCode.Unauthorized;
            }
        }
    }
}
