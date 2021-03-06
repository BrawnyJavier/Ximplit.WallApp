﻿using System.Data.Entity;
using WallApp.classes;
namespace WallApp.DataModel
{
    public class WallAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<WallAppStoredEmails> StoredEmailsTemplates { get; set; }
        public DbSet<PostsLikes> PostsLikes { get; set; }
        public DbSet<CommentsLikes> CommentsLikes { get; set; }
    }
}
