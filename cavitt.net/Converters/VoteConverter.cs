using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using System;

namespace cavitt.net.Converters
{
    public class VoteConverter : IConverter<Vote, VoteDto>
    {
        private readonly ILoggerRepository _loggerRepository;

        public VoteConverter(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
    }

        public VoteDto Convert(Vote source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {
                VoteDto vote = new VoteDto
                {
                     PostId = source_object.PostId,
                     UserId = source_object.UserId,
                     UserVote = source_object.UserVote
                };

                return vote;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public Vote Convert(VoteDto source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {
                Vote vote = new Vote
                {
                    PostId = source_object.PostId,
                    UserId = source_object.UserId,
                    UserVote = source_object.UserVote
                };

                return vote;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }
    }
}
