using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cavitt.net.Interfaces;
using cavitt.net.Dtos;

namespace cavitt.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteRepository _voteRepository;

        public VotesController(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }


        // GET: api/Vote
        [HttpPost]
        [Route("MyVote")]
        public IActionResult GetVote([FromBody] VoteDto vote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Vote checkVote = new Vote { PostId = postId, UserId = userId };
            var castVote = _voteRepository.GetVote(vote);

            //if (castVote == null)
            //{
            //    return NotFound();
            //}

            return Ok(castVote);
        }

        // GET: api/Vote
        [HttpGet]
        [Route("VoteCount/{postID}")]
        public IActionResult GetVoteCounts([FromRoute] int postID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postCounts = _voteRepository.GetPostVotes(postID);

            if (postCounts == null)
            {
                return NotFound();
            }

            return Ok(postCounts);
        }

        // PUT: api/Votes/5
        [HttpPut]
        public async Task<IActionResult> PutVote([FromBody] VoteDto vote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != vote.Id)
            //{
            //    return BadRequest();
            //}

            var updatedVote = _voteRepository.AddorUpdateVote(vote);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!VoteExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return Ok(updatedVote);
        }

        // POST: api/Votes
        [HttpPost]
        public async Task<IActionResult> PostVote([FromBody] VoteDto vote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentVote = _voteRepository.AddorUpdateVote(vote);
            return Ok(currentVote);
        }
    }
}