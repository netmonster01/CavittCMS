using cavitt.net.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cavitt.net.Interfaces
{
    public interface IBlogRepository
    {

        PostDto GetPost(int postId);
        PostDto GetLatestPost();
        List<PostDto> GetPosts();
        Task<bool> CreatePostAsync(PostDto post);
        Task<bool> CreateCommentAsync(CommentDto comment);
        Task<bool> DeletePost(int postId);

    }
}
