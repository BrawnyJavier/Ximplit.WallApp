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
    public class PostsController : ApiController
    {
        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            using (var _Context = new WallAppContext())
            {
                return _Context.Posts.ToList();
            }
        }
        // GET: api/Posts/5
        public Post GetPostById(int id)
        {
            using (var _Context = new WallAppContext())
            {
                return _Context.Posts.Where(p => p.PostId == id).FirstOrDefault();
            }
        }
        // GET: api/Posts/5
        [HttpGet]
        public object GetPostAndCommentsById(int id)
        {
            using (var _Context = new WallAppContext())
            {
                return _Context.Posts.Where(p => p.PostId == id).Select(o => new
                {
                    content = o.content,
                    author = o.Author.UserName,
                    Comments = _Context.Comments.Where(c => c.Post.PostId == id).ToList()
                }).FirstOrDefault();
            }
        }
        [HttpGet]
        public object GetPostAndComments()
        {
            using (var _Context = new WallAppContext())
            {
                var returnData = _Context.Posts.Select(o => new PostDTOS
                {
                    PostId = o.PostId,
                    content = o.content,
                    CreationDate = o.CreationDate,
                    CreationDateFormated = "",
                    AuthorUserName = o.Author.UserName,
                    comments = _Context.Comments.Where(x => x.Post.PostId == o.PostId)
                    .Select(
                          c => new CommentDTO
                          {
                              AuthorUsename = c.CommentAuthor.UserName,
                              CommentID = c.CommentID,
                              Content = c.Content
                          }).ToList()
                                }).OrderByDescending(w => w.CreationDate)
                    .ToList();
                var DateFormatter = new Miscellaneous.Formaters();
                foreach (var value in returnData)
                {
                    value.CreationDateFormated = DateFormatter.TimelineDateFormat(value.CreationDate);
                }

                return returnData;
                //.OrderByDescending(a => a.CreationDate)
            }
        }
        // POST: api/Posts <---                            ROUTE.
        [BasicAuth] // Only logged in users can call this method
        public object CreatePost([FromBody]Post value)
        {
            if (ModelState.IsValid)
            {
                using (var _Context = new WallAppContext())
                {
                    System.DateTime now = System.DateTime.Now;
                    value.CreationDate = now;
                    value.Author = _Context.Users.Where(u => u.UserName == Thread.CurrentPrincipal.Identity.Name).FirstOrDefault();
                    _Context.Posts.Add(value);
                    _Context.SaveChanges();
                    return HttpStatusCode.OK;
                }
            }
            else return HttpStatusCode.BadRequest;
        }
        // PUT: api/Posts/5
        [BasicAuth]
        public object UpdatePost([FromBody]PostDTOS value)
        {
            if (ModelState.IsValid)
            {
                // Only the owner of the post can delete it.
                if (value.AuthorUserName == Thread.CurrentPrincipal.Identity.Name)
                {
                    using (var _context = new WallAppContext())
                    {
                        var PostInDatabase = _context.Posts.Where(p => p.PostId == value.PostId).FirstOrDefault();
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
        public object DeletePost(int id)
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
