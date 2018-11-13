using cavitt.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Dtos
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<Vote> Votes { get; set; }
    }
}
