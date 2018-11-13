
using System;
using System.Collections.Generic;

namespace cavitt.net.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public ApplicationUser Author { get; set; }
        public DateTime DateCreated{get;set;}
        public DateTime DateModified { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes{ get; set; }
    }
}
