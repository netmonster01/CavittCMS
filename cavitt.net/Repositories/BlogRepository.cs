using cavitt.net.Data;
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cavitt.net.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerRepository _loggerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConverter<Post, PostDto> _converter;
        private readonly IConverter<Comment, CommentDto> _commentConverter;
        public BlogRepository(IServiceProvider serviceProvider, IHttpContextAccessor accessor)
        {
            _serviceProvider = serviceProvider;
            _applicationDbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _loggerRepository = _serviceProvider.GetRequiredService<ILoggerRepository>();
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _converter = _serviceProvider.GetRequiredService<IConverter<Post, PostDto>>();
            _accessor = accessor;
            _commentConverter = _serviceProvider.GetRequiredService<IConverter<Comment, CommentDto>>();
        }


        public async Task<bool> CreateCommentAsync(CommentDto comment)
        {
            bool didCreate = false;
            try
            {
                //Comment c = new Comment()
                //{
                //    UserId = 
                //}
                //comment.User.UserName = await GetCurrentUser();
               // _applicationDbContext.Comments.Add(_commentConverter.Convert(comment));
                await _applicationDbContext.SaveChangesAsync();
                didCreate = true;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }

            return didCreate;
        }

        public async Task<bool> CreatePostAsync(PostDto post)
        {
            bool didCreate = false;

            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                try
                {
                    // get user
                    post.Author = await GetCurrentUser();
                    Post newPost = new Post {
                        Content = post.Content,
                        UserId = post.UserId,
                        Title = post.Title,
                        Author = new ApplicationUser { UserName = post.Author, Id = post.UserId },
                        
                    };

                    _applicationDbContext.Posts.Add(newPost);
                    await _applicationDbContext.SaveChangesAsync();
                    didCreate = true;
                }
                catch (Exception ex)
                {
                    _loggerRepository.Write(ex);
                }
            }

            return didCreate;
        }

        private async Task<string> GetCurrentUser()
        {
            string userName = string.Empty;
            try
            {
                if (_accessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    ClaimsPrincipal currentUser = _accessor.HttpContext.User;

                    var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;

                    ApplicationUser user = await _userManager.FindByNameAsync(currentUserName);
                    userName = string.Format("{0} {1}", user.FirstName, user.LastName);
                }
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }
            return userName;
        }

      

        public PostDto GetPost(int postId)
        {
            PostDto post = new PostDto();
            try
            {
                post = _converter.Convert(_applicationDbContext.Posts.FirstOrDefault(x => x.PostId == postId));
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }

            return post;
        }

        public PostDto GetLatestPost()
        {
            PostDto post = new PostDto();
            try
            {
                var p = _applicationDbContext.Posts.Include(c => c.Comments).Include(u => u.Author).LastOrDefault();
                post = _converter.Convert(p);
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }

            return post;
        }

        public List<PostDto> GetPosts()
        {
            List<PostDto> posts = new List<PostDto>();
            try
            {
                var pl = _applicationDbContext.Posts.Include(c => c.Comments).Include(u => u.Author).OrderBy(o => o.DateCreated).ToList();

                posts = pl.Select(p => _converter.Convert(p)).ToList() ;

                //posts = _applicationDbContext.Posts.Select(
                //    p=> _converter.Convert(p)
                //    ).OrderBy(o => o.DateCreated).ToList();
                //posts = (from p in _applicationDbContext.Posts
                //         join u in _applicationDbContext.Users on p.UserId equals u.Id
                //         //join c in _applicationDbContext.Comments on p.PostId equals c.PostId
                //         //where p.PostId > 0

                //         //(from comment in _applicationDbContext.Comments
                //         // where comment.PostId == p.PostId
                //         // select (comment)).ToList(),
                //         select (new Post
                //         {
                //             Author = p.Author,
                //             Comments = _applicationDbContext.Comments.Where(c => c.PostId == p.PostId).Select(c=> c).ToList(),
                //             Content = p.Content,
                //             DateCreated = p.DateCreated,
                //             DateModified = p.DateModified,
                //             Likes = p.Likes,
                //             PostId = p.PostId,
                //             Title = p.Title,
                //             UserId = p.UserId,
                //             DisLikes = p.DisLikes
                //         })).OrderBy(c=> c.DateCreated).ToList();
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }

            return posts;
        }

        //public bool DeletePost(int postId)
        //{
        //    // delete post and comments
        //    throw new NotImplementedException();
        //}

        public async Task<bool> DeletePost(int postId)
        {
            try
            {
                var post = await _applicationDbContext.Posts.FindAsync(postId);
                // remove comments
                var comments = _applicationDbContext.Comments.Where(c => c.PostId == postId);
                _applicationDbContext.Comments.RemoveRange(comments);
                _applicationDbContext.Posts.Remove(post);
                _applicationDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return false;
                    
            }

        }
    }
}
