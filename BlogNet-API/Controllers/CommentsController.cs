using Microsoft.AspNetCore.Mvc;
using BlogNet_DAL;
using BlogNet_DAL.Models;

namespace BlogNet_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentRepository commentRepository;

        public CommentsController(CommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        [HttpGet("{id:int}")]
        public Comment Get(int id)
        {
            return commentRepository.Get(id);
        }
        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return commentRepository.Get();
        }
        [HttpPost]
        public ActionResult<Comment> Post([FromBody] CommentBase comment)        {
            if (comment != null)
            {
                return commentRepository.Insert(comment);
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult<bool> Put(int id, [FromBody] CommentBase comment)
        {
            if (comment != null)
            {
                Comment updatedComment = new Comment
                {
                    CommentID = id,
                    Content = comment.Content,
                    CommentDate = comment.CommentDate,
                    UserID = comment.UserID,
                    PostID = comment.PostID
                };
                if (commentRepository.Update(updatedComment))
                {
                    return true;
                }
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            if (commentRepository.Delete(id))
            {
                return true;
            }
            return BadRequest();
        }
    }
}
