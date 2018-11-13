using System;
using System.ComponentModel.DataAnnotations;

namespace cavitt.net.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Message { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }   
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
    }
}
