using System;
using System.Collections.Generic;
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
    public class PostsController : ApiController
    {
        // GET: api/Posts
        public IEnumerable<Post> Get()
        {
            using (var _Context = new WallAppContext())
            {
                return _Context.Posts.ToList();
            }
        }

        // GET: api/Posts/5
        public Post Get(int id)
        {
            using (var _Context = new WallAppContext())
            {
                return _Context.Posts.Where(p => p.PostId == id).FirstOrDefault();
            }
        }
        [BasicAuth] // Only logged in users can call this method
        // POST: api/Posts <---                            ROUTE.
        public object Post([FromBody]Post value)
        {
            if (ModelState.IsValid)
            {
                using (var _Context = new WallAppContext())
                {
                    _Context.Posts.Add(value);
                    _Context.SaveChanges();
                    return HttpStatusCode.OK;
                }
            }
            else return HttpStatusCode.BadRequest;
        }
        [BasicAuth]
        // PUT: api/Posts/5
        public object Put([FromBody]PostDTOS value)
        {
            if (ModelState.IsValid)
            {
                // Only the owner of the post can delete it.
                if (value.AuthorUserName == Thread.CurrentPrincipal.Identity.Name)
                {
                    using (var _context = new WallAppContext())
                    {
                        var PostInDatabase = _context.Posts.Where(p => p.PostId == value.PostId).FirstOrDefault();
                        PostInDatabase.title = value.title;
                        PostInDatabase.content = value.content;
                        _context.SaveChanges();
                        return HttpStatusCode.OK;
                    }
                }
                else return HttpStatusCode.Unauthorized;
            }
            else return HttpStatusCode.BadRequest;
        }
        // DELETE: api/Posts/5
        [BasicAuth]
        public object Delete(int id)
        {
            using (var _context = new WallAppContext())
            {
                var toDelete = _context.Posts.Where(p => p.PostId == id).FirstOrDefault();
                if (toDelete.Author.UserName == Thread.CurrentPrincipal.Identity.Name)
                {
                    _context.Posts.Remove(toDelete);
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.Unauthorized;
                }
            }
        }
    }
}
