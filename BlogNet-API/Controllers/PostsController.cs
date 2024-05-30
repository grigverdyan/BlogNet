using Microsoft.AspNetCore.Mvc;
using BlogNet_DAL;
using BlogNet_DAL.Models;

namespace BlogNet_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostRepository postRepository;

        public PostsController(PostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet("{id:int}")]
        public Post Get(int id)
        {
            return postRepository.Get(id);
        }
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return postRepository.Get();
        }
        [HttpPost]
        public ActionResult<Post> Post([FromBody] PostBase post)
        {
            if (post != null)
            {
                return postRepository.Insert(post);
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult<bool> Put(int id, [FromBody] PostBase post)
        {
            if (post != null)
            {
                Post updatedPost = new Post
                {
                    PostID = id,
                    Title = post.Title,
                    Content = post.Content,
                    PostDate = post.PostDate,
                    UserID = post.UserID
                };
                if (postRepository.Update(updatedPost))
                {
                    return true;
                }
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            if (postRepository.Delete(id))
            {
                return true;
            }
            return BadRequest();
        }
    }
}
