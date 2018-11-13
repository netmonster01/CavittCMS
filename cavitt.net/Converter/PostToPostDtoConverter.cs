using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Converter
{
    public class PostToPostDtoConverter : IConverter<Post, PostDto>
    {
        private readonly ILoggerRepository _loggerRepository;
        public PostToPostDtoConverter(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }
        public PostDto Convert(Post sourcePost)
        {
           
            if (sourcePost == null)
            { return null; }

            try
            {
                PostDto post = new PostDto {
                    Author = sourcePost.Author.UserName,
                    Comments = sourcePost.Comments.ToList(),
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
    }
}
