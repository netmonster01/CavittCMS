using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cavitt.net.Data;
using cavitt.net.Interfaces;
using cavitt.net.Dtos;

namespace cavitt.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlogRepository _blogRepository;

        public PostsController(ApplicationDbContext context, IBlogRepository blogRepository)
        {
            _context = context;
            _blogRepository = blogRepository;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<PostDto> GetPosts()
        {
            return _blogRepository.GetPosts();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public IActionResult GetPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = _blogRepository.GetPost(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] int id, [FromBody] PostDto post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.PostId)
            {
                return BadRequest();
            }

           bool didCreate = await _blogRepository.CreatePostAsync(post);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PostExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return Ok(didCreate);
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] PostDto post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool didCreate = await _blogRepository.CreatePostAsync(post);

            return Ok(didCreate);
            //return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}