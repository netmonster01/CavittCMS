
using cavitt.net.Dtos;
using cavitt.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Interfaces
{
    public interface IVoteRepository
    {
        VoteDto GetVote(VoteDto vote);
        VoteDto AddorUpdateVote(VoteDto vote);
        //Vote UpdateVote(Vote vote);
        bool VoteExists(int postId, string userId);
        bool VoteExists(Vote vote);
        bool VoteExists(VoteDto vote);

        VoteCountDto GetPostVotes(int postId);
    }
}
