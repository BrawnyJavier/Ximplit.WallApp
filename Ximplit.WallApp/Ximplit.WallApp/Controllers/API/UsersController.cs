using Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Http;
using WallApp.classes;
using WallApp.DataModel;
using Ximplit.WallApp.Services;

namespace Ximplit.WallApp.Controllers.API
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public bool Login(string credentials)
        {
            try
            {
                string[] Credentials = credentials.Split('|');
                string username = Credentials[0];
                string password = Credentials[1];
                using (var _context = new WallAppContext())
                {
                    bool r = _context.Users.Any(user =>
                 user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                 && user.Password == password
                 );
                    return r;
                }
            }
            catch (Exception) { return false; }
        }
        [HttpGet]
        public bool sendPassword(string username)
        {
            using (var context = new WallAppContext())
            {
                User user = context.Users.Where(u => u.UserName == username).FirstOrDefault();
                if (user != null)
                {
                    var emailHandler = new EmailSender();
                    var Email = context.StoredEmailsTemplates.Where(email => email.EmailCode == "PASSWORD").FirstOrDefault();
                    string EmailSubject = Email.Subject;
                    string EmailBody = Email.Body + " Usuario: " + user.UserName + " Contraseña: " + user.Password;
                    emailHandler.SendEmail(EmailBody, EmailSubject, user.Email);
                    return true;
                }
                return false;
            }
        }
        [HttpGet]
        public IEnumerable<object> GetUsers()
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
        [HttpGet]
        public object GetUserByUsername(string userName)
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
        [HttpPost]
        public object CreateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                // User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(val.ToString());
                using (var _context = new WallAppContext())
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    var emailHandler = new EmailSender();
                    var Email = _context.StoredEmailsTemplates.Where(email => email.EmailCode == "WELCOME").FirstOrDefault();
                    string EmailBody = Email.Body;
                    string EmailSubject = Email.Subject + " " + user.Name + " " + user.LastName + "!";
                    emailHandler.SendEmail(EmailBody, EmailSubject, user.Email);
                }
                return HttpStatusCode.OK;
            }
            else return HttpStatusCode.BadRequest;
        }
        // PUT: api/Users/5        
        [HttpPut]
        [BasicAuth]
        public object UpdateUser([FromBody]User value)
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
        [HttpDelete]
        [BasicAuth]
        public object DeleteUser(string username)
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
