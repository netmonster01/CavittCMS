using cavitt.net.Dtos;
using cavitt.net.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cavitt.net.Interfaces
{
    public interface IBlogRepository
    {
        //Blog GetBlog(int blogId);
        //List<Blog> GetBlogs();
        PostDto GetPost(int postId);
        PostDto GetLatestPost();
        List<PostDto> GetPosts();
        Task<bool> CreatePostAsync(PostDto post);
        Task<bool> CreateCommentAsync(CommentDto comment);
        //Task<bool> CreateBlog(Blog blog);

        //Task<bool> CreateBlogImageAsync(BlogImage blogImage);
        //bool BlogExists(string title);
        Task<bool> DeletePost(int postId);

    }
}
