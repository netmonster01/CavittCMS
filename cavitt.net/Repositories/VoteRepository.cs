using cavitt.net.Data;
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Repositories
{
    public class VoteRepository : IVoteRepository
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILoggerRepository _loggerRepository;

        public VoteRepository(ApplicationDbContext applicationDbContext, ILoggerRepository loggerRepository) {

            _applicationDbContext = applicationDbContext;
            _loggerRepository = loggerRepository;
        }

        public VoteDto AddorUpdateVote(VoteDto incomingVote)
        {
            try
            {
                var post = _applicationDbContext.Posts.Where(p => p.PostId == incomingVote.PostId).FirstOrDefault();

                // did user vote on this post? No
                if (!VoteExists(incomingVote.PostId, incomingVote.UserId))
                {
                    if (incomingVote.UserVote == 1)
                    {
                        post.Likes++;

                    }
                    else if (incomingVote.UserVote == -1)
                    {
                        post.Likes--;
                    }

                    // add new vote
                    _applicationDbContext.Votes.Add(new Vote { PostId = incomingVote.PostId, UserId = incomingVote.UserId, UserVote = incomingVote.UserVote } );
                }
                else
                {
                    // get the user vote.
                    var previousVote = _applicationDbContext.Votes.Where(v => v.UserId == incomingVote.UserId).FirstOrDefault();

                    // check it.
                    if (incomingVote.UserVote > previousVote.UserVote)
                    {
                        post.Likes++;
                        post.DisLikes--;

                    }
                    else if (incomingVote.UserVote < previousVote.UserVote)
                    {
                        post.Likes--;
                        post.DisLikes++;// = post.DisLikes++;
                    }

                    previousVote.UserVote = incomingVote.UserVote;
                    // update vote
                    _applicationDbContext.Update(previousVote);
                }
                // update post
                _applicationDbContext.Update(post);
                _applicationDbContext.SaveChanges();
                return incomingVote;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }

         
        }

        public VoteCountDto GetPostVotes(int postId)
        {
            try
            {
                VoteCountDto count = new VoteCountDto();
                var post = _applicationDbContext.Posts.Where(p => p.PostId == postId).FirstOrDefault();
                if (post != null)
                {
                    count.DisLikes = post.DisLikes;
                    count.Likes = post.Likes;
                    count.PostId = post.PostId;
                }
                return count;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }



        public VoteDto GetVote(VoteDto vote)
        {
            try
            {
                VoteDto castedVote = new VoteDto();
                
                castedVote = _applicationDbContext.Votes.Where(v => v.PostId == vote.PostId && v.UserId == vote.UserId).Select(v=> new VoteDto { PostId = v.PostId, UserId = v.User.UserName, UserVote = v.UserVote}).FirstOrDefault();
                return castedVote;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public bool VoteExists(Vote vote)
        {
            return VoteExists(vote.PostId, vote.UserId);
        }

        public bool VoteExists(int postId, string userId)
        {
            return _applicationDbContext.Votes.Any(i => i.PostId == postId && i.UserId == userId);
        }

        public bool VoteExists(VoteDto vote)
        {
            return VoteExists(vote.PostId, vote.UserId);
        }
    }
}
