using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallApp.classes
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        public User Author { get; set; }
    }
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [Required]
        public string Content { get; set; }
        public Comment ParentComment { get; set; }
    }
}
