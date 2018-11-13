using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using System;
using System.Linq;

namespace cavitt.net.Converters
{
    public class CommentToCommentDtoConverter : IConverter<Comment, CommentDto>
    {
        private readonly ILoggerRepository _loggerRepository;

        public CommentToCommentDtoConverter(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public CommentDto Convert(Comment sourceComment)
        {
           
            if (sourceComment == null)
            { return null; }

            try
            {
                CommentDto comment = new CommentDto
                {
                    Message = sourceComment.Message,
                    DateCreated = sourceComment.DateCreated,
                    DateModified = sourceComment.DateModified,
                    DisLikes = sourceComment.DisLikes,
                    Likes = sourceComment.Likes,
                    PostId = sourceComment.PostId,
                    UserId = sourceComment.UserId,
                    CommentId = sourceComment.CommentId
                };

                return comment;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }
    }
}
