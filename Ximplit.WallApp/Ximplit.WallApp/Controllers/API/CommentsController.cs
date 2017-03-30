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
    public class CommentsController : ApiController
    {
        // GET: api/Comments                <- ROUTE
        public IEnumerable<Comment> Get()
        {
            using (var contex = new WallAppContext())
            {
                return contex.Comments.ToList();
            }
        }
        // GET: api/Comments/id             <- ROUTE
        public Comment Get(int id)
        {
            using (var contex = new WallAppContext())
            {
                return contex.Comments.Where(c => c.CommentID == id).FirstOrDefault();
            }
        }
        // POST: api/Comments                   <- ROUTE
        [BasicAuth] // Only logged in users can comment.      
        public object Post([FromBody]CommentDTO value)
        {
            if (ModelState.IsValid)
            {
                using (var context = new WallAppContext())
                {
                    var Comment = new Comment
                    {
                        CommentAuthor = context.Users.Where(u =>
                                    u.UserName == Thread.CurrentPrincipal.Identity.Name)
                                    .FirstOrDefault(),

                        ParentComment = context.Comments.Where(c =>
                                    c.CommentID == value.ParentCommentID).FirstOrDefault(),
                        Content = value.Content
                    };
                    context.Comments.Add(Comment);
                    context.SaveChanges();
                    return HttpStatusCode.OK;
                }
            }
            else return HttpStatusCode.BadRequest;
        }
        // PUT: api/Comments/id                         <- ROUTE
        [BasicAuth] // Only logged in users can update comments.      
        public object Put([FromBody]CommentDTO value)
        {
            // Only the author of the comment can make changes to it.
            if (value.AuthorUsename == Thread.CurrentPrincipal.Identity.Name)
            {
                using (var context = new WallAppContext())
                {
                    var commentInDatabase = context.Comments.Where(c =>
                    c.CommentID == value.CommentID
                    ).FirstOrDefault();
                    commentInDatabase.Content = value.Content;
                    context.SaveChanges();
                    return HttpStatusCode.OK;
                }
            }
            else return HttpStatusCode.Unauthorized;
        }
        // DELETE: api/Comments/id                     <- ROUTE
        [BasicAuth] // Only logged in users can delete comments.      
        public object Delete(int id)
        {
            using (var context = new WallAppContext())
            {
                var CommentInDatabase = context.Comments.Where(c => c.CommentID == id &&
                // We check that the current user is the owner of the given comment
                        c.CommentAuthor == context.Users.
                        Where(u => u.UserName == Thread.CurrentPrincipal.Identity.Name)
                        .FirstOrDefault()
                ).FirstOrDefault();
                // If a post with the given id and having the current user as owner exist, we can proceed to delete it.
                if (CommentInDatabase != null)
                {
                    context.Comments.Remove(CommentInDatabase);
                    return HttpStatusCode.OK;
                }
                else return HttpStatusCode.Unauthorized;
            }
        }
    }
}
