using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallApp.classes
{
    public class PostDTOS
    {
        public string CreationDateFormated { get; set; }
        public DateTime CreationDate { get; set; }
        public int PostId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string AuthorUserName { get; set; }
        public List<CommentDTO> comments { get; set; }
    }
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public string Content { get; set; }
        public int ParentCommentID { get; set; }
        public string AuthorUsename { get; set; }
        public int PostId { get; set; }
    }
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
