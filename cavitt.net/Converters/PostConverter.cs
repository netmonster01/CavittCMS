using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Converters
{
    public class PostConverter : IConverter<Post, PostDto>
    {
        private readonly ILoggerRepository _loggerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConverter<Comment, CommentDto> _converter;
        public PostConverter(ILoggerRepository loggerRepository, UserManager<ApplicationUser> userManager, IConverter<Comment, CommentDto> converter)
        {
            _loggerRepository = loggerRepository;
            _userManager = userManager;
            _converter = converter;
        }

        public PostDto Convert(Post sourcePost)
        {

            if (sourcePost == null)
            { return null; }

            try
            {
                var user = _userManager.FindByIdAsync(sourcePost.UserId).Result;
                PostDto post = new PostDto
                {
                    Author = string.Format("{0} {1}", user.FirstName, user.LastName),
                    Comments = sourcePost.Comments.Select(c=> _converter.Convert(c)).ToList(),
                    Content = sourcePost.Content,
                    DateCreated = sourcePost.DateCreated,
                    DateModified = sourcePost.DateModified,
                    DisLikes = sourcePost.DisLikes,
                    Likes = sourcePost.Likes,
                    PostId = sourcePost.PostId,
                    Title = sourcePost.Title,
                    UserId = sourcePost.UserId
                };

                return post;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public Post Convert(PostDto source_object)
        {
            throw new NotImplementedException();
        }
    }
}
