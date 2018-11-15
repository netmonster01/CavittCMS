
using cavitt.net.Dtos;
using cavitt.net.Models;

namespace cavitt.net.Interfaces
{
    public interface IVoteRepository
    {
        VoteDto GetVote(VoteDto vote);
        VoteDto AddorUpdateVote(VoteDto vote);
        bool VoteExists(int postId, string userId);
        bool VoteExists(Vote vote);
        bool VoteExists(VoteDto vote);

        VoteCountDto GetPostVotes(int postId);
    }
}
