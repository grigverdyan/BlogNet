using Microsoft.AspNetCore.Mvc;
using BlogNet_DAL;
using BlogNet_DAL.Models;

namespace BlogNet_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public UsersController(UserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }

        [HttpGet("{id:int}")]
        public User Get(int id)
        {
            return userRepository.Get(id);
        }
        [HttpGet]
        public IEnumerable<User> Get() 
        {
            return userRepository.Get();
        }
        [HttpPost]
        public ActionResult<User> Post([FromBody] UserBase user) 
        {
            if (user != null)
            {
                return userRepository.Insert(user);
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult<bool> Put(int id, [FromBody] UserBase user) 
        {
            if (user != null)
            {
                User updatedUser = new User
                {
                    UserID = id,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    RegistrationDate = user.RegistrationDate,
                    PhotoUrl = user.PhotoUrl
                };
                if (userRepository.Update(updatedUser)) 
                {
                    return true;
                }
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id) 
        { 
            if (userRepository.Delete(id)) 
            {
                return true;
            }
            return BadRequest();
        }
    }
}
