using System;
using System.ComponentModel.DataAnnotations;

namespace cavitt.net.Dtos
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string Message { get; set; }

        public string UserId { get; set; } 
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
    }
}
