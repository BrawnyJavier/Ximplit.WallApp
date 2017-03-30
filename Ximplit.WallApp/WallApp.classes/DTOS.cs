using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallApp.classes
{
    public class PostDTOS
    {
        public int PostId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string AuthorUserName { get; set; }
    }
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public string Content { get; set; }
        public int ParentCommentID { get; set; }
        public string AuthorUsename { get; set; }
    }
}
