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
        //private DbContext _context;
        //public UsersController()
        //{
        //    _context = new WallAppContext();
        //}
        // GET: api/Users
        public IEnumerable<User> Get()
        {
            using (var _context = new WallAppContext())
            {
                return _context.Users.ToList();
            }
        }

        [BasicAuth]
        public object Get(string userName)
        {
            using ( var _context = new WallAppContext())
            {
                var user = _context.Users.Where(x => x.UserName == Thread.CurrentPrincipal.Identity.Name).FirstOrDefault();

                    return user;
            }
        }

        // GET: api/Users/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        public object Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                using (var _context = new WallAppContext())
                {
                    _context.Users.Add(user);
                }
                return HttpStatusCode.OK;
            }
            else return HttpStatusCode.BadRequest;

        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
