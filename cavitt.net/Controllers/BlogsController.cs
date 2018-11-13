using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cavitt.net.Interfaces;
using cavitt.net.Dtos;

namespace cavitt.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
     

        public BlogsController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }


        [HttpGet]
        [Route("Posts")]
        public IActionResult GetPosts()
        {
            var posts = _blogRepository.GetPosts();

            return Ok(posts);
        }

        [HttpGet]
        [Route("LatestPost")]
        public IActionResult GetLatestPosts()
        {
            var lastPost = _blogRepository.GetLatestPost();

            return Ok(lastPost);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> CreatePostAsync(PostDto post)
        {
            var posts = await _blogRepository.CreatePostAsync(post);

            return Ok(posts);
        }

        [HttpPost]
        [Route("Comment")]
        public async Task<IActionResult> CreateCommentAsync(CommentDto comment)
        {
            var posts = await _blogRepository.CreateCommentAsync(comment);

            return Ok(posts);
        }


        //// PUT: api/Blogs/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBlog([FromRoute] int id, [FromBody] Blog blog)
        //{
        //    bool results = false;
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != blog.BlogId)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        results = await _blogRepository.CreateBlog(blog);
        //        return Ok(results);
        //    }
        //    catch (Exception)
        //    {
        //        return NoContent();
        //    }
           
        //}

        // DELETE: api/Blogs/5
        [HttpDelete()]
        [Route("Post/{postId}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] int postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var didDelete = await _blogRepository.DeletePost(postId);

            return Ok(didDelete);
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post([FromBody]IFormFile file)
        {
            long size = file.Length;

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            //foreach (var formFile in files)
            //{
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
           // }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { size, filePath });
        }
    }
}